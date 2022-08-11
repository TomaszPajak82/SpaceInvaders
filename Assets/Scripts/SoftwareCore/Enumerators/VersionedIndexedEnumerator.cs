using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SoftwareCore.Enumerators
{
    public struct VersionedIndexedEnumerator<T> :IEnumerator<T> {

        private int version;
        private int index;
        private T current;

        private IVersionedIndexed<T> versionedIndexed;

        public VersionedIndexedEnumerator(IVersionedIndexed<T> versionedIndexed) {

            this.versionedIndexed = versionedIndexed;

            version = versionedIndexed.GetVersion();

            index = -1;
            current = default;
        }


        public T Current => current;

        public bool MoveNext() {


            if (version != versionedIndexed.GetVersion()) {
                throw new InvalidOperationException("Collection was modified !");
            }

            index++;
            if (versionedIndexed.IsIndexValid(index)) {
                current = versionedIndexed.GetValue(index);
                return true;
            }

            return false;

        }

        
        object IEnumerator.Current {
            get { 
                return current; 
            }
        }

        void IDisposable.Dispose() {
            
        }

        void IEnumerator.Reset() {
            if (version != versionedIndexed.GetVersion()) {
                throw new InvalidOperationException("Collection was modified !");
            }

            index = -1;
            current = default;
        }
        
        
    }
}