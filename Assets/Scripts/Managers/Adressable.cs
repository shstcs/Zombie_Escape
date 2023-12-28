using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Adressable : MonoBehaviour
{
    public AssetReference SpawnablePrefab;
    
    public void CreatePrefab()
    {
        List<AsyncOperationHandle<GameObject>> handles = new List<AsyncOperationHandle<GameObject>>();

        AsyncOperationHandle<GameObject> handle = SpawnablePrefab.InstantiateAsync();
        handles.Add(handle);
    }
}
