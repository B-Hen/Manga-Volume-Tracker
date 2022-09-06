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
using VolumeTrackerApp.Pages;

namespace VolumeTrackerApp.ViewModels
{
class MainPageViewModel
    {
        //variables
        public ObservableCollection<MangaObject> Data { get; set; }
        public IGraphQLWebsocketJsonSerializer JsonSerializer { get; }

        //commands
        public ICommand SearchCommand { get; set; }
        public ICommand SelectedCommand { get; set; }

        //properties
        private MangaObject selectedManga;
        public MangaObject SelectedManga
        {
            get { return selectedManga; }
            set { selectedManga = value; }
        }

        //constructor
        public MainPageViewModel()
        {
            Data = new ObservableCollection<MangaObject>();

            SearchCommand = new Command<string>(async(search) => 
            {
                await GetAnilistData(search);
            });

            SelectedCommand = new Command<MangaObject>(async(mangaObject) => {
                await Application.Current.MainPage.Navigation.PushAsync(new SelectedPage(mangaObject));
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
                           media (id: $id, search: """ + search + @""", type: MANGA, isAdult: false) {
                             id, description,
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
                    Data.Add(new MangaObject { id = media.id, title = media.title.english, imageURL = media.coverImage.medium, description = media.description});
                }
                else if(media.title.english == null)
                {
                    if(media.title.romaji != null)
                    {
                        Data.Add(new MangaObject { id = media.id, title = media.title.romaji, imageURL = media.coverImage.medium, description = media.description });
                    }
                    else
                    {
                        Data.Add(new MangaObject { id = media.id, title = media.title.native, imageURL = media.coverImage.medium, description = media.description });
                    }
                }
            }
        }
    }
}
