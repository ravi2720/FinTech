namespace FinTech_ApiPanel.Application.Abstraction.IBiometric
{
    public interface IBiometricService
    {
        Task<string> CaptureFingerprintAsync();
    }
}
