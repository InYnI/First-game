using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image[] _lives;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        for (int i = 0; i < _lives.Length; i++)
        {
            _lives[i].enabled = i < _playerController.Health;
        }
    }

    public void ApplyDamage(int damage)
    {
        _playerController.Health -= damage;

        if (_playerController.Health <= 0)
            _playerController.Death();
    }
}
