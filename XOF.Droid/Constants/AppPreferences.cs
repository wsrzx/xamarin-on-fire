
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XOF.Droid
{

    public class AppPreferences
    {
        private ISharedPreferences _sharedPreferences;
        private ISharedPreferencesEditor _sharedPreferencesEditor;
        private Context _context;
        private readonly string PREFERENCE_ACCESS_KEY = "XOF_DEMO";

        public AppPreferences(Context context)
        {
            _context = context;
            _sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(_context);
            _sharedPreferencesEditor = _sharedPreferences.Edit();
        }

        public void SaveAccessKey(string key)
        {
            _sharedPreferencesEditor.PutString(PREFERENCE_ACCESS_KEY, key);
            _sharedPreferencesEditor.Commit();
        }

        public string GetAccessKey()
        {
            return _sharedPreferences.GetString(PREFERENCE_ACCESS_KEY, "");
        }
    }
}
