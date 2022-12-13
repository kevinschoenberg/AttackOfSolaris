using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPosition : MonoBehaviour, ISaveable
{
    Rigidbody2D rb;
    public float posX;
    public float posY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public object SaveState()
    {
        return new SaveData()
        {
            posX = rb.transform.position.x,
            posY = rb.transform.position.y
        };

    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        Vector2 pos;
        pos.x = saveData.posX;
        pos.y = saveData.posY;
        transform.position = pos;
    }

    [Serializable]
    private struct SaveData
    {
        public float posX;
        public float posY;
        public float posZ;
    }
}
