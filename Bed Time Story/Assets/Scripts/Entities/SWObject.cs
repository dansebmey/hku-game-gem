using System;
using UnityEngine;

public abstract class SWObject : SWMonoBehaviour
{
    public Vector3 initPos;
    public Vector3 cachedPos;

    protected virtual void Start()
    {
        GameManager.RegisterObject(this);
        initPos = transform.position;
    }

    public abstract void OnLevelReset();
}