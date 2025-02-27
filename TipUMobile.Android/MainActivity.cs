﻿
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Refractored.XamForms.PullToRefresh.Droid;
using FFImageLoading.Forms.Platform;
using Octane.Xamarin.Forms.VideoPlayer.Android;
using Xamarin.Forms;
using Android.Content.Res;
using VideoBrek.Views.MediaDetails;
using Android.Views;

namespace VideoBrek.Droid
{
    [Activity(Label = "Video Brek", Icon = "@drawable/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            //Calls from the view that should rotate
            MessagingCenter.Subscribe<VideoPlay>(this, "allowLandScape", sender =>
            {
                OnConfigurationChanged(new Configuration() { Orientation = Orientation.Landscape });
            });

            //When the page is closed this is called
            MessagingCenter.Subscribe<VideoPlay>(this, "preventLandScape", sender =>
            {
                OnConfigurationChanged(new Configuration() { Orientation = Orientation.Portrait });
            });

            base.OnCreate(savedInstanceState);

            CachedImageRenderer.Init(enableFastRenderer: true);
            Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            PullToRefreshLayoutRenderer.Init();
            CachedImageRenderer.InitImageViewHandler();
            FormsVideoPlayer.Init();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            switch (newConfig.Orientation)
            {
                case Orientation.Landscape:
                    RequestedOrientation = ScreenOrientation.Landscape;
                    Forms.SetTitleBarVisibility(AndroidTitleBarVisibility.Never);
                    break;
                case Orientation.Portrait:
                    Forms.SetTitleBarVisibility(AndroidTitleBarVisibility.Default);
                    RequestedOrientation = ScreenOrientation.Portrait;
                    break;

            }
        }

    }
}