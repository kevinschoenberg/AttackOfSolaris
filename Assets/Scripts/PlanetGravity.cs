using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
public class PlanetGravity : MonoBehaviour
{
    Rigidbody2D rb;
    
    public float gravityForce = 5.0f;

    public GameObject planet;
    public PlayerMovement pm;

    private float _rotateAngle;
    private float _lastUpdate;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 v = planet.transform.position - transform.position;
        
        // Gravity
        
        if ((pm.IsUnityNull() || !pm.IsGrounded()))
        {
            rb.AddForce(v.normalized * (gravityForce * Time.deltaTime));
            _lastUpdate = Time.time;
        }
            
            

        //Rotate object to be perpendicular to the planet surface
        _rotateAngle = 90 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotateAngle);
    }

    public void SetPlanet(GameObject newPlanet)
    {
        planet = newPlanet;
    }
}