using UnityEngine;

public class ScanArea : MonoBehaviour
{
    protected Character character;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }
}