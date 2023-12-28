using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCoin : MonoBehaviour
{
    private GameObject _coin;

    public void SetCoins()
    {
        _coin = Resources.Load<GameObject>("Prefabs\\Items\\Coin");
        for (int i = 0; i<5; i++)
        {
            float x = GetRandom();
            float z = GetRandom();
            Vector3 coinPos = new Vector3(x, 0.5f, z);
            GameObject coin = Instantiate(_coin);
            coin.transform.position = coinPos;
        }
    }

    private float GetRandom()
    {
        float _random = Random.Range(-50.0f, 50.0f);
        return _random;
    }
}
