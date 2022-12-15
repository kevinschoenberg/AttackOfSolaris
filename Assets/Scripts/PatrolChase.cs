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
    public Transform CenterPoint;

    /*Variables for animation*/
    public bool isFacingRight = true;
    private enum MovementState {idle, walking};
    private SpriteRenderer sprite;
    private Animator anim;
    public Vector2 dir;
    public Vector2 dir2;
    Vector3 pos_old;
    Vector3 pos;
    Vector3 dif_pos;
    MovementState State;

    // Start is called before the first frame update
    void Start()
    {
        CenterPoint = GameObject.Find("CenterPoint").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dir = Vector2.left * (Time.deltaTime * _walkingSpeed * index);
        if (index < 0)
        {
            State = MovementState.walking;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.walking;
            sprite.flipX = false;
        }
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
                Vector2 CenterToEnemy =  transform.position- CenterPoint.transform.position;
                Vector2 CenterToPlayer = player.transform.position- CenterPoint.transform.position;

                float EnemyAngle = Mathf.Atan2(CenterToEnemy.x, CenterToEnemy.y) * Mathf.Rad2Deg * Mathf.Sign(CenterToEnemy.x);
                float PlayerAngle = Mathf.Atan2(CenterToPlayer.x, CenterToPlayer.y) * Mathf.Rad2Deg * Mathf.Sign(CenterToPlayer.x);

                if (transform.position.x < CenterPoint.position.x)
                    EnemyAngle = 360 - EnemyAngle;
                if (player.transform.position.x < CenterPoint.position.x)
                    PlayerAngle = 360 - PlayerAngle;
                float totalangle = EnemyAngle - PlayerAngle;
                if (totalangle > 180)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                    State = MovementState.walking;
                    sprite.flipX = true;
                }
                else if (totalangle < -180)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                    State = MovementState.walking;
                    sprite.flipX = false;
                }
                else if (totalangle > 0)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                    State = MovementState.walking;
                    sprite.flipX = false;
                }
                else
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                    State = MovementState.walking;
                    sprite.flipX = true;
                }
            }
            else
            {
                transform.Translate(Vector2.zero, Space.Self);
            }
        }
        //UpdateAnimationState(); '
        anim.SetInteger("AnimState", (int)State);

    }

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    private void UpdateAnimationState()
    {
        Vector3 pos = transform.position;
        dif_pos = pos_old - pos;
        MovementState State;

        if (transform.rotation.z > 0)
        {
            if ((dif_pos.x > 0 & dif_pos.x > 0) || (dif_pos.x < 0 & dif_pos.x > 0)/* || dir.x < 0*/)
            {
                State = MovementState.walking;
                sprite.flipX = false;
            }
            else if ((dif_pos.x < 0 & dif_pos.x < 0) || (dif_pos.x > 0 & dif_pos.x < 0)/* || dir.x > 0*/)
            {
                State = MovementState.walking;
                sprite.flipX = true;
            }
            else
            {
                State = MovementState.idle;
            }
        }
        else
        {
            if ((dif_pos.x < 0 & dif_pos.x < 0) || (dif_pos.x > 0 & dif_pos.x < 0) /* || dir.x < 0*/)
            {
                State = MovementState.walking;
                sprite.flipX = false;
            }
            else if ((dif_pos.x > 0 & dif_pos.x > 0) || (dif_pos.x < 0 & dif_pos.x > 0) /* || dir.x > 0*/)
            {
                State = MovementState.walking;
                sprite.flipX = true;
            }
            else
            {
                State = MovementState.idle;
            }
        }
        pos_old = pos;
        anim.SetInteger("AnimState", (int)State);
    }

}
