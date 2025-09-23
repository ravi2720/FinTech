using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.Auth;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Entities.UserServices;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IServiceMasterRepository _serviceMasterRepository;
        private readonly IUserServiceRepository _userServiceRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private static readonly Random _random = new Random();

        public CreateUserHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            IMapper mapper,
            IServiceMasterRepository serviceMasterRepository,
            IWalletRepository walletRepository,
            IClientRepository clientRepository,
            IUserServiceRepository userServiceRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _walletRepository = walletRepository;
            _mapper = mapper;
            _serviceMasterRepository = serviceMasterRepository;
            _userServiceRepository = userServiceRepository;
            _clientRepository = clientRepository;
        }

        public async Task<ApiResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // ===== VALIDATIONS =====

                // 1. Full Name validation
                if (string.IsNullOrWhiteSpace(request.FullName))
                    return ApiResponse.BadRequestResponse("Full Name is required.");

                // 2. Email format validation
                if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
                    return ApiResponse.BadRequestResponse("A valid Email is required.");

                // 3. Password validation (at least 6 characters, customize as needed)
                if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                    return ApiResponse.BadRequestResponse("Password must be at least 6 characters long.");

                // ===== CHECK EXISTING USER =====
                var existingUserList = await _userRepository.GetAllAsync();

                var existingUser = existingUserList.FirstOrDefault(x => x.Email == request.Email);
                if (existingUser != null)
                    return ApiResponse.ConflictResponse("User with this email already exists.");

                // ===== CREATE USER =====
                var passwordResult = _cryptoService.GenerateSaltedHash(request.Password);

                string prefix = existingUserList.Where(x => x.IsAdmin).Select(x => x.Prefix).FirstOrDefault() ?? "USER";

                var user = _mapper.Map<UserMaster>(request);
                user.UserName = GenerateUsername(existingUserList, prefix);
                user.EmailVerified = false;
                user.PhoneVerified = false;
                user.KYCVerified = false;
                user.IPin = EncryptionUtils.GenerateOtp();
                user.Salt = passwordResult.Salt;
                user.Password = passwordResult.Hash;
                user.IsAdmin = false;
                user.IsActive = true;

                var userId = await _userRepository.AddAsync(user);

                if (userId <= 0)
                    return ApiResponse.BadRequestResponse("User creation failed.");

                await CreateWallet(userId);
                await MapServices(userId);
                await CreateClient(userId);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse("Error while creating user. " + ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        async Task MapServices(long userId)
        {
            try
            {
                var serviceList = await _serviceMasterRepository.GetAllAsync();

                foreach (var service in serviceList)
                {
                    await _userServiceRepository.AddAsync(new UserService
                    {
                        UserId = userId,
                        ServiceId = service.Id,
                        IsActive = false
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding services.", ex);
            }
        }

        string GenerateUsername(IEnumerable<UserMaster> users, string prefix)
        {
            try
            {
                string username;

                do
                {
                    char[] usernameChars = new char[prefix.Length + 5];

                    // Add prefix
                    for (int i = 0; i < prefix.Length; i++)
                    {
                        usernameChars[i] = prefix[i];
                    }

                    // Add 5 random digits
                    for (int i = prefix.Length; i < usernameChars.Length; i++)
                    {
                        usernameChars[i] = (char)('0' + _random.Next(10));
                    }

                    username = new string(usernameChars);

                } while (users.Any(u => u.UserName == username));

                return username;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while generating username. {ex.Message}", ex);
            }
        }


        async Task CreateWallet(long userId)
        {
            try
            {
                var mainWallet = new Wallet
                {
                    UserId = userId,
                    TotalBalance = 0,
                    HeldAmount = 0
                };
                await _walletRepository.AddAsync(mainWallet);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating wallet. {ex.Message}");
            }
        }

        async Task CreateClient(long userId)
        {
            try
            {
                var encryptionKey = _cryptoService.Generate32CharKey();
                var client = new Client
                {
                    UserId = userId,
                    ClientId = _cryptoService.GenerateClientId(),
                    ClientSecret = _cryptoService.GenerateClientSecret(),
                    EncryptionKey = encryptionKey
                };

                await _clientRepository.AddAsync(client);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating client. {ex.Message}");
            }
        }
    }
}