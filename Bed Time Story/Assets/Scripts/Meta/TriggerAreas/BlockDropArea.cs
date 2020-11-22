using UnityEngine;

public class BlockDropArea : TriggerArea
{
    public SpriteRenderer outlineSpriteRenderer;
    internal bool containsBlock;
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<ToyBlock>();
        if (prop != null)
        {
            containsBlock = true;
            outlineSpriteRenderer.gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        var prop = other.gameObject.GetComponent<ToyBlock>();
        if (prop != null)
        {
            containsBlock = false;
            outlineSpriteRenderer.gameObject.SetActive(true);
        }
    }
}