using UnityEngine;

public class Cube : Prop
{
    public override void OnInteract(Character actor)
    {
        transform.position = new Vector3(transform.position.x + (actor.transform.localScale.x), transform.position.y);
    }
}