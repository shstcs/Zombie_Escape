using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public void SetBGM()
    {
        _audioSource = GameManager.GM.GetComponent<AudioSource>();
        _audioSource.clip = GameManager.Adressable.bgCilp;
        _audioSource.loop = true;
        _audioSource.Play();
    }
    public void SetVolume()
    {
        float distance = GameManager.Player.GetDistanceWithGhost();
        float minusVolume = distance - 3 > 0 ? (distance - 10) / 3 : 0;

        _audioSource.volume = 1-minusVolume;
    }
}
