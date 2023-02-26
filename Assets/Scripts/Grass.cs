using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Grass : MonoBehaviour
{
    private Animator _animator;
    private int _grassFallHash = Animator.StringToHash("GrassFall");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerActions>(out PlayerActions playerActions))
        {
            _animator.Play(_grassFallHash);
            Destroy(gameObject, 1f);
        }
    }
}
