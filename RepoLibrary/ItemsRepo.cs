using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace RepoLibrary
{
    public class ItemsRepo
    {
        private RestClient _client;
        public IRestResponse Get(string url)
        {
            _client = new RestClient(url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = _client.Execute(request);
            return response;
        }
        public IRestResponse Post<T>(string url, T obj)
        {
            _client = new RestClient(url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(obj);
            IRestResponse response = _client.Execute(request);
            return response;
        }
        public IRestResponse Put<T>(string url, T obj)
        {
            _client = new RestClient(url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.PUT);
            request.AddJsonBody(obj);
            IRestResponse response = _client.Execute(request);
            return response;
        }
        public IRestResponse Delete(string url)
        {
            _client = new RestClient(url)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = _client.Execute(request);
            return response;
        }
        public IEnumerable<T> ReturnList<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(response.Content);
        }
        public T ReturnObject<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
