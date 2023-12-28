using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gm;
    private static bool _initialized;
    public static GameManager GM
    {
        get
        {
            if (!_initialized)
            {
                _initialized = true;
                GameObject go = GameObject.Find("@GameManager");
                if (go == null)
                {
                    go = new GameObject() { name = @"GameManager" };
                    go.AddComponent<GameManager>();
                    go.AddComponent<AudioSource>();
                    DontDestroyOnLoad(go);
                    _gm = go.GetComponent<GameManager>();
                }
            }
            return _gm;
        }
    }

    private readonly UIManager _ui = new UIManager();
    private Player _player;
    private readonly AudioManager _audioManager = new AudioManager();
    private readonly Adressable _adressable = new Adressable();

    public static UIManager UI => GM._ui;
    public static Player Player => GM._player;
    public static AudioManager Audio => GM._audioManager;
    public static Adressable Adressable => GM._adressable;

    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameClear;
    public bool IsOver { get; set; } = false;

    public int coinCount = 0;
    private int CreateCount;

    private void Awake()
    {
        OnGameClear += () => IsOver = true;
        OnGameOver += () => IsOver = true;
    }
    private void Update()
    {
        if (!IsOver)
        {
            if (CreateCount < 4) CreateCount++;
            else if (CreateCount == 4)
            {
                _ui.SetEndPanel();
                _ui.SetUIPanel();
                CreateCount++;
                _audioManager.SetBGM();
            }
            else
            {
                _ui.SetLightBattary();
                _audioManager.SetVolume();
            }
        }
    }

    public void GameInit()
    {
        _adressable.CreatePrefabs();
        CreateCount = 0;
        IsOver = false;
    }

    public void CallGameStart()
    {
        OnGameStart?.Invoke();
    }
    public void CallGameOver()
    {
        OnGameOver?.Invoke();
        Time.timeScale = 0;
    }
    public void CallGameClear()
    {
        OnGameClear?.Invoke();
        Time.timeScale = 0;
    }
    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
