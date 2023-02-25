using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage;

    private Animator _animator;
    private float _verticalMove;
    private int VerticalMoveHash = Animator.StringToHash("VerticalMove");
    public float Speed;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _verticalMove = Input.GetAxis("Vertical") * Speed;

        _animator.SetFloat(VerticalMoveHash, Mathf.Abs(_verticalMove));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
        {
            healthSystem.ApplyDamage(_damage);
        }
    }
}
