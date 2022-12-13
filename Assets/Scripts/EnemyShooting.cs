using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform gunRotationPoint;
    public AudioSource soundEngine;
    public float shootingDelay = 4f;
    public float bulletForce = 20f;
    private float _inputX;
    private float _lastTime = 0f;
    public Transform player;
    private Vector2 _playerPos;

    // Update is called once per frame
    void Update()
    {
        _playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 lookDirection = _playerPos - new Vector2(gunRotationPoint.position.x, gunRotationPoint.position.y);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        gunRotationPoint.rotation = Quaternion.Euler(0f, 0f, angle);


        if (Time.timeSinceLevelLoad > _lastTime + shootingDelay)
        {
            StartCoroutine(ShootCommand());
            
            _lastTime = Time.timeSinceLevelLoad;
        }
    }

    IEnumerator ShootCommand()
    {
        Shoot(0.2f);
        yield return new WaitForSeconds(0.2f);
        Shoot(0.1f);
        yield return new WaitForSeconds(0.2f);
        Shoot(0.0f);
    }
    void Shoot(float ang)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        PlanetGravity pg = bullet.GetComponent<PlanetGravity>();
        
        soundEngine.Play();

        pg.SetPlanet(GetComponent<PlanetGravity>().planet);

        Vector3 dir = firePoint.right + ang * new Vector3(firePoint.right.y, -firePoint.right.x, 0f);

        rb.AddForce(dir * bulletForce, ForceMode2D.Impulse);
    }
}
