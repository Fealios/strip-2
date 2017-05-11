using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YouStrip.Models
{
    public class Request
    {
        public string SongName { get; set; }
        public Request(string songName)
        {
            SongName = songName;
        }


        public string SendRequest()
        {
            var client = new RestClient("https://kashyap32-youtubetomp3-v1.p.mashape.com/");
            var request = new RestRequest(SongName, Method.GET);
            request.AddHeader("X-Mashape-Key", "crzGQW4aYomshKi5WsrQzmQn9V5Up1SV43Qjsn8E2bow75qHzw");
            request.AddHeader("Accept", "text/plain");
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var testString = jsonResponse["data"][0]["link"].ToString();
            return testString;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
