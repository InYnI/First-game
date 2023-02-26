using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image[] _lives;

    private PlayerActions _playerActions;

    private void Start()
    {
        _playerActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        for (int i = 0; i < _lives.Length; i++)
        {
            _lives[i].enabled = i < _playerActions.Health;
        }
    }

    public void ApplyDamage(int damage)
    {
        _playerActions.Health -= damage;

        if (_playerActions.Health <= 0)
            _playerActions.Death();
    }
}
