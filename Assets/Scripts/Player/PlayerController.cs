using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _whatStopMovement;
    [SerializeField] private LayerMask _destructibleBlocks;

    private SpriteRenderer _characterSprite;
    private Animator _animator;
    private Material _materialBlink;
    private Material _materialDefault;
    private float _attackRange = 0.5f;
    private float _horizontalMove;
    private float _verticalMove;
    private int VerticalMoveHash = Animator.StringToHash("VerticalMove");
    private int HorizontalMoveHash = Animator.StringToHash("HorizontalMove");
    private int AttackHash = Animator.StringToHash("Attack");
    private int DeathHash = Animator.StringToHash("Death");
    public int Health = 3;

    private void Start()
    {
        _characterSprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _movePoint.parent = null;
        _materialBlink = Resources.Load("PlayerBlink", typeof(Material)) as Material;
        _materialDefault = _characterSprite.material;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _movePoint.position) <= 0.5f)
        {
            _horizontalMove = Input.GetAxis("Horizontal") * _moveSpeed;
            _verticalMove = Input.GetAxis("Vertical") * _moveSpeed;

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f),
                        .2f, _whatStopMovement))
                {
                    _movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
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
                }
            }

            _animator.SetFloat(HorizontalMoveHash, Mathf.Abs(_horizontalMove));

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f),
                        .2f, _whatStopMovement))
                {
                    _movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }

            _animator.SetFloat(VerticalMoveHash, Mathf.Abs(_verticalMove));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger(AttackHash);

        Collider2D[] hitBlock = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _destructibleBlocks);

        foreach(Collider2D block in hitBlock)
        {
            block.GetComponent<DestructionBlock>().Destruction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _characterSprite.material = _materialBlink;
            Invoke("ResetMaterial", .2f);
        }
    }

    private void ResetMaterial()
    {
        _characterSprite.material = _materialDefault;
    }

    private void Delay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Death()
    {
        _animator.Play(DeathHash);
        Invoke("Delay", 2.5f);
        _moveSpeed = 0f;
    }
}
