using SoftwareCore.Enumerators;
using SoftwareCore.Specification;
using SoftwareCore.Storage;
using SpaceInvaders.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpaceInvaders.HighScore
{

    public class HighScoreRepository : IHighScoreRepository, IVersionedIndexed<HighScoreInfo>
    {
        
        IPersistentStorage PersistentStorage { get; set; }

        List<HighScoreInfo> HighScores { get; set; }

        string StorageKey { get; set; }

        string StorageKeySecondary { get; set; }

        IHighScoreSettings Settings { get; set; }

        int version;

        public HighScoreRepository(IHighScoreSettings settings, IPersistentStorage persistentStorage) {

            Settings = settings;

            PersistentStorage = persistentStorage;
            HighScores = new List<HighScoreInfo>();

            StorageKey = nameof(HighScoreRepository);
            StorageKeySecondary = nameof(HighScoreRepository) + "_Secondary";

            UpdateRepository();
        }

        bool UpdateRepository() {

            bool retrivalSuccess = false;
            List<HighScoreInfo> retrivedData = null;

            (retrivalSuccess, retrivedData) = GetDataFromStorage(StorageKey);

            if (!retrivalSuccess) { //data might be corrupted try to grab data from secondary key
                (retrivalSuccess, retrivedData) = GetDataFromStorage(StorageKeySecondary);
            }

            if (retrivalSuccess) {

                version++;

                HighScores.Clear();

                if (retrivedData != null) {
                    HighScores.AddRange(retrivedData);
                }
            }

            return retrivalSuccess;
        }

        public (bool success, List<HighScoreInfo> retrivedData) GetDataFromStorage(string storageKey) {

            bool success = false;
            List<HighScoreInfo> deserializedData = null;

            try {
                success = PersistentStorage.TryGet(storageKey, out string serializedData);
                deserializedData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HighScoreInfo>>(serializedData);
            } catch {
                success = false;
            }

            return (success, deserializedData);
        }

        public IEnumerable<HighScoreInfo> Get(ISpecification<HighScoreInfo> specification) {

            if (specification != null) {
                return ApplySpecification(specification);
            } else {
                return this;
            }
        }

        IEnumerable<HighScoreInfo> ApplySpecification(ISpecification<HighScoreInfo> specification) {
            return SpecificationEvaluator.Evaluate(HighScores.AsQueryable(), specification);
        }

        public void Add(HighScoreInfo highScore) {

            version++;

            HighScores.Add(highScore);
            HighScores.Sort((x, y) => -1 * Comparer<int>.Default.Compare(x.value, y.value));

            if (Settings.StoredCount < HighScores.Count) {
                int toRemove = HighScores.Count - Settings.StoredCount;
                if (toRemove > 0) {
                    if (toRemove > HighScores.Count) {
                        toRemove = HighScores.Count;
                    }

                    HighScores.RemoveRange(HighScores.Count - toRemove, toRemove);
                }
            }

            string serializedData = Newtonsoft.Json.JsonConvert.SerializeObject(HighScores);

            PersistentStorage.Store(StorageKey, serializedData);
            PersistentStorage.Store(StorageKeySecondary, serializedData);
        }

        public void Clear() {

            version++;

            HighScores.Clear();

            PersistentStorage.Clear(StorageKey);
            PersistentStorage.Clear(StorageKeySecondary);
        }


        public VersionedIndexedEnumerator<HighScoreInfo> GetEnumerator() {
            return new VersionedIndexedEnumerator<HighScoreInfo>(this);
        }

        IEnumerator<HighScoreInfo> IEnumerable<HighScoreInfo>.GetEnumerator() {
            return HighScores.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return HighScores.GetEnumerator();
        }


        int IVersionedIndexed<HighScoreInfo>.GetVersion() {
            return version;
        }

        bool IVersionedIndexed<HighScoreInfo>.IsIndexValid(int index) {
            return index < HighScores.Count && index >= 0;
        }

        HighScoreInfo IVersionedIndexed<HighScoreInfo>.GetValue(int index) {
            return HighScores[index];
        }
    }

}