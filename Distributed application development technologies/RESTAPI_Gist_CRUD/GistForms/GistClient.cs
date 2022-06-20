using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GistForms
{
    static class GistClient
    {
        private static String urlPrefix = "https://api.github.com/gists";
        private static async Task<Gist> JsonToGistAsync(JToken j)
        {
            var gist = new Gist();
            gist.url = (string)j.SelectToken("url");
            gist.id = (string)j.SelectToken("id");
            gist.@public = (bool)j.SelectToken("public");
            gist.description = (string)j.SelectToken("description");
            var files = (JObject)j.SelectToken("files");
            var list = new List<GistFile>();
            foreach (var file in files)
            {
                var gistFile = new GistFile(file.Key, (string)file.Value["raw_url"]);
                gistFile.content = await ConnectionHandler.httpClient.GetStringAsync(gistFile.raw_url);
                list.Add(gistFile);
            }
            gist.files = list;
            return gist;
        }
        static dynamic CreateGistJson(string desc, Tuple<string, string>[] filenamesContents)
        {
            dynamic _result = new JObject();
            dynamic _file = new JObject();
            _result.description = desc;
            _result.files = JObject.FromObject(new { });
            foreach (var filenameContent in filenamesContents)
                _result.files[filenameContent.Item1] = JObject.FromObject(new { content = filenameContent.Item2 });
            return _result;
        }

        internal static async Task<List<Gist>> GetGists()
        {
            var requestUrl = new Uri(string.Format($"{urlPrefix}?"));
            var response = await ConnectionHandler.httpClient.GetStringAsync(requestUrl);
            var json = JArray.Parse(response);
            List<Gist> result = new List<Gist>();
            foreach (var j in json)
            {
                result.Add(await JsonToGistAsync(j));
            }
            return result;
        }

        internal static async Task CreateGist(string description, bool isPublic, Tuple<string, string>[] filenamesContents)
        {
            var requestUri = string.Format($"{urlPrefix}");
            var result = CreateGistJson(description, filenamesContents);
            result.@public = isPublic.ToString().ToLower();
            var data = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            var response = await ConnectionHandler.httpClient.PostAsync(requestUri, data);
            response.EnsureSuccessStatusCode();
        }

        internal static async Task DeleteGist(string id)
        {
            var requestUri = new Uri(string.Format($"{urlPrefix}/{id}"));
            var response = await ConnectionHandler.httpClient.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }
        internal static async Task EditGist(string id, string description, Tuple<string, string>[] filenamesContents)
        {
            var requestUri = new Uri(string.Format($"{urlPrefix}/{id}"));
            var result = CreateGistJson(description, filenamesContents);
            var data = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            var response = await ConnectionHandler.httpClient.PatchAsync(requestUri, data);
            response.EnsureSuccessStatusCode();
        }
    }
}
