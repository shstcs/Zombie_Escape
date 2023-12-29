using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battary : MonoBehaviour, IEatable
{
    private AudioSource _audioSource;

    #region LifeCycle

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _audioSource.clip = GameManager.Adressable.battaryClip;
    }

    #endregion

    #region Eat, Destroy

    public void Eat()
    {
        _audioSource.Play();
        GameManager.Player.HandLight.ChargeBattary();
        gameObject.transform.position = new Vector3(transform.position.x, 100f, transform.position.z);
        StartCoroutine(nameof(LateDestroy));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == GameManager.Player.gameObject.layer)
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

