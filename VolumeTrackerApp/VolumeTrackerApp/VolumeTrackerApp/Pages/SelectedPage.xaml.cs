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

            if (App.MangaDatabase.MangaInList(selectedManga.id))
            {
                AddRemoveButton.Text = "Remove from Collecting";
            }
        }

        async private void AddMangaToCollection(object sender, EventArgs e)
        {
            if(AddRemoveButton.Text == "Add to Collecting")
            {
                await App.MangaDatabase.SaveMangaAsync(new MangaObject
                {
                    id = selectedManga.id,
                    title = selectedManga.title,
                    imageURL = selectedManga.imageURL,
                    description = selectedManga.description,
                    volumesCollected = 0
                });

                await DisplayAlert("Added", selectedManga.title + " added to collecting", "Close");

                AddRemoveButton.Text = "Remove from Collecting";
            }
            else
            {
                await App.MangaDatabase.DeleteManga(selectedManga);

                await DisplayAlert("Removed", selectedManga.title + " removed to collecting", "Close");

                AddRemoveButton.Text = "Add to Collecting";
            }
        }
    }
}