using AutoMapper;
using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Commands.FinancialComponentMasters;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Commands.ServiceMasters;
using FinTech_ApiPanel.Application.Commands.UserBanks;
using FinTech_ApiPanel.Application.Commands.UserFinancialComponents;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Commands.Whitelists;
using FinTech_ApiPanel.Application.Queries.FinancialComponentMasters;
using FinTech_ApiPanel.Application.Queries.UserFinancialComponents;
using FinTech_ApiPanel.Domain.DTOs.BankMasters;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using FinTech_ApiPanel.Domain.DTOs.FundRequests;
using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Entities.FinancialComponentMasters;
using FinTech_ApiPanel.Domain.Entities.FundRequests;
using FinTech_ApiPanel.Domain.Entities.ServiceMasters;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserFinancialComponents;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Entities.Whitelists;

namespace FinTech_ApiPanel.Infrastructure.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // User Master
            CreateMap<UserMaster, CreateUserCommand>().ReverseMap();
            CreateMap<UserMaster, UserMasterDto>().ReverseMap();
            CreateMap<UserMaster, UpdateUserCommand>().ReverseMap();
            CreateMap<UserMaster, VerifyUserDetailCommand>().ReverseMap();
            CreateMap<UserMaster, LoggedInUserDto>().ReverseMap();
            CreateMap<UserMaster, UserMasterListDto>().ReverseMap();

            // Services
            CreateMap<ServiceMaster, CreateServiceMasterCommand>().ReverseMap();
            CreateMap<ServiceMaster, UpdateServiceMasterCommand>().ReverseMap();

            // Fund Request
            CreateMap<FundRequest, CreateFundRequestCommand>().ReverseMap();
            CreateMap<FundRequest, FundRequestDto>().ReverseMap();
            CreateMap<FundRequest, UpdateFundRequestCommand>().ReverseMap();

            // Bank
            CreateMap<BankMaster, CreateBankMasterCommand>().ReverseMap();
            CreateMap<BankMaster, UpdateBankMasterCommand>().ReverseMap();
            CreateMap<BankMaster, BankDto>().ReverseMap();

            // Whitelist
            CreateMap<WhitelistEntry, WhitelistDto>().ReverseMap();
            CreateMap<WhitelistEntry, CreateWhitelistCommand>().ReverseMap();
            CreateMap<WhitelistEntry, UpdateWhitelistCommand>().ReverseMap();

            // user bank
            CreateMap<UserBank, CreateUserBankCommand>().ReverseMap();
            CreateMap<UserBank, UpdateUserBankCommand>().ReverseMap();
            CreateMap<UserBank, UserBankDto>().ReverseMap();
            CreateMap<UserBank, BankDto>().ReverseMap();

            // Wallet
            CreateMap<TransactionLog, TransactionLogDto>().ReverseMap();

            // FinancialComponent
            CreateMap<FinancialComponentMaster, CreateFinancialComponentCommand>().ReverseMap(); 
            CreateMap<FinancialComponentMaster, UpdateFinancialComponentCommand>().ReverseMap();
            CreateMap<FinancialComponentMaster, FinancialComponentDto>().ReverseMap();

            CreateMap<UserFinancialComponent, FinancialComponentDto>().ReverseMap();
            CreateMap<UserFinancialComponent, UserFinancialComponentDto>().ReverseMap();
            CreateMap<UserFinancialComponent, UpdateUserFinancialComponentCommand>().ReverseMap();
            CreateMap<UserFinancialComponent, CreateUserFinancialComponentCommand>().ReverseMap();
        }
    }
}