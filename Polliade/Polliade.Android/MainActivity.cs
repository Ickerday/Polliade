﻿using Android.App;
using Android.Content.PM;
using Android.OS;
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
    }
}