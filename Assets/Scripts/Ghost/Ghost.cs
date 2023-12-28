using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour, IWalkable
{
    private NavMeshAgent _agent;
    private Player _player;
    private AudioSource _audioSource;
    private bool isDebuff;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _player = GameManager.Player;
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        _agent.SetDestination(_player.transform.position);
    }

    public void SpeedDown()
    {
        if(!isDebuff)
        {
            isDebuff = true;
            _agent.speed = 1f;
            StartCoroutine(nameof(RestoreSpeed));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _player.gameObject.layer)
        {
            _audioSource.Play();
            GameManager.GM.CallGameOver();
        }
    }

    private IEnumerator RestoreSpeed()
    {
        yield return new WaitForSecondsRealtime(2f);
        _agent.speed = 5f;
        isDebuff = false;
    }
}
