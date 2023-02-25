using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DestructionBlock : MonoBehaviour
{
    private Animator _animator;
    private int _destructionHash = Animator.StringToHash("Destruction");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Destruction()
    {
        _animator.Play(_destructionHash);
        Destroy(gameObject, 1f);
    }
}
