using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PatrolIgnore : MonoBehaviour
{
    [SerializeField] public float walkingTime = 5f;

    private int index = 1;
    private float _lastTime = 0f;
    private float _currentTime = 0f;
    private float _walkingSpeed = 2f;

    private void Update()
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

}
