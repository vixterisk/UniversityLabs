using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GistForms
{
    internal class OAuthAuthorizationHandler
    {
        private string clientID;
        private string clientSecret;
        private string state;
        private string code;
        private bool success = false;
        private ManualResetEvent manualReset = new ManualResetEvent(false);

        public OAuthAuthorizationHandler(string clientID, string clientSecret)
        {
            this.clientID = clientID;
            this.clientSecret = clientSecret;

            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            state = new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        internal async Task GetToken()
        {
            var procStartInfo = new ProcessStartInfo($"https://github.com/login/oauth/authorize?client_id={this.clientID}&scope=gist&state={this.state}")
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(procStartInfo);
            _ = Task.Run(RunServer);
            manualReset.WaitOne(60000);
        }

        private void RunServer()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:12345/");
            listener.Start();
            while (listener.IsListening)
            {
                var context = listener.GetContext();
                ProcessContext(context);
                if (success)
                    listener.Stop();
            }
        }
        private async void ProcessContext(HttpListenerContext context)
        {
            if (!success)
            {
                code = context.Request.QueryString["code"];
                var state = context.Request.QueryString["state"];
                byte[] bytes;
                if (!string.IsNullOrEmpty(code) && state == this.state)
                {
                    var requestUri = new Uri($"https://github.com/login/oauth/access_token?client_id={clientID}&client_secret={clientSecret}&code={code}");
                    var response = await ConnectionHandler.httpClient.PostAsync(requestUri, null);
                    string responseString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseString);
                    ConnectionHandler.RemoveTokenHeader();
                    ConnectionHandler.AddTokenHeader((string)json.SelectToken("access_token"));
                    ConnectionHandler.Scopes = (string)json.SelectToken("scope");
                }
                success = !string.IsNullOrEmpty(ConnectionHandler.Token);
                if (success)
                {
                    bytes = Encoding.UTF8.GetBytes(
$@"
<html>
<body>
App has been authorized. You may close this page now.
</body>
</html>");
                }
                else
                    bytes = Encoding.UTF8.GetBytes($@"
<html>
<body>
App has not been authorized.
</body>
</html>");
                context.Response.StatusCode = 200;
                context.Response.KeepAlive = true;
                context.Response.ContentLength64 = bytes.Length;
                var output = context.Response.OutputStream;
                output.Write(bytes, 0, bytes.Length);
                context.Response.Close();
                manualReset.Set();
            }
        }
    }
}