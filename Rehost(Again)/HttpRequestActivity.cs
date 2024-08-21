using System;
using System.Activities;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Rehost_Again_
{
    public class HttpRequestActivity : CodeActivity
    {
        public InArgument<string> Endpoint { get; set; }
        public InArgument<string> Method { get; set; }
        public InArgument<string> Body { get; set; }
        public OutArgument<string> Response { get; set; }

        // Additional fields
        public InArgument<string> ClientCertificate { get; set; }
        public InArgument<string> ClientCertificatePassword { get; set; }
        public InArgument<bool> EnableSSLVerification { get; set; }

        public InArgument<bool> Preview { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            var endpoint = Endpoint.Get(context);
            var method = Method.Get(context);
            var body = Body.Get(context);

            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentException("Endpoint cannot be null or empty.");
            }

            var clientCertificate = ClientCertificate.Get(context);
            var clientCertificatePassword = ClientCertificatePassword.Get(context);
            var enableSSLVerification = EnableSSLVerification.Get(context);

            // Use synchronous method to call async operation
            var httpResponse = SendHttpRequest(endpoint, method, body, clientCertificate, clientCertificatePassword, enableSSLVerification).GetAwaiter().GetResult();
            Response.Set(context, httpResponse);
        }

        private async Task<string> SendHttpRequest(string endpoint, string method, string body, string clientCertificate, string clientCertificatePassword, bool enableSSLVerification)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    // Add client certificate if provided
                    if (!string.IsNullOrEmpty(clientCertificate))
                    {
                        var certificate = new X509Certificate2(clientCertificate, clientCertificatePassword);
                        handler.ClientCertificates.Add(certificate);
                    }

                    // Enable or disable SSL verification
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        return enableSSLVerification || errors == System.Net.Security.SslPolicyErrors.None;
                    };

                    using (var client = new HttpClient(handler))
                    {
                        HttpResponseMessage response = null;

                        switch (method.ToUpper())
                        {
                            case "GET":
                                response = await client.GetAsync(endpoint);
                                break;
                            case "POST":
                                response = await client.PostAsync(endpoint, new StringContent(body));
                                break;
                            case "PUT":
                                response = await client.PutAsync(endpoint, new StringContent(body));
                                break;
                            case "DELETE":
                                response = await client.DeleteAsync(endpoint);
                                break;
                            default:
                                throw new NotSupportedException($"HTTP method {method} is not supported.");
                        }

                        response.EnsureSuccessStatusCode(); // Throw if not a success code.

                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new ApplicationException("An error occurred while sending the HTTP request.", ex);
            }
        }
    }
}
