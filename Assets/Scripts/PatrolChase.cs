using UnityEngine;

public class PatrolChase : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public float speed = 4.0f;
    [SerializeField] public float walkingTime = 5f;
    private int index = 1;
    private float _lastTime = 0f;
    private float _currentTime = 0f;
    private Vector2 _distanceToPLayer;    
    private float _walkingSpeed = 2f;
    private int _isPlayerOnRightSide = 0;


    private void Update()
    {
    if (Vector2.Distance(transform.position, player.transform.position) > Enemy.MaxDist)
        {
            if (_lastTime == 0f)
            {
                _lastTime = Time.time;
                _currentTime = Time.time;
            }
            if (_currentTime - _lastTime < walkingTime)
            {
                transform.Translate(Vector2.left * (Time.deltaTime * _walkingSpeed * index), Space.Self);
                _currentTime = Time.time;
            }
            else
            {
                index *= -1;
                _lastTime = Time.time;
            }
        }
    else
        {
            //Find the player
            // use player location to get a direction (right or left)
            _distanceToPLayer = player.transform.position - transform.position;
            if (_distanceToPLayer[0] >= 0)
            {
                _isPlayerOnRightSide = -1;
            }
            else
            {
                _isPlayerOnRightSide = 1;
            }
            // Use that to change "_isPlayerOnRightSide" and walk in that direction
            //Walk towards the player
            if (Vector2.Distance(transform.position, player.transform.position) <= Enemy.MaxDist)
            {
                transform.Translate(Vector2.left * (Time.deltaTime * speed * _isPlayerOnRightSide), Space.Self);
            }
            else
            {
                transform.Translate(Vector2.zero, Space.Self);
            }
        }
    }

}
