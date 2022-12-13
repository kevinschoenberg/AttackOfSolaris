using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioSource soundEngine;
    public float shootingDelay = 0.4f;
    public float bulletForce = 20f;
    private float _inputX;
    private float _lastTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.timeSinceLevelLoad > _lastTime + shootingDelay)
        {
            Shoot();
            _lastTime = Time.timeSinceLevelLoad;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        PlanetGravity pg = bullet.GetComponent<PlanetGravity>();
        
        soundEngine.Play();

        pg.SetPlanet(GetComponent<PlanetGravity>().planet);

        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
