using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckArea : PlayerArea
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
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
            Player.IsGrounded = false;
        }
    }
}
