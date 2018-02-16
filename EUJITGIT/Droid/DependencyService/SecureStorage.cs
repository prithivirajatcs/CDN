using System;
using Android.Content;
using Android.Preferences;
using EUJIT.Droid.DependencyService;
using EUJIT.Interface;
using EUJIT.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SecureStorage))]
namespace EUJIT.Droid.DependencyService
{
    public class SecureStorage : ISecureStorage
    {
        public bool Contains(string key)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            return pref.Contains(key);
        }

        public void Delete(string key)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            ISharedPreferencesEditor editor = pref.Edit();
            editor.Remove(key).Commit();
        }

        public byte[] Retrieve(string key)
        {
            return null;
        }

        public string RetrieveString(string key)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            return pref.GetString(key, "");
        }

        public bool RetriveBool(string key)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            return pref.GetBoolean(key, false);

        }

        public void Store(string key, byte[] dataBytes)
        {

        }

        public void StoreBool(string key, bool datastr)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            ISharedPreferencesEditor editor = pref.Edit();
            editor.PutBoolean(key, datastr).Commit();
        }

        public void StoreString(string key, string datastr)
        {
            var pref = PreferenceManager.GetDefaultSharedPreferences((Context)MainActivity.CurrentActivity);
            ISharedPreferencesEditor editor = pref.Edit();
            editor.PutString(key, datastr).Commit();
        }
    }
}