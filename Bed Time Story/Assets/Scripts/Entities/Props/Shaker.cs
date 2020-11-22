using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D[] coll2ds;
    private bool _triggered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll2ds = GetComponents<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponentInChildren<GroundCheckArea>() != null)
        {
            _triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_triggered)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            foreach (Collider2D c2d in coll2ds)
            {
                c2d.enabled = false;
            }
        }
    }
}
