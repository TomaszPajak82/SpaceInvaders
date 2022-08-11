using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoftwareCore.Storage
{
    public interface IPersistentStorage
    {
        void Store(string key, string data);

        bool TryGet(string key, out string data);

        void Clear(string key);
    }
}
