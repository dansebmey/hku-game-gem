using System;
using UnityEngine;

public abstract class SWObject : SWMonoBehaviour
{
    internal Vector3 initPos;
    internal Vector3 cachedPos;
    internal Vector3 cachedVelocity;

    protected virtual void Start()
    {
        GameManager.RegisterObject(this);
        initPos = transform.position;
        cachedPos = transform.position;
        cachedVelocity = Vector3.zero;
    }

    public abstract void OnLevelReset();
}