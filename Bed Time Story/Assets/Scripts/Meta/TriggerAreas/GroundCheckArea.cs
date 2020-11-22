﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckArea : PlayerArea
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Landed on the ground");
            Player.IsGrounded = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Player.IsGrounded = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("No longer on the ground");
            Player.IsGrounded = false;
        }
    }
}
