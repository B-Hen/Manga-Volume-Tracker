using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VolumeTrackerApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectedPage : ContentPage
    {
        public SelectedPage(Models.MangaObject selectedManga)
        {
            InitializeComponent();
            MangaTitle.Text = selectedManga.title;
            MangaImage.Source = selectedManga.imageURL;
        }
    }
}