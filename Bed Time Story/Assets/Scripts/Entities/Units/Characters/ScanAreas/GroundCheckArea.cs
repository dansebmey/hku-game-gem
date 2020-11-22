using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckArea : ScanArea
{
    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("No longer on the ground");
            Player.IsGrounded = false;
        }
    }
}
