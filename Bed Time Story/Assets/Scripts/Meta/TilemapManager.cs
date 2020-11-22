using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapManager : MonoBehaviour
{
    private void Awake()
    {
        Transform mirrorParent = GetComponentInChildren<Transform>(); 
        foreach (Transform tile in GetComponentsInChildren<Transform>())
        {
            if (tile != mirrorParent)
            {
                Transform mirroredTile = Instantiate(tile, new Vector3(tile.position.x, tile.position.y * -1), Quaternion.identity);
                mirroredTile.parent = mirrorParent;
                mirroredTile.rotation = Quaternion.Euler(0, 0, 180);
                
                var localScale = mirroredTile.localScale;
                localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
                mirroredTile.localScale = localScale;
            }
        }
    }
}
