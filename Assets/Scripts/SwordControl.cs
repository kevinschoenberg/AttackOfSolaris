using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    //Sword animation:
    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerMovement pm;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (pm.isFacingRight)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
        if (Input.GetButton("Fire1"))
        {
            anim.SetTrigger("SwordSwing");
            print(pm.isFacingRight);
        }
        else
        {
            anim.ResetTrigger("SwordSwing");
        }
    }
}
