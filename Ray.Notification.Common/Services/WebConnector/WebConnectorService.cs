using Newtonsoft.Json;
using Ray.Notification.Common.Exceptions;
using RestSharp;
using System;
using System.Net;

namespace Ray.Notification.Common.Services.WebConnector
{
    public class WebConnectorService : IWebConnectorService
    {
        private readonly string _uri;

        public WebConnectorService()
        {
            _uri = "http://localhost:9001/api/WebConnector";
        }

        public bool IsUserLogin()
        {
            var isUserLogin = Get("IsUserLogin", false);

            return isUserLogin.Result && Convert.ToBoolean(isUserLogin.Value.ToString());
        }

        public string GetBaseUrl()
        {
            var baseUrl = Get("BaseUrl", false);

            return baseUrl.Result ? baseUrl.Value.ToString() : throw new MessageException("آدرس پایه یافت نشد");
        }

        public ResultModel Get(string key, bool delete = true)
        {
            var client = new RestClient($"{_uri}/Get?key={key}&delete={delete}") { Timeout = -1 };
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return JsonConvert.DeserializeObject<ResultModel>(response.Content);
                case HttpStatusCode.BadRequest:
                    return new ResultModel(false, string.Empty);
                case HttpStatusCode.InternalServerError:
                    throw new WebConnectorException("ارتباط با سرویس افزونه ورد برقرار نشد");
            }

            throw new WebConnectorException("ارتباط با سرویس افزونه ورد برقرار نشد");

        }

        public bool Set(string key, string value)
        {
            var client =
                new RestClient($"{_uri}/Set?key={key}&value={value}") { Timeout = -1 };
            var request = new RestRequest(Method.POST);
            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new WebConnectorException("ارتباط با سرویس افزونه ورد برقرار نشد");

            var result = JsonConvert.DeserializeObject<ResultModel>(response.Content);

            return result != null && result.Result;
        }

        public ResultModel GetAllData()
        {
            var client = new RestClient($"{_uri}/GetData") { Timeout = -1 };
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new WebConnectorException("ارتباط با سرویس افزونه ورد برقرار نشد");

            return JsonConvert.DeserializeObject<ResultModel>(response.Content);
        }

        public string GetUserId()
        {
            var client = new RestClient($"{_uri}/Login") { Timeout = -1 };
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new WebConnectorException("ارتباط با سرویس افزونه ورد برقرار نشد");

            var user = JsonConvert.DeserializeObject<ResultModel>(response.Content);

            return user != null && user.Result ? user.Value.ToString() : throw new WebConnectorException();
        }
    }
}
