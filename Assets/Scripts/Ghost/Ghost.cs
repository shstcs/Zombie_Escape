using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour, IWalkable
{
    private NavMeshAgent _agent;
    private Player _player;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == _player.gameObject.layer)
        {
            GameManager.GM.CallGameOver();
        }
    }
}
