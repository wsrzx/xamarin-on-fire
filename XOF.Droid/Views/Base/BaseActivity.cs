using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XOF.Droid.Services;

namespace XOF.Droid.Views.Base
{
    public class BaseActivity : Activity
    {


        protected readonly FirebaseService _firebaseService = FirebaseService.Instance;

        protected void ShowToast(string message)
        {
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

    }
}