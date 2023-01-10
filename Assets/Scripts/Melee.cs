using System;
using TMPro;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public int damage = 2;
    public int weaponRange = 1;
    public Transform hitPoint;
    private float lastTime = 0f;
    private PlayerMovement _pm;
    private bool oldDir = true;
    public AudioSource soundEngine;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.timeSinceLevelLoad > lastTime + 0.2f)
        {
            Hit();
            soundEngine.Play();
            lastTime = Time.timeSinceLevelLoad;
        }
        _pm = GetComponentInParent<PlayerMovement>();
        if (_pm.isFacingRight && !oldDir)
        {
            hitPoint.Translate(2f, 0f, 0f);
        }
        else if (!_pm.isFacingRight && oldDir)
        {
            hitPoint.Translate(-2f, 0f, 0f);
        }
        oldDir = _pm.isFacingRight;
    }

    void Hit()
    {
        var thingsHit = Physics2D.OverlapCircleAll(hitPoint.position, weaponRange);

        foreach (var thing in thingsHit)
        {
            if (thing.gameObject.name.Contains("Enemy")||thing.gameObject.name.Contains("Tech"))
            {
                thing.TryGetComponent<Health>(out var health);
                health.TakeDamage(damage);
            }
        }
    }
}