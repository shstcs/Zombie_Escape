using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IEatable
{
    private GameManager _gm;
    private AudioSource _audioSource;

    #region LifeCycle

    private void Awake()
    {
        _gm = GameManager.GM;
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _audioSource.clip = GameManager.Adressable.coinClip;
    }

    #endregion

    #region Eat,Destroy

    public void Eat()
    {
        _gm.coinCount++;
        if(_gm.coinCount >= 5)
        {
            _gm.CallGameClear();
        }
        GameManager.UI.SetCoin(_gm.coinCount-1);
        _audioSource.Play();
        gameObject.transform.position = new Vector3(transform.position.x, 100f, transform.position.z);
        StartCoroutine(nameof(LateDestroy));
        Debug.Log($"CoinCount : {_gm.coinCount}");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == GameManager.Player.gameObject.layer)
        {
            Eat();
        }
    }
    private IEnumerator LateDestroy()
    {
        yield return new WaitForSecondsRealtime(3);

        Destroy(gameObject);
    }

    #endregion
}
