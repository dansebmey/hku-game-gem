using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : SWMonoBehaviour
{
    [SerializeField] public Player player;
    
    private Camera _camera;
    private Canvas _pauseCanvas;
    
    private SpriteRenderer _overlayRenderer;

    [SerializeField] private Color transitionColour;
    [SerializeField] private Color pauseScreenColour;

    protected override void Awake()
    {
        base.Awake();
        
        _camera = GetComponent<Camera>();
        _overlayRenderer = GetComponentInChildren<SpriteRenderer>();
        _pauseCanvas = GetComponentInChildren<Canvas>(true);
    }

    internal void FadeIn(float fadeDuration)
    {
        StartCoroutine(_FadeIn(fadeDuration));
    }
    
    private IEnumerator _FadeIn(float fadeDuration)
    {
        float r = transitionColour.r;
        float g = transitionColour.g;
        float b = transitionColour.b;
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
        
        float r = transitionColour.r;
        float g = transitionColour.g;
        float b = transitionColour.b;
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

    internal void TogglePauseOverlay(bool doShow)
    {
        _overlayRenderer.color = doShow ? pauseScreenColour : Color.clear;
        _pauseCanvas.gameObject.SetActive(doShow);
    }

    private void Update()
    {
        Vector3 camPos = transform.position;
        transform.position = new Vector3(player.transform.position.x, camPos.y, camPos.z);
    }
}
