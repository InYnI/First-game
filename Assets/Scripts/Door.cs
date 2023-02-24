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
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (Score.CountOfDiamonds >= _amountOfDiamondsRequired)
        {
            _animator.Play("OpenDoor");
        }
    }
}
