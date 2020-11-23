using UnityEngine;

public class SWMonoBehaviour : MonoBehaviour
{
    protected GameManager GameManager;
    
    protected virtual void Awake()
    {
        GameManager = FindObjectOfType<GameManager>();
        Debug.Log("Game manager = " + GameManager);
    }
}