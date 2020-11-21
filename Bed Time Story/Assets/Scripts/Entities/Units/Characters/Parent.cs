using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : Character
{
    protected override void InitInteractableObjectTypes()
    {
        interactableObjectTypes.Add(typeof(Cube));
    }
}
