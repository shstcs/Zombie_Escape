using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IEatable
{
    private GameManager _gm;

    private void Awake()
    {
        _gm = GameManager.GM;
    }

    public void Eat()
    {
        _gm.coinCount++;
        if(_gm.coinCount >= 5)
        {
            _gm.CallGameClear();
        }
        Destroy(gameObject);
        Debug.Log($"CoinCount : {_gm.coinCount}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == GameManager.Player.gameObject.layer)
        {
            Eat();
        }
    }
}
