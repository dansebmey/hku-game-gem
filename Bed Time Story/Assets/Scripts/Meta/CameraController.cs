using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SWMonoBehaviour
{
    [SerializeField] public Player player;
    
    private Camera _camera;
    
    private SpriteRenderer _overlayRenderer;

    protected override void Awake()
    {
        base.Awake();
        
        _camera = GetComponent<Camera>();
        _overlayRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    internal void FadeIn(float fadeDuration)
    {
        StartCoroutine(_FadeIn(fadeDuration));
    }
    
    private IEnumerator _FadeIn(float fadeDuration)
    {
        float r = _overlayRenderer.color.r;
        float g = _overlayRenderer.color.g;
        float b = _overlayRenderer.color.b;
        float alpha = 1;

        while (alpha > 0)
        {
            alpha -= 0.01f / fadeDuration;
            _overlayRenderer.color = new Color(r, g, b, alpha);
            
            yield return new WaitForSeconds(0.01f);
        }

        GameManager.gameState = GameManager.GameState.PLAYING;
    }

    internal void FadeOut(float fadeDuration, Action onCompleteAction)
    {
        StartCoroutine(_FadeOut(fadeDuration, onCompleteAction));
    }

    private IEnumerator _FadeOut(float fadeDuration, Action onCompleteAction)
    {
        GameManager.gameState = GameManager.GameState.TRANSITIONING;
        
        float r = _overlayRenderer.color.r;
        float g = _overlayRenderer.color.g;
        float b = _overlayRenderer.color.b;
        float alpha = 0;

        while (alpha < 1)
        {
            alpha += 0.01f / fadeDuration;
            _overlayRenderer.color = new Color(r, g, b, alpha);
            
            yield return new WaitForSeconds(0.01f);
        }
        
        onCompleteAction.Invoke();
        FadeIn(fadeDuration);
    }

    private void Update()
    {
        Vector3 camPos = transform.position;
        transform.position = new Vector3(player.transform.position.x, camPos.y, camPos.z);
    }
}
