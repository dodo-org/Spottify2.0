
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spottify2.Core.Singeltons
{
    public class Api_Communication
    {
        private static readonly Lazy<Api_Communication> _instance = new Lazy<Api_Communication>(() => new Api_Communication());

        public static Api_Communication Instance => _instance.Value;

        private string Token;

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

                client.DefaultRequestHeaders.Add("Token", Token);
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
                    return default;
                }
            }
            catch (Exception e)
            {
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














    }
}

