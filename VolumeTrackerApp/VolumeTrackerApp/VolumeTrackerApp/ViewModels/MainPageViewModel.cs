using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using VolumeTrackerApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using GraphQL.Client.Http;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL;
using GraphQL.Client.Serializer.Newtonsoft;

namespace VolumeTrackerApp.ViewModels
{
class MainPageViewModel
    {
        //variables
        public ObservableCollection<string> Data { get; set; }
        public IGraphQLWebsocketJsonSerializer JsonSerializer { get; }

        //commands
        public ICommand SearchCommand { get; set; }

        //constructor
        public MainPageViewModel()
        {
            Data = new ObservableCollection<string>();
            Data.Add("Test");

            //search command which will take in the value of the search bar and use it to get info
            //back from the anilist api
            SearchCommand = new Command<string>(async(search) => 
            {
                Data.Add(search);
                await GetAnilistData(search);
            });
        }

        public void GetSearchValue(string search)
        {
            Data.Add(search);
            
        }

        public async Task GetAnilistData(string search)
        {
            var graphQLClient = new GraphQLHttpClient("https://graphql.anilist.co/", new NewtonsoftJsonSerializer());

            var request = new GraphQLHttpRequest
            {
                Query = "query{Media(search: \"chainsaw\"){id,volumes,title{english}}}"
            };

            var response = await graphQLClient.SendQueryAsync<AnislistData.Data>(request);
            Data.Add(response.Data.Media.title.english);
            /*var client = new HttpClient();
   
            var stringContent = new StringContent("query ($id: Int, $page: Int, $perPage: Int, $search: String, $type: MediaType) {Page (page: $page, perPage: $perPage) {pageInfo {total currentPage lastPage hasNextPage perPage} media (id: $id, search: chainsaw man, type: MANGA) {id title {romaji english native} volumes chapters coverImage {extraLarge large medium color}}}}");

            var httpResponse = await client.PostAsync("https://graphql.anilist.co/", stringContent);

            var json = await httpResponse.Content.ReadAsStringAsync();

            var obj = JsonConvert.DeserializeObject<AnislistData.Rootobject>(json);*/
        }
    }
}
