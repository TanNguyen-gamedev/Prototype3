using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _repeatWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPosition = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.x/2;    
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < _startPosition.x - _repeatWidth)
        {
            transform.position = _startPosition;
        }
    }
}
