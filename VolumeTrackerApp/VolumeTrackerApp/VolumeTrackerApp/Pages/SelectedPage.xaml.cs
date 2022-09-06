using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeTrackerApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolumeTrackerApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedPage : ContentPage
    {
        MangaObject selectedManga;
        public SelectedPage(Models.MangaObject selectedManga)
        {
            InitializeComponent();
            this.selectedManga = selectedManga;
            MangaTitle.Text = selectedManga.title;
            MangaImage.Source = selectedManga.imageURL;
            MangaDescription.Text = selectedManga.description;
        }

        async private void AddMangaToCollection(object sender, EventArgs e)
        {
            if(App.MangaDatabase.MangaInList(selectedManga.id))
            {
                await DisplayAlert("Manga already in collection", "Then manga you are trying to add is already in your collection", "Close");
            }
            else
            {
                await App.MangaDatabase.SaveMangaAsync(new MangaObject
                {
                    id = selectedManga.id, 
                    title = selectedManga.title,
                    imageURL = selectedManga.imageURL,
                    description = selectedManga.description,
                    volumesCollected = 0
                });
            }
        }
    }
}