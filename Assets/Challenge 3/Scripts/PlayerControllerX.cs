using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;

    public float heightLimit;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public PlayerInput playerInput;

    private InputAction _jumpAction;

    void Awake()
    {
        _jumpAction = playerInput.actions["Jump"];
    }

    void OnEnable()
    {
        _jumpAction.performed += OnJump;
    }

    void OnDisable()
    {
        _jumpAction.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext callback)
    {
        if(callback.performed && !gameOver && transform.position.y < heightLimit)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        if(other.gameObject.CompareTag("Ground"))
        {
            // Bounce off the Ground so player dont sink
            playerRb.AddForce(Vector3.up * floatForce * 3f);
        }

    }

}
