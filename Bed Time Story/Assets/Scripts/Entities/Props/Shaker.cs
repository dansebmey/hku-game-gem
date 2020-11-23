using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : SWObject
{
    private Rigidbody2D rb;
    private Collider2D[] coll2ds;
    private bool _triggered;

    protected override void Awake()
    {
        base.Awake();
        
        rb = GetComponent<Rigidbody2D>();
        coll2ds = GetComponents<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<GroundCheckArea>() != null)
        {
            GameManager.AudioManager.Play("shaker-in", 0.05f);
            _triggered = true;
        }
    }

    private void Update()
    {
        if (transform.position.y > 5.5f)
        {
            ResetObject(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_triggered)
        {
            GameManager.AudioManager.Play("shaker-out", 0.05f);
            
            rb.bodyType = RigidbodyType2D.Dynamic;
            foreach (Collider2D c2d in coll2ds)
            {
                c2d.enabled = false;
            }
        }
    }

    private void ResetObject(bool setActive)
    {
        _triggered = false;
        transform.position = initPos;
        
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero;
        foreach (Collider2D c2d in coll2ds)
        {
            c2d.enabled = true;
        }
        
        gameObject.SetActive(setActive);
    }

    public override void OnLevelReset()
    {
        ResetObject(true);
    }
}
