using Firebase.Storage;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
        
        private string url = "https://yourfirebaseprojectname.firebaseio.com/users.json";
        private HttpClient client = new HttpClient();

        public MainPage()
		{
			InitializeComponent();
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public async void addData(Object sender, EventArgs e)
        {
            var newUser = new user { name = Username.Text, lastName = Lastname.Text };
            var content = JsonConvert.SerializeObject(newUser);
            await client.PostAsync(url,new  StringContent(content));

        }
    
        public async  void getData(Object sender, EventArgs e)
        {
            var firedata = await client.GetStringAsync(url);
            data.Text = firedata.ToString();
        }
        public async void photo(Object sender,EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true

                 //Directory = "Sample",
                 // Name = "test.jpg"
             });


            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");



            var stream = File.Open(file.Path, FileMode.Open);

            var task = new FirebaseStorage("youfirebaseprojectname.appspot.com")
            .Child("data")
            .Child("random")
            .Child("file.png")
            .PutAsync(stream);

            var downloadUrl = await task;
            data.Text = downloadUrl;


        }

       
    }
}
