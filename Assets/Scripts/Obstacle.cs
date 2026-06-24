using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BoolEventChannelSO _gameOverChannel;
    private bool _isGameOver = false;

      private void OnEnable()
    {
        if(_gameOverChannel)
        {
            _gameOverChannel.OnEventRaised += OnGameOver;
        }
    }

    private void OnDisable()
    {
        if(_gameOverChannel)
        {
            _gameOverChannel.OnEventRaised -= OnGameOver;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isGameOver)
        {
            _rb.linearVelocity = Vector3.zero;
            return;
        }
        _rb.AddForce(Vector3.left * _speed);
    }

    private void OnGameOver(bool gameOver)
    {
        _isGameOver = gameOver;
    }

}
