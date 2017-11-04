using BusinessProyect.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<TokenResponse> GetToken(string urlBase, string userName, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                   "grant_type=password&username={0}&password={1}", userName, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resulJson = await response.Content.ReadAsStringAsync();
                var resutl = JsonConvert.DeserializeObject<TokenResponse>(resulJson);

                return resutl;
            }
            catch
            {
                return null;
            }

        }
        #endregion

        #region Method the get generic
        public async Task<Responses> Get<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Responses
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Responses
                {
                    IsSuccess = true,
                    Message = "OK",
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
