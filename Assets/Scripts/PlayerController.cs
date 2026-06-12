using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravityModifier = 3f;
    [SerializeField] private BoolEventChannelSO _gameOverChannel;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _explosionSmoke;
    [SerializeField] private ParticleSystem _dirtParticle;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _crashSound;
    private AudioSource _audioSource;
    private InputAction _jumpAction;
    private Rigidbody _rb;
    private float _speed = 1f;
    private bool _isGround = true;

    private bool _isGameOver = false;
    

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _jumpAction = _playerInput.actions["Jump"];
        _animator.SetFloat("Speed_f", _speed);
    }

    private void OnEnable()
    {
        _jumpAction.performed += OnJump;
    }

    private void OnDisable()
    {
        _jumpAction.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext callback)
    {
        if(callback.performed && _isGround && !_isGameOver)
        {
            _animator.SetTrigger("Jump_trig");
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _audioSource.PlayOneShot(_jumpSound, 1f);
            _isGround = false;
            _dirtParticle.Stop();
        }
    }

    private void FixedUpdate()
    {
        // When Rigidbody fall, increase gravity
        if(_rb.linearVelocity.y < 0)
        {
            _rb.AddForce(Physics.gravity * _gravityModifier, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _isGround = true;
            if(_isGameOver)
            {
                return;
            }
            _dirtParticle.Play();     
        }
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            _dirtParticle.Stop();
            _gameOverChannel?.RaiseEvent(true);
            _animator.SetBool("Death_b",true);
            _animator.SetInteger("DeathType_int",1);
            _isGameOver = true;
            _explosionSmoke.Play();
            _audioSource.PlayOneShot(_crashSound, 1f);
            Debug.Log("Game Over");
        }
         
    }


}
