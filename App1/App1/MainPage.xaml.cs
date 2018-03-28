using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
        
        private string url = "https://essths-9bd84.firebaseio.com/users.json";
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
    }
}
