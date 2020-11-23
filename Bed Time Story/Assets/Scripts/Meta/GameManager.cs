using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    internal enum GameState { PLAYING, TRANSITIONING }

    internal GameState gameState = GameState.TRANSITIONING;
    
    private AudioManager _am;
    internal AudioManager AudioManager
    {
        get => AudioManager.GetInstance();
    }

    [SerializeField] private CameraController cameraController;
    internal CameraController CameraController => cameraController;

    private List<SWObject> _allObjects;

    private void Awake()
    {
        _am = GetComponent<AudioManager>();
        _allObjects = new List<SWObject>();
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
}