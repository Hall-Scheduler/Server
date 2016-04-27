﻿using HallScheduler.Desktop.Client.ViewModels;
using HallScheduler.Desktop.Infrastructure.ExtensionMethods;
using HallScheduler.Desktop.Infrastructure.Helpers;
using HallScheduler.Desktop.Models;
using HallScheduler.Desktop.Services.Contracts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HallScheduler.Desktop.Client.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            this.InitializeComponent();
            this.ViewModel = new LoginViewModel();
        }

        public LoginViewModel ViewModel { get; set; }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var httpService = NinjectHelper.Kernel.Get<IHttpService>();
            var tokenUrl = "http://localhost:38013/Token";
            var tokenData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", this.usernameBox.Text),
                new KeyValuePair<string, string>("password", this.passwordBox.SecurePassword.ConvertToUnsecureString())
            };

            var response = await httpService.Post<AuthTokenModel>(tokenUrl, tokenData);
            var responseAsAuthTokenModel = (response as AuthTokenModel);

            var identityService = NinjectHelper.Kernel.Get<IIdentityService>();
            identityService.AuthToken = responseAsAuthTokenModel.Access_Token.ConvertToSecureString();
            identityService.LoadIdentity(httpService);

            var nextPage = new SelectHallView();
            nextPage.Show();
            this.Close();

            // Get Token with username and password + grant_type
            // Save it as secureString
            // Get IdentityService
            // Save the token to the identityService
            // LoadFullIdentity data with IdentityService
            // Proceed to the next page
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
