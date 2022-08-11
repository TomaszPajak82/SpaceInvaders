using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Storage
{
    public class PlayerPrefsPersistentStorage : IPersistentStorage
    {

        public void Store(string key, string data) {
            PlayerPrefs.SetString(key, data);
            PlayerPrefs.Save();
        }

        public bool TryGet(string key, out string data) {

            if (!PlayerPrefs.HasKey(key)) {
                data = null;
                return false;
            }

            data = PlayerPrefs.GetString(key);

            return true;
        }

        public void Clear(string key) {
            PlayerPrefs.DeleteKey(key);
        }

    }

}