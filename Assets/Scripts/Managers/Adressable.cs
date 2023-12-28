using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Adressable : MonoBehaviour
{
    public AudioClip bgCilp;
    public AudioClip coinClip;
    private int _coinAmount = 5;
    List<AsyncOperationHandle<GameObject>> handles;

    public void CreatePrefabs()
    {
        handles = new List<AsyncOperationHandle<GameObject>>();
        CreateMap();
        CreateGhost();
        CreateCoin();
        CreateUI();
        LoadAudio();
    }

    private void CreateMap()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Prefabs/Environments/Map");
        handles.Add(handle);
        handle = Addressables.InstantiateAsync("Prefabs/Environments/Lights");
        handles.Add(handle);
    }
    private void CreateGhost()
    {
        Vector3 ghostPos = new Vector3(10, 0, 20);
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Assets/AddressableAssets/Prefabs/Characters/Zombie.prefab", ghostPos,Quaternion.identity);
        handles.Add(handle);
    }
    private void CreateCoin()
    {
        for (int i = 0; i < _coinAmount; i++)
        {
            float x = GetRandom();
            float z = GetRandom();
            Vector3 coinPos = new Vector3(x, 0.5f, z);
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Prefabs/Items/Coin", coinPos,Quaternion.identity);
            handles.Add(handle);
        }
    }
    private void CreateUI()
    {
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Prefabs/UI/EndPanel");
        handles.Add(handle);
        handle = Addressables.InstantiateAsync("Prefabs/UI/UI");
        handles.Add(handle);
    }
    private void LoadAudio()
    {
        Addressables.LoadAssetAsync<AudioClip>("Assets/Audios/suspense.mp3").Completed +=
            (handle) =>
            {
                bgCilp = handle.Result;
            };

        Addressables.LoadAssetAsync<AudioClip>("Assets/Audios/Coin.mp3").Completed +=
            (handle) =>
            {
                coinClip = handle.Result;
            };
    }

    public void Release()
    {
        foreach(var handle in handles)
        {
            Addressables.Release(handle);
        }
    }
    private float GetRandom()
    {
        float _random = Random.Range(-50.0f, 50.0f);
        return _random;
    }
}
