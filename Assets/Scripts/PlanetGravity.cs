using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlanetGravity : MonoBehaviour
{
    Rigidbody2D rb;

    public float gravityDistance = 250.0f;
    public float gravityForce = 5.0f;

    public GameObject planet;

    private float lookAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        // Distance to the planet
        float dist = Vector3.Distance(planet.transform.position, transform.position);
        
        Vector3 v = planet.transform.position - transform.position;
        
        // Gravity
        rb.AddForce(v.normalized * ((1.0f - dist / gravityDistance) * gravityForce));


        lookAngle = 90 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }

    public void SetPlanet(GameObject newPlanet)
    {
        planet = newPlanet;
    }
}