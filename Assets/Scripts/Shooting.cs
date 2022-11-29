using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private bool _isFacingRight = true;
    private float _inputX;
    private float _lastTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.timeSinceLevelLoad > _lastTime + 0.2f)
        {
            PlayerMovement pm = GetComponent<PlayerMovement>();
            _isFacingRight = pm.isFacingRight;
            
            Shoot();
            _lastTime = Time.timeSinceLevelLoad;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        PlanetGravity pg = bullet.GetComponent<PlanetGravity>();

        pg.SetPlanet(GetComponent<PlanetGravity>().planet);

        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
       
        
    }
}
