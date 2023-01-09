using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int _index;

    private Transform _player;

    private void Awake()
    {
        _player = GameObject.Find("Player").transform;

        if(DataContainer.spawnpointIndex == _index)
            _player.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
            DataContainer.spawnpointIndex = _index;
    }
}
