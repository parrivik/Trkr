using System;
using System.Collections.Generic;
using Trkr.RestServices.DataConstruct;
using Xamarin.Forms;



namespace Trkr
{
    public partial class LoginPage : ContentPage
    {
        Trkr.RestServices.RestService _restService;

        public LoginPage()
        {
            InitializeComponent();
            _restService = new Trkr.RestServices.RestService();
        }


        async void OnLogin(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(loginEmail.Text) && !string.IsNullOrWhiteSpace(loginPassword.Text))
            {

                UserData userData = await _restService.CheckIfUserExists(GenerateRequestUri(Constants.userAdress));

                if (userData != null && userData.Email.Equals(loginEmail.Text) && userData.Password.Equals(loginPassword.Text))
                {
                    BindingContext = userData;
                    App.logedInUser = userData;
                    Application.Current.MainPage = new UiContent.MainPage();
                }
                else
                {
                    await DisplayAlert("Alert", "Email or Password are wrong!", "OK");
                }
            }
        }


        async void OnRegister(object sender, EventArgs e)
        {
            UserData userToSave = new UserData();
            userToSave.Email = registerEmail.Text;
            userToSave.Password = registerPassword.Text;

            if (!string.IsNullOrWhiteSpace(registerEmail.Text) && !string.IsNullOrWhiteSpace(registerPassword.Text))
            {
                UserData userData = await _restService.CreateUser(GenerateUserUri(Constants.userAdress), userToSave);

                if (userData != null)
                {
                    BindingContext = userData;
                    App.logedInUser = userData;
                    Application.Current.MainPage = new UiContent.MainPage();
                }
                else
                {
                    await DisplayAlert("Alert", "User already exists!", "OK");
                }

                //bool response = await DisplayAlert("Register", "Would you like to register yourself?", "Yes", "No");

            }
        }


        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"{loginEmail.Text}";
            requestUri += $"?apiKey={Constants.APIKey}";
            return requestUri;
        }


        string GenerateUserUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?apiKey={Constants.APIKey}";
            return requestUri;
        }

    }
}
