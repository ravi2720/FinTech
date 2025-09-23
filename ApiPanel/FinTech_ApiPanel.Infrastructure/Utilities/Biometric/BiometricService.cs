using FinTech_ApiPanel.Application.Abstraction.IBiometric;
using System.Text;

namespace FinTech_ApiPanel.Infrastructure.Utilities.Biometric
{
    public class BiometricService : IBiometricService
    {
        public BiometricService()
        {
            
        }

        public async Task<string> CaptureFingerprintAsync()
        {
            HttpClient _httpClient = new HttpClient();
            var url = "http://127.0.0.1:11100/rd/capture";
            var requestXml = @"<PidOptions ver=""1.0"">
                <Opts 
                    fCount=""1"" 
                    fType=""2"" 
                    pCount=""0"" 
                    pType=""0"" 
                    format=""0"" 
                    pidVer=""2.0"" 
                    timeout=""15000"" 
                    otp="""" 
                    wadh="""" 
                    posh="""" />
            </PidOptions>";

            var content = new StringContent(requestXml, Encoding.UTF8, "application/xml");

            try
            {
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                // Read the RD service XML response (PidData)
                var responseXml = await response.Content.ReadAsStringAsync();

                // You can deserialize or return as-is
                return responseXml;
            }
            catch (HttpRequestException ex)
            {
                return $"Error connecting to RD Service: {ex.Message}";
            }
            catch (TaskCanceledException)
            {
                return "Request timed out or user did not scan fingerprint.";
            }
        }
    }
}
