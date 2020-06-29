﻿using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using System.Collections.Generic;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidWTVersus.DBEntities;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Akavache;
using System.Reactive.Linq;

namespace AndroidWTVersus
{
    [Activity(Theme = "@style/AppTheme")]
    public class ComparisonTankActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        #region initialization interface values
        TextView textMessage;
        ArrayOfPlanes planes;
        #endregion

        #region initialization interface values
        Context context;
        
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Initialization View, context and Bottom Menu

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ComparisonTankLayout);
            context = Application.Context;
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            #endregion


            //string s = planes.PlanesList[120].Name;
            textMessage = FindViewById<TextView>(Resource.Id.message);
            textMessage.SetText("Before Task", TextView.BufferType.Normal);
            //  Task.Run(() => GetPlanesListFromApi());
            FromCacheAsync().ConfigureAwait(false);

        }

        async Task FromCacheAsync()
        {
            var arrayOfPlanesCached = await BlobCache.UserAccount.GetObject<ArrayOfPlanes>("cachedArrayOfPlanes");


            RunOnUiThread(() => {
                textMessage.SetText(arrayOfPlanesCached.PlanesListApi[100].Name, TextView.BufferType.Normal);
            });
        }

        //private void GetPlanesListFromApi()
        //{
        //    string URL = context.Resources.GetString(Resource.String.apiPlanesUrl);
        //    ApiXmlReaderInitial initial = new ApiXmlReaderInitial();
        //    XmlReader xReader = initial.ApiXmlReader(URL);
        //    XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfPlanes));
        //    ArrayOfPlanes arrayOfPlanes = (ArrayOfPlanes)serializer.Deserialize(xReader);
        //    planes = arrayOfPlanes;
        //    RunOnUiThread(() =>
        //    {
        //        textMessage.SetText(planes.PlanesListApi[200].Name, TextView.BufferType.Normal);
        //    });
        //}

        #region Menu navigation items

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_compare:
                    //textMessage.SetText(Resource.String.title_compare);
                    return true;
                case Resource.Id.navigation_statistics:
                    return true;
                case Resource.Id.navigation_feedback:
                    return true;
            }
            return false;
        }
        #endregion

        #region Permission

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
        #endregion
    }
}

