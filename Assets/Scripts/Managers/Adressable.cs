using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Adressable : MonoBehaviour
{
    #region Fields

    public AudioClip bgCilp;
    public AudioClip coinClip;
    public AudioClip battaryClip;
    private int _coinAmount = 5;
    private int _battaryAmount = 3;
    List<AsyncOperationHandle<GameObject>> handles;

    #endregion

    #region Create Assets

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
            Vector3 coinPos = GetRandom.GetRandomPos();
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Prefabs/Items/Coin", coinPos,Quaternion.identity);
            handles.Add(handle);
        }
        for (int i = 0; i < _battaryAmount; i++)
        {
            Vector3 battaryPos = GetRandom.GetRandomPos();
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync("Assets/AddressableAssets/Prefabs/Items/Battary.prefab", battaryPos, Quaternion.identity);
            handles.Add(handle);
        }
    }
    private void CreateUI()
    {
        Addressables.InstantiateAsync("Prefabs/UI/EndPanel").Completed +=
            (handle) =>
            {
                handles.Add(handle);
                GameManager.UI.SetEndPanel();
            };

        Addressables.InstantiateAsync("Prefabs/UI/UI").Completed +=
            (handle) =>
            {
                GameManager.UI.SetUIPanel();
                handles.Add(handle);
                GameManager.GM.IsCreate = true;
            };

        
    }
    private void LoadAudio()
    {
        Addressables.LoadAssetAsync<AudioClip>("Assets/Audios/suspense.mp3").Completed +=
            (handle) =>
            {
                bgCilp = handle.Result;
                GameManager.Audio.SetBGM();
            };

        Addressables.LoadAssetAsync<AudioClip>("Assets/Audios/Coin.mp3").Completed +=
            (handle) =>
            {
                coinClip = handle.Result;
            };

        Addressables.LoadAssetAsync<AudioClip>("Assets/Audios/Charge.mp3").Completed +=
            (handle) =>
            {
                battaryClip = handle.Result;
            };
    }

    #endregion

    #region Release Assets

    public void Release()
    {
        foreach(var handle in handles)
        {
            Addressables.ReleaseInstance(handle);
        }
    }

    #endregion  
}
