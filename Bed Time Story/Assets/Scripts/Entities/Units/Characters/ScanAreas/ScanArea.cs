using UnityEngine;

public class ScanArea : MonoBehaviour
{
    protected Player Player;

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
    }
}