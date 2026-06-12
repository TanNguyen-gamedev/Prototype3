using UnityEngine;

public class ScreenMovememnt : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private BoolEventChannelSO _gameOverChannel;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    private void OnGameOver(bool value)
    {
        _speed = 0;
    }
}
