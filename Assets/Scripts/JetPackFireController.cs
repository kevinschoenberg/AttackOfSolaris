using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackFireController : MonoBehaviour
{
    //Sword animation:
    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerMovement pm;

    public Transform fire;
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
            fire.Translate(-offset, 0f, 0f);

        }
        else if (!pm.isFacingRight && oldDir)
        {
            fire.Translate(offset, 0f, 0f);
        }
        oldDir = pm.isFacingRight;
    }
}