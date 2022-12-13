using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    //Sword animation:
    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerMovement pm;

    public Transform sword;
    private bool oldDir = true;

    void Start()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        float offset = 0.75f;
        if (pm.isFacingRight && !oldDir)
        {
            anim.SetBool("FacingLeft", false);
            sword.Translate(offset, 0f, 0f);
        
        }
        else if (!pm.isFacingRight && oldDir)
        {
            anim.SetBool("FacingLeft", true);
            sword.Translate(-offset, 0f, 0f);
        }
        oldDir = pm.isFacingRight;

        if (Input.GetButton("Fire1"))
        {
            if (pm.isFacingRight)
            {
                anim.SetTrigger("SwordSwing");
            }
            else if (!pm.isFacingRight)
            {
                anim.SetTrigger("SwordSwingLeft");
            }
        }
        else
        {
            anim.ResetTrigger("SwordSwing");
            anim.ResetTrigger("SwordSwingLeft");
        }
    }
}
