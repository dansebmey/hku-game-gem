using UnityEngine;

public class MagneticSnapArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<ToyBlock>();
        if (prop != null)
        {
            
        }
    }
}