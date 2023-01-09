using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask whatStopMovement;

    private SpriteRenderer _characterSprite;
    private Animator _animator;
    private Vector3 _input;

    private void Start()
    {
        _characterSprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        movePoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f),
                        .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }

                if (Input.GetAxis("Horizontal") != 0)
                {
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        _characterSprite.flipX = true;
                    }
                    else if (Input.GetAxis("Horizontal") > 0)
                    {
                        _characterSprite.flipX = false;
                    }

                    _animator.SetInteger("State", 1);
                }
                else
                {
                    _animator.SetInteger("State", 0);
                }

            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f),
                        .2f, whatStopMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }

                if (Input.GetAxis("Vertical") != 1f)
                {
                    if (Input.GetAxis("Horizontal") < 1f)
                    {
                        _characterSprite.flipY = false;
                    }
                    else if (Input.GetAxis("Horizontal") > 1f)
                    {
                        _characterSprite.flipY = true;
                    }

                    _animator.SetInteger("State", 2);
                }
                else
                {
                    _animator.SetInteger("State", 3);
                }
            }
        }
    }
}
