using Org.BouncyCastle.Asn1.X509.Qualified;
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

        // Các thuộc tính bổ sung
        public InArgument<string> ClientCertificate { get; set; }

        public InArgument<string> ClientCertificatePassword { get; set; }

        public InArgument<bool> EnableSSLVerification { get; set; } = new InArgument<bool>(false);

        public InArgument<string> TimeOut { get; set; }

        public InArgument<bool> Preview { get; set; }

        public AcceptResponseAsMode AcceptResponseAsMode { get; set; }

        public RequestMethod RequestMethod { get; set; }

        public InArgument<string> AddParameter { get; set; }
        
        public InArgument<string> AddFilePath { get; set; }

        public Type TypeParameter { get; set; }
        protected override void Execute(CodeActivityContext context)
        {

            var addParameter = AddParameter.Get(context);
            var endpoint = Endpoint.Get(context);
            var method = Method.Get(context);
            var body = Body.Get(context);
            var enableSSLVerification = EnableSSLVerification.Get(context);
            var addFilePath = AddFilePath.Get(context);
            var accpectMode = AcceptResponseAsMode.GetType();
            var requestMethod = RequestMethod.GetType();
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentException("Endpoint cannot be null or empty.");
            }

            var clientCertificate = ClientCertificate.Get(context);
            var clientCertificatePassword = ClientCertificatePassword.Get(context);
            if (enableSSLVerification)
            {
                
                Console.WriteLine("SSL verification is enabled.");
            }
            else
            {
                Console.WriteLine("SSL verification is disabled.");
            }


            // Sử dụng phương thức đồng bộ để gọi hoạt động bất đồng bộ
            var httpResponse = SendHttpRequest(endpoint, method, body, clientCertificate, clientCertificatePassword, enableSSLVerification,addParameter, addFilePath,accpectMode,requestMethod).GetAwaiter().GetResult();
            Response.Set(context, httpResponse);
        }

        private async Task<string> SendHttpRequest(string endpoint, string method, string body, string clientCertificate, string clientCertificatePassword, bool enableSSLVerification, string addParameter, string addFilePath, Type accpectMode, Type requestMethod)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    // Thêm chứng chỉ khách hàng nếu có
                    if (!string.IsNullOrEmpty(clientCertificate))
                    {
                        var certificate = new X509Certificate2(clientCertificate, clientCertificatePassword);
                        handler.ClientCertificates.Add(certificate);
                    }

                    // Kích hoạt hoặc vô hiệu hóa xác minh SSL
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

                        response.EnsureSuccessStatusCode(); // Ném ngoại lệ nếu mã trạng thái không thành công

                        return await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại hoặc xử lý ngoại lệ nếu cần
                throw new ApplicationException("An error occurred while sending the HTTP request.", ex);
            }
        }
    }
}
