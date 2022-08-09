using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using VolumeTrackerApp.Models;
using System.Threading.Tasks;
using GraphQL.Client.Http;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Serializer.Newtonsoft;

namespace VolumeTrackerApp.ViewModels
{
class MainPageViewModel
    {
        //variables
        public ObservableCollection<MangaObject> Data { get; set; }
        public IGraphQLWebsocketJsonSerializer JsonSerializer { get; }

        //commands
        public ICommand SearchCommand { get; set; }

        //constructor
        public MainPageViewModel()
        {
            Data = new ObservableCollection<MangaObject>();

            SearchCommand = new Command<string>(async(search) => 
            {
                await GetAnilistData(search);
            });
        }

        public async Task GetAnilistData(string search)
        {
            var graphQLClient = new GraphQLHttpClient("https://graphql.anilist.co/", new NewtonsoftJsonSerializer());

            var request = new GraphQLHttpRequest
            {
                Query = @"query ($id: Int, $page: Int, $perPage: Int) {
                         Page (page: $page, perPage: $perPage) {
                           pageInfo {
                             total,
                             currentPage,
                             lastPage,
                             hasNextPage,
                             perPage,
                           }
                           media (id: $id, search: """ + search + @""", type: MANGA) {
                             id,
                                       title {
                                           romaji
                               english
                               native
                                       },
                                       volumes,
                                       coverImage {
                                           extraLarge
                               large
                               medium
                               color
                                       }
                                   }
                         }
                           }"
            };

            var response = await graphQLClient.SendQueryAsync<Data>(request);
            Data.Clear();
            foreach(Media media in response.Data.Page.media)
            {
                if(media.title.english != null)
                {
                    Data.Add(new MangaObject {title = media.title.english, imageURL = media.coverImage.medium });
                }
                else if(media.title.english == null)
                {
                    if(media.title.romaji != null)
                    {
                        Data.Add(new MangaObject { title = media.title.romaji, imageURL = media.coverImage.medium });
                    }
                    else
                    {
                        Data.Add(new MangaObject { title = media.title.native, imageURL = media.coverImage.medium });
                    }
                }
            }
        }
    }
}
