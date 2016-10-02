using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using Firebase.Xamarin.Database.Streaming;
using XOF.Droid.Constants;
using XOF.Droid.Models;
using User = Firebase.Xamarin.Auth.User;
using System.Reactive.Linq;

namespace XOF.Droid.Services
{
    public class FirebaseService
    {
        public static FirebaseService Instance { get; } = new FirebaseService();

        private FirebaseConfig FirebaseConfig => new FirebaseConfig(AppSettings.ApiKey);
        private FirebaseClient FirebaseDbCliente => new FirebaseClient(AppSettings.DbUrl);

        private User _currentUser = null;
        public User CurrentUser => _currentUser;
        protected AppPreferences AppPreferences => new AppPreferences(Android.App.Application.Context);

        public async Task CreateUserAsync(string email, string pwd)
        {
            try
            {
                var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
                var firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(email, pwd);
                _currentUser = firebaseAuthLink.User;

                SaveToken(firebaseAuthLink.FirebaseToken);
            }
            catch
            {
                // ignored
                // the service returns a ugly 404 if user or pass data does not exists
            }
        }

        public async Task LoginAsync(string email, string pwd)
        {
            try
            {
                var firebaseAuthProvider = new FirebaseAuthProvider(FirebaseConfig);
                var firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, pwd);
                _currentUser = firebaseAuthLink.User;

                SaveToken(firebaseAuthLink.FirebaseToken);
            }
            catch
            {
                // ignored
            }
        }

        public async Task PostAsync(ChatMessage message)
        {
            var item = await FirebaseDbCliente
                  .Child("chat")
                  .PostAsync(message);
        }

        public void SaveToken(string firebaseToken)
        {
            AppPreferences.SaveAccessKey(firebaseToken);
        }

        public async Task<ObservableCollection<ChatMessage>> QueryAsync()
        {
            var items = await FirebaseDbCliente
              .Child("chat")
              .OrderByKey()
              .OnceAsync<ChatMessage>();

            return new ObservableCollection<ChatMessage>(items.Select(c => c.Object).ToList());
        }
    }
}
