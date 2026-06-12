using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private GameObject[] _obstaclesPrefabs;
    [SerializeField] private BoolEventChannelSO _gameOverChannel;
    private float _spawnTimer;
    private bool _shouldSpawn = true;    

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

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer > _spawnInterval)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if(_shouldSpawn)
        {
            GameObject obstacle = Instantiate(_obstaclesPrefabs[Random.Range(0,_obstaclesPrefabs.Length)],
            transform.position,
            transform.rotation);
            _spawnTimer = 0f;

            Destroy(obstacle, 5f);
        }
    }

    private void OnGameOver(bool stopSpawn)
    {
        _shouldSpawn = !stopSpawn;
    }
}
