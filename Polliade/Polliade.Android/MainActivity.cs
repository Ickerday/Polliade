using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Microsoft.Identity.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Polliade.Droid
{
    [Activity(Label = "Polliade",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);
                Forms.Init(this, savedInstanceState);

                LoadApplication(new App());
            }
            catch (Exception ex)
            {
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            AuthenticationContinuationHelper.SetAuthenticationContinuationEventArgs(requestCode, resultCode, data);
            base.OnActivityResult(requestCode, resultCode, data);
        }
    }
}