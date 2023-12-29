using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    private static bool _isInitialized;
    public bool IsOver { get; private set; } = false;
    public bool IsCreate { get; set; } = false;

    private static GameManager _gm;
    public static GameManager GM
    {
        get
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
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

    public event Action OnGameOver;
    public event Action OnGameClear;

    public int coinCount = 0;

    #endregion

    #region LifeCycle

    private void Awake()
    {
        OnGameClear += () => IsOver = true;
        OnGameOver += () => IsOver = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) _adressable.Release();
        if (Input.GetKeyDown(KeyCode.P)) _adressable.CreatePrefabs();
        if (Input.GetKeyDown(KeyCode.C)) ConnectUI();
        if (!IsOver&&IsCreate)
        {
            _ui.SetLightBattary();
            _audioManager.SetVolume();
            _player.HandLight.LightRaycast();
        }
    }

    #endregion

    #region Create, Connect, Release

    public void GameInit()
    {
        _adressable.CreatePrefabs();
        IsOver = false;
    }
    public void ConnectUI()
    {
        _ui.SetEndPanel();
        _ui.SetUIPanel();
    }
    public void EndGame()
    {
        Time.timeScale = 1;
        _adressable.Release();
        Cursor.lockState = CursorLockMode.None;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("StartScene");
        IsCreate = false;
    }

    #endregion

    #region CallBackFuntion

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

    #endregion

    public void SetPlayer(Player player)
    {
        _player = player;
    }
}
