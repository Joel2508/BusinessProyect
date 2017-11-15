using BusinessProyect.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProyect.Service
{
    public class ApiService
    {
        #region Methods

        #region Method the connection
        public async Task<Responses> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Responses
                {
                    IsSuccess = true,
                    Message = "Please turn on your internet setting.",
                };
            }

            var isRechable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isRechable)
            {
                return new Responses
                {
                    IsSuccess = false,
                    Message = "Check your connection the internet",
                };
            }

            return new Responses
            {
                IsSuccess = true,
                Message = "OK",
            };
        }
        #endregion

        #region Method the reponse token
        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                new StringContent(string.Format("grant_type=password&username={0}&password={1}", username, password),
                Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Method the get generic
        public async Task<Responses> GetList<T>(string urlBase, string servicePrefix, string controller,
             string accessToken, string tokenType)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Responses
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Responses
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
        #endregion

        #endregion
    }
}
