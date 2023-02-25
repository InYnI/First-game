using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _diamond;
    [SerializeField] private int _countOfDiamonds;

    private Animator _animator;
    private int _openHash = Animator.StringToHash("Open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            _animator.Play(_openHash);
            CollectDiamond(_diamond);
        }
    }

    private void CollectDiamond(SpriteRenderer Diamond)
    {
        if (Diamond.TryGetComponent<RedDiamond>(out RedDiamond redDiamond))
        {
            Score.CountOfRedDiamonds += _countOfDiamonds;
        }
        else if (Diamond.TryGetComponent<Diamond>(out Diamond diamond))
        {
            Score.CountOfDiamonds += _countOfDiamonds;
        }

        Destroy(this);
    }
}
