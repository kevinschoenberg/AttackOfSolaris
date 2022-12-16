using System.Collections;
using UnityEngine;

public class FuelTank : MonoBehaviour
{

    [SerializeField] public GameObject player;
    private PlayerMovement PlayerMove;

    void Start()
    {
        PlayerMove = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        float time_passed = Time.deltaTime;
        float DistToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (DistToPlayer < 3f && PlayerMove.fuel < 10f)
        {
            PlayerMove.fuel += time_passed*2;
        }
    }
}