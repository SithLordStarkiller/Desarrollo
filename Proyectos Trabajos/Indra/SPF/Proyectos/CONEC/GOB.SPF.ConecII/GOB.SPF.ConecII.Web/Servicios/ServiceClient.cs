using GOB.SPF.ConecII.Web.Resources;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GOB.SPF.ConecII.Web.Servicios
{
    internal static class ServiceClient
    {
        private const string privateKey = "sw6p!eg?3uwr2jufr*6eth@qE";
        private const string issuer = "GOB.SPF";
        private const string audience = "GOB.SPF.CONECII";
        private static SymmetricSecurityKey sign = new SymmetricSecurityKey(Encoding.Default.GetBytes(privateKey));
        private static SigningCredentials credentials = new SigningCredentials(sign, SecurityAlgorithms.HmacSha256);
        private static JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
        private static JwtSecurityToken _token;
        private static string _apiUrl;
        private static int _apiPort;

        private static string ApiUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_apiUrl))
                    _apiUrl = ConfigurationManager.AppSettings[ResourceAppSettings.URLApi];
                return _apiUrl;
            }
        }

        private static int ApiPort
        {
            get
            {
                if (string.IsNullOrEmpty(_apiUrl))
                {
                    var port = ConfigurationManager.AppSettings[ResourceAppSettings.PortApi];
                    if (!string.IsNullOrEmpty(port))
                    {
                        _apiPort = Convert.ToInt32(port);
                    }
                    else
                    {
                        _apiPort = -1;
                    }
                }
                return _apiPort;
            }
        }
        private static JwtSecurityToken Token
        {
            get
            {
                if (_token == null || DateTime.UtcNow.AddSeconds(60) >= _token.ValidTo)
                {
                    var tokenDescriptor = new SecurityTokenDescriptor()
                    {
                        Issuer = issuer,
                        Audience = audience,
                        IssuedAt = DateTime.UtcNow,
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = credentials,
                    };
                    _token = securityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                }
                return _token;
            }
        }
        private static string _tokenString;
        private static string TokenString
        {
            get
            {
                if (string.IsNullOrEmpty(_tokenString) || DateTime.UtcNow.AddSeconds(60) >= _token.ValidTo)
                    _tokenString = securityTokenHandler.WriteToken(Token);
                return _tokenString;
            }
        }

        private static HttpClient _client;
        internal static HttpClient Client
        {
            get
            {
                if (_client == null)
                    _client = new HttpClient();
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenString}");
                return _client;
            }
        }

        internal static async Task<T> GetObjectAsync<T>(string controller, string action,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null)
        {
            T returnValue;
            var uri = GetUri(controller, action, queryStringParameters);
            try
            {
                var response = await Client.GetStringAsync(uri);
                returnValue = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception ex)
            {
                returnValue = default(T);
            }
            return returnValue;
        }

        internal static async Task<TResponse> PostObjectAsync<TRequest, TResponse>(string controller, string action, TRequest data)
        {
            TResponse returnValue;
            var uri = GetUri(controller, action);
            try
            {
                var response = await Client.PostAsJsonAsync(uri, data);
                if (response.IsSuccessStatusCode)
                {
                    returnValue = await response.Content.ReadAsAsync<TResponse>();
                }
                else
                {
                    returnValue = default(TResponse);
                }
            }
            catch (Exception ex)
            {
                returnValue = default(TResponse);
            }
            return returnValue;
        }

        internal static async Task<bool> PostObjectAsync<TRequest>(string controller, string action, TRequest data)
        {
            var uri = GetUri(controller, action);
            try
            {
                var response = await Client.PostAsJsonAsync(uri, data);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal static async Task<TResponse> PutObjectAsync<TRequest, TResponse>(string controller, string action, string idName,
            string idValue, TRequest data)
        {
            TResponse returnValue;
            var uri = GetUri(controller, action, new[] { new KeyValuePair<string, string>(idName, idValue) });
            try
            {
                var response = await Client.PutAsJsonAsync(uri, data);
                if (response.IsSuccessStatusCode)
                {
                    returnValue = await response.Content.ReadAsAsync<TResponse>();
                }
                else
                {
                    returnValue = default(TResponse);
                }
            }
            catch (Exception ex)
            {
                returnValue = default(TResponse);
            }
            return returnValue;
        }

        internal static async Task<TResponse> PostObjectAsync<TResponse, TRequest>(string controller, string action, string idName,
            string idValue, TRequest data)
        {
            TResponse returnValue;
            var uri = GetUri(controller, action, new[] { new KeyValuePair<string, string>(idName, idValue) });
            try
            {
                var response = await Client.PostAsJsonAsync(uri, data);
                if (response.IsSuccessStatusCode)
                {
                    returnValue = await response.Content.ReadAsAsync<TResponse>();
                }
                else
                {
                    returnValue = default(TResponse);
                }
            }
            catch (Exception ex)
            {
                returnValue = default(TResponse);
            }
            return returnValue;
        }

        internal static async Task<bool> PutObjectAsync<TRequest>(string controller, string action, string idName,
            string idValue, TRequest data)
        {
            var uri = GetUri(controller, action,
                new[] { new KeyValuePair<string, string>(idName, idValue) });
            try
            {
                var response = await Client.PutAsJsonAsync(uri, data);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static Uri GetUri(string controller, string action,
            IEnumerable<KeyValuePair<string, string>> queryStringParameters = null, string scheme = "http")
        {
            var queryString = string.Empty;
            if (queryStringParameters != null)
                queryString = GetQueryString(queryStringParameters);

            var uriBuilder = new UriBuilder()
            {
                Port = ApiPort,
                Scheme = scheme,
                Host = ApiUrl,
                Path = Path.Combine("api", controller, action),
                Query = queryString,
            };

            return uriBuilder.Uri;
        }

        private static string GetQueryString(IEnumerable<KeyValuePair<string, string>> values)
        {
            List<string> parameters = new List<string>();
            foreach (var elem in values)
            {
                parameters.Add($"{elem.Key}={elem.Value}");
            }
            string queryString = string.Join("&", parameters.ToArray());
            return queryString;
        }
    }
}