using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    
    internal enum GameState { PLAYING, TRANSITIONING, PAUSED
    }

    internal GameState gameState = GameState.TRANSITIONING;
    
    private AudioManager _am;
    internal AudioManager AudioManager
    {
        get => AudioManager.GetInstance();
    }

    [SerializeField] private CameraController cameraController;
    internal CameraController CameraController => cameraController;

    private List<SWObject> _allObjects;
    private List<Entity> _entities;

    private void Awake()
    {
        _am = GetComponent<AudioManager>();
        _allObjects = new List<SWObject>();
        _entities = new List<Entity>();
    }

    private void Start()
    {
        AudioManager.Play("Soundtrack");
        AudioManager.FadeVolume("Soundtrack", 0, 0.5f, 1.5f);
        CameraController.FadeIn(1);
    }

    public void ResetLevel()
    {
        CameraController.FadeOut(1, ResetAllObjectPositions);
    }

    private void ResetAllObjectPositions()
    {
        foreach (SWObject obj in _allObjects)
        {
            obj.OnLevelReset();
            obj.transform.position = obj.initPos;
        }
    }

    public void RegisterObject(SWObject obj)
    {
        _allObjects.Add(obj);
    }

    public void RegisterEntity(Entity entity)
    {
        _entities.Add(entity);
    }

    public void SetCheckpoint()
    {
        foreach (SWObject obj in _allObjects)
        {
            obj.cachedPos = obj.transform.position;

            Rigidbody2D rb = obj.transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                obj.cachedVelocity = rb.velocity;
            }
        }
    }

    public void ResetToCheckpoint()
    {
        CameraController.FadeOut(1, _ResetToCheckpoint);
    }

    private void _ResetToCheckpoint()
    {
        foreach (SWObject obj in _allObjects)
        {
            obj.OnLevelReset();
            obj.transform.position = obj.cachedPos;

            Rigidbody2D rb = obj.transform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = obj.cachedVelocity;
            }
        }
    }

    public void TogglePause(bool doPause)
    {
        CameraController.TogglePauseOverlay(doPause);
        AudioManager.TogglePause("Soundtrack", doPause);
        gameState = doPause ? GameState.PAUSED : GameState.PLAYING;
        // Time.timeScale = doPause ? 0 : 1;
        foreach (Entity entity in _entities)
        {
            entity.TogglePause(doPause);
        }
    }
    
    private List<Type> tooltipsShownFor = new List<Type>();
    public void RegisterTooltipShown(Type type)
    {
        tooltipsShownFor.Add(type);
    }

    public bool IsTooltipShownFor(Type type)
    {
        return tooltipsShownFor.Contains(type);
    }
}