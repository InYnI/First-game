using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerActions : MonoBehaviour
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
    private float _durationAnimationDeath = 2.5f;
    private float _speedOfBlinking = .2f;
    private float _horizontalMove;
    private float _verticalMove;
    private float _radiusCircle = .2f;
    private float _distanceBetweenPoints = 0.5f;
    private float _axisX = 0f;
    private float _axisY = 0f;
    private float _axisZ = 0f;
    private int _verticalMoveHash = Animator.StringToHash("VerticalMove");
    private int _horizontalMoveHash = Animator.StringToHash("HorizontalMove");
    private int _attackHash = Animator.StringToHash("Attack");
    private int _deathHash = Animator.StringToHash("Death");
    private const string _resetMaterial = nameof(ResetMaterial);
    private const string _delay = nameof(Delay);
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

        if (Vector3.Distance(transform.position, _movePoint.position) <= _distanceBetweenPoints)
        {
            _horizontalMove = Input.GetAxis("Horizontal") * _moveSpeed;
            _verticalMove = Input.GetAxis("Vertical") * _moveSpeed;

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), _axisY, _axisZ),
                        _radiusCircle, _whatStopMovement))
                {
                    _movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), _axisY, _axisZ);
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

            _animator.SetFloat(_horizontalMoveHash, Mathf.Abs(_horizontalMove));

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(_movePoint.position + new Vector3(_axisX, Input.GetAxisRaw("Vertical"), _axisZ),
                        .2f, _whatStopMovement))
                {
                    _movePoint.position += new Vector3(_axisX, Input.GetAxisRaw("Vertical"), _axisZ);
                }
            }

            _animator.SetFloat(_verticalMoveHash, Mathf.Abs(_verticalMove));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetTrigger(_attackHash);

        Collider2D[] hitBlock = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _destructibleBlocks);

        foreach(Collider2D block in hitBlock)
        {
            block.GetComponent<DestructionBlock>().Destruction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _characterSprite.material = _materialBlink;
            Invoke(_resetMaterial, _speedOfBlinking);
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
        _animator.Play(_deathHash);
        Invoke(_delay, _durationAnimationDeath);
        _moveSpeed = 0f;
    }
}
