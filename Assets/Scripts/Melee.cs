using System.Drawing;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int damage = 20;
    public int weaponRange = 10;
    public Transform hitPoint;
    private float _lastTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.timeSinceLevelLoad > _lastTime + 0.2f)
        {
            Hit();
            _lastTime = Time.timeSinceLevelLoad;
        }
    }

    void Hit()
    {
        var thingsHit = Physics2D.OverlapCircleAll(hitPoint.position, weaponRange);

        foreach (var thing in thingsHit)
        {
            if (thing.gameObject.name.Contains("Enemy"))
            {
                thing.TryGetComponent<Health>(out var health);
                health.TakeDamage(damage);
            }
        }
    }
}