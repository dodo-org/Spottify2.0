
using Newton = Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Spottify2.Models.Reply;
using MJM_Systems.ApiCalls;

namespace Spottify2.Core.Singeltons
{
    public class Api_Communication
    {
        private static readonly Lazy<Api_Communication> _instance = new Lazy<Api_Communication>(() => new Api_Communication());

        public static Api_Communication Instance => _instance.Value;

        public string Token;

        public void SetToken(string _token)
        {
            Token = _token;
        }
        private static void HandleErrorRespons(HttpResponseMessage response)
        {

            switch (response.StatusCode)
            {
                //Todo: WriteLogs 
                //case System.Net.HttpStatusCode.BadRequest:
                //    MessageBox.Show("Ihre eingegebenen Daten sind fehlerhaft" + response.Content.ReadAsStringAsync().Result);
                //    break;
                //case System.Net.HttpStatusCode.Locked:
                //    MessageBox.Show("Der eingegebene Datensatz existiert bereits");
                //    break;
                //case System.Net.HttpStatusCode.NoContent:
                //    MessageBox.Show("Keine daten gefunden");
                //    break;
                //case System.Net.HttpStatusCode.Forbidden:
                //    MessageBox.Show("Token ist falsch");
                //    break;
            }

        }

        public TOutput? Get<TOutput>(string _url)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.


                HttpResponseMessage response = client.GetAsync(_url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                client.Dispose();


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Parse the response body.

                    var responseContent = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var PostResponse = JsonSerializer.Deserialize<TOutput>(responseContent);

                    return PostResponse;
                }
                else
                {
                    // Error handdler aufrufen
                    HandleErrorRespons(response);
                    throw new Exception("Error");
                    return default;
                }
            }
            catch (Exception e)
            {
                throw e;
                return default;
            }
        }

        public TOutput? Post<TOutput>(string _url, Dictionary<string, string> Input = null)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("Token", Token);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                var content = new FormUrlEncodedContent(Input);

                HttpResponseMessage response = client.PostAsync(_url, content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                client.Dispose();


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Parse the response body.

                    var responseContent = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var PostResponse = JsonSerializer.Deserialize<TOutput>(responseContent);

                    return PostResponse;
                }
                else
                {
                    // Error handdler aufrufen
                    HandleErrorRespons(response);
                    return default;
                }
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public TOutput? Update<TOutput>(string _url, Dictionary<string, string> Input = null)
        {
            try
            {
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Add("token", Token);

                var content = new FormUrlEncodedContent(Input);


                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.PutAsync(_url, content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                client.Dispose();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var PostResponse = JsonSerializer.Deserialize<TOutput>(response.Content.ReadAsStringAsync().Result);

                    return PostResponse;
                }
                else
                {
                    HandleErrorRespons(response);
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return default;
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                return default;
            }
        }

        public Task<bool?> Login(Dictionary<string, string> Input = null)
        {
            bool? _AnswerTrue = true;
            bool? _AnswerFalse = false;


            try
            {
                HttpClient client = new HttpClient();


                // List data response.
                var content = new StringContent(Newton.JsonConvert.SerializeObject(Input), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(URL_S.Login, content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                client.Dispose();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var Result = response.Content.ReadAsStringAsync().Result;
                    var PostResponse = JsonSerializer.Deserialize<LoginReply_Model>(Result);
                    Token = PostResponse.token;
                    return Task.FromResult(_AnswerTrue);
                }
                else
                {
                    HandleErrorRespons(response);
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return Task.FromResult(_AnswerFalse);
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                return Task.FromResult(_AnswerFalse);
            }
        }

        public Task<RegistrationReply_Model?> Register(Dictionary<string, string> Input = null)
        {
            RegistrationReply_Model? Response = null;

            try
            {
                HttpClient client = new HttpClient();


                // List data response.
                var content = new StringContent(Newton.JsonConvert.SerializeObject(Input), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync(URL_S.Registration, content).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                client.Dispose();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var Result = response.Content.ReadAsStringAsync().Result;
                    RegistrationReply_Model? PostResponse = JsonSerializer.Deserialize<RegistrationReply_Model>(Result);
                    Token = PostResponse.token;
                    PostResponse.Response = RegistrationResponses.Success;


                    return Task.FromResult(PostResponse);
                }
                else
                {
                    Response = new RegistrationReply_Model();
                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        if(response.Content.ReadAsStringAsync().Result == "Der UserName existiert bereits")
                        {
                            Response.Response = RegistrationResponses.UsernameExists;
                        }
                        else
                        {
                            Response.Response = RegistrationResponses.EmailExists;
                        }
                        return Task.FromResult(Response);
                    }


                    HandleErrorRespons(response);
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return Task.FromResult(Response);
                }
            }
            catch (Exception ex)
            {
                // Log Exception
                throw ex;
                return null;
            }

        }
    }

}

