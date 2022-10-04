using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlanetGravity : MonoBehaviour
{
    Rigidbody2D rb;
    
    public float gravityForce = 5.0f;

    public GameObject planet;

    private float _lookAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        Vector2 v = planet.transform.position - transform.position;
        
        // Gravity
        rb.AddForce(v.normalized * gravityForce);


        _lookAngle = 90 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _lookAngle);
    }

    public void SetPlanet(GameObject newPlanet)
    {
        planet = newPlanet;
    }
}