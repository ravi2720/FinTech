namespace FinTech_ApiPanel.Application.Abstraction.IHeaders
{
    public interface IHeaderService
    {
        Task AddHeaders(HttpRequestMessage request, string endpointIp, string? outletId = null, Dictionary<string, string>? additionalHeaders = null);
    }
}
