using System;
using System.Runtime.CompilerServices;
using Foundation;
using Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using EUJIT.Interface;
using EUJIT.iOS.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(SecureStorage))]
namespace EUJIT.iOS.DependencyService
{


    public class SecureStorage : ISecureStorage
    {
        #region ISecureStorage Members

        //NSUserDefaults to store 
        public void StoreString(string key, string datastr)
        {
            if (key != null && datastr != null)
            {
                var storeToUserdefaults = NSUserDefaults.StandardUserDefaults;
                storeToUserdefaults.SetString(datastr, key);
                storeToUserdefaults.Synchronize();
            }
        }

        //NSUserDefaults to retrive
        public string RetrieveString(string key)
        {
            if (key != null)
            {
                string retrivedFromUserDefaults = NSUserDefaults.StandardUserDefaults.StringForKey(key);
                return retrivedFromUserDefaults;
            }
            return "";
        }

        public void StoreBool(string key, bool datastr)
        {
            if (key != null)
            {
                var storeToUserdefaults = NSUserDefaults.StandardUserDefaults;
                storeToUserdefaults.SetBool(datastr, key);
                storeToUserdefaults.Synchronize();
            }
        }


        public bool RetriveBool(string key)
        {
            if (key != null)
            {
                bool retrivedFromUserDefaults = NSUserDefaults.StandardUserDefaults.BoolForKey(key);
                return retrivedFromUserDefaults;
            }
            return false;
        }

        //NSUserDefaults to check key exists 
        public bool Contains(string key)
        {
            if (key != null)
            {
                var containsInUserDefaults = NSUserDefaults.StandardUserDefaults[key];
                if (containsInUserDefaults != null)
                {
                    return true;
                }
            }
            return false;
            //using (var existingRecord = GetExistingRecord(key))
            //{
            //    return existingRecord != null;
            //}
        }

        //NSUserDefaults to delete 
        public void Delete(string key)
        {
            if (key != null)
            {
                var deleteFromUserDefaults = NSUserDefaults.StandardUserDefaults;
                deleteFromUserDefaults.RemoveObject(key);
                deleteFromUserDefaults.Synchronize();
            }
        }


        #endregion

        #region private static methods
        private static void CheckError(SecStatusCode resultCode, [CallerMemberName] string caller = null)
        {
            if (resultCode != SecStatusCode.Success)
            {
                throw new Exception(string.Format("Failed to execute {0}. Result code: {1}", caller, resultCode));
            }
        }

        private static SecRecord GetKeyRecord(string key, NSData data = null)
        {
            var record = new SecRecord(SecKind.GenericPassword)
            {
                Service = NSBundle.MainBundle.BundleIdentifier,
                Account = key
            };

            if (data != null) record.Generic = data;

            return record;
        }

        private static SecRecord GetExistingRecord(string key)
        {
            var existingRecord = GetKeyRecord(key);

            SecStatusCode resultCode;
            SecKeyChain.QueryAsRecord(existingRecord, out resultCode);

            return resultCode == SecStatusCode.Success ? existingRecord : null;
        }
        #endregion


        /// <summary>
        /// Implements <see cref="ISecureStorage"/> for iOS using <see cref="SecKeyChain"/>.
        /// </summary>
        /// <summary>
        /// Stores data.
        /// </summary>
        /// <param name="key">Key for the data.</param>
        /// <param name="dataBytes">Data bytes to store.</param>
        public void Store(string key, byte[] dataBytes)
        {
            using (var data = NSData.FromArray(dataBytes))
            using (var newRecord = GetKeyRecord(key, data))
            {
                Delete(key);
                //CheckError(SecKeyChain.Add(newRecord));
                SecStatusCode s = SecKeyChain.Add(newRecord);
            }
        }

        /// <summary>
        /// Retrieves stored data.
        /// </summary>
        /// <param name="key">Key for the data.</param>
        /// <returns>Byte array of stored data.</returns>
        public byte[] Retrieve(string key)
        {
            SecStatusCode resultCode;

            using (var existingRecord = GetKeyRecord(key))
            using (var record = SecKeyChain.QueryAsRecord(existingRecord, out resultCode))
            {
                if (resultCode == SecStatusCode.ItemNotFound)
                    return null;

                CheckError(resultCode);
                return record.Generic.ToArray();
            }
        }

        /// <summary>
        /// Deletes data.
        /// </summary>
        /// <param name="key">Key for the data to be deleted.</param>
        /// <summary>
        /// Checks if the storage contains a key.
        /// </summary>
        /// <param name="key">The key to search.</param>
        /// <returns>True if the storage has the key, otherwise false.</returns>

        public byte[] Serialize(object credentials)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, null); // Credentials);
                return stream.ToArray();
            }
        }

    }
}