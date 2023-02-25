using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    [SerializeField] public int _amountOfDiamondsRequired;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Open();
    }

    private void Open()
    {
        if (Score.CountOfDiamonds >= _amountOfDiamondsRequired)
        {
            _animator.Play("OpenDoor");
        }
    }
}
