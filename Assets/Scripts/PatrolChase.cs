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

    /*Variables for animation*/
    public bool isFacingRight = true;
    private enum MovementState {idle, walking};
    private SpriteRenderer sprite;
    private Animator anim;
    public Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dir = Vector2.left * (Time.deltaTime * _walkingSpeed * index);
        if (Vector2.Distance(transform.position, player.transform.position) > Enemy.MaxDist)
        {
            if (_lastTime == 0f)
            {
                _lastTime = Time.time;
                _currentTime = Time.time;
            }
            if (_currentTime - _lastTime < walkingTime)
            {
                transform.Translate(dir, Space.Self);
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
            //Find the distance to the player
            _distanceToPLayer = player.transform.position - transform.position;
            //Walk towards the player
            if (Vector2.Distance(transform.position, player.transform.position) <= Enemy.MaxDist)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
            }
            else
            {
                transform.Translate(Vector2.zero, Space.Self);
            }
            
        }
        UpdateAnimationState(); 

    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    private void UpdateAnimationState()
    {
        MovementState State;
        if (dir.x < 0)
        {
            State = MovementState.walking;
            sprite.flipX = false;
        }
        else if (dir.x > 0)
        {
            State = MovementState.walking;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.idle;
        }
        anim.SetInteger("AnimState", (int)State);
    }

}
