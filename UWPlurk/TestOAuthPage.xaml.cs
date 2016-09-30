using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPlurk.Api;
using UWPlurk.Api.OAuth;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白頁項目範本已記錄在 http://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPlurk
{
    /// <summary>
    /// 可以在本身使用或巡覽至框架內的空白頁面。
    /// </summary>
    public sealed partial class TestOAuthPage : Page
    {
        public TestOAuthPage()
        {
            this.InitializeComponent();
        }

        private void AppSecretBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void AppSecret_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Test request token API.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonRequestToken_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(AppKey.Text))
            {
                MessageDialog msgbox = new MessageDialog("Please enter appkey.");
                msgbox.Commands.Add(new UICommand("Ok") { Id = 0 });

                var result = await msgbox.ShowAsync();
                return;
            }

            if (String.IsNullOrEmpty(AppSecret.Text))
            {
                MessageDialog msgbox2 = new MessageDialog("Please enter appsecret.");
                msgbox2.Commands.Add(new UICommand("Ok") { Id = 0 });

                var result = await msgbox2.ShowAsync();
                return;
            }

            // Create a testApi object for retrieve Request Token
            PlurkAPI plurkApi = new PlurkAPI(AppKey.Text, AppSecret.Text);
            await plurkApi.GetRequestToken();
            OAuthToken token = plurkApi.Token;

            if (token != null)
            {
                TokenContent.Text = !String.IsNullOrEmpty(token.content) ? token.content : "";
                TokenSecret.Text = !String.IsNullOrEmpty(token.secret) ? token.secret : "";
            }

        }

        private void AccessTokenbutton_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (!String.IsNullOrEmpty(TokenContent.Text) && !String.IsNullOrEmpty(TokenSecret.Text))
            {
                //string verificationUrl = PlurkAPI
                PlurkAPI plurkApi = new PlurkAPI(AppKey.Text, AppSecret.Text, TokenContent.Text, TokenSecret.Text);
                string authericateUrl = plurkApi.GetAuthorizeTokenUrl();
                
            }
            */
        }

        private void TokenContent_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TokenSecret_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
