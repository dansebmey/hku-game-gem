using System;
using UnityEngine;

public abstract class TriggerArea : MonoBehaviour
{
    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void OnTriggerExit2D(Collider2D other);
}