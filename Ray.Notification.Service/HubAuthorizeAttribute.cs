using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ray.Notification.Service
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HubAuthorizeAttribute : Attribute, IAuthorizeHubConnection, IAuthorizeHubMethodInvocation
    {
        public bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
        {
            return IsUserAuthorized(request);
        }

        public bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            return IsUserAuthorized(hubIncomingInvokerContext.Hub.Context.Request);
        }

        protected bool IsUserAuthorized(IRequest request)
        {
            try
            {
                var parameters = new Dictionary<string, string>();

                var url = ConfigurationManager.AppSettings["idp_url"] + "/connect/introspect";

                var token = request.QueryString["Authorization"];
                var clientId = ConfigurationManager.AppSettings["idp_id"];
                var clientSecret = ConfigurationManager.AppSettings["idp_secret"];

                parameters.Add("token", token);
                parameters.Add("client_id", clientId);
                parameters.Add("client_secret", clientSecret);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.PostAsync(url, new FormUrlEncodedContent(parameters)).GetAwaiter().GetResult();
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var validateResult = JsonConvert.DeserializeObject<ValidateResultModel>(result);

                    return validateResult != null && validateResult.Validate;
                }
            }
            catch
            {
                return false;
            }
        }

        public class ValidateResultModel
        {
            public string UserName { get; set; }

            public bool Validate => !string.IsNullOrEmpty(UserName);
        }
    }
}