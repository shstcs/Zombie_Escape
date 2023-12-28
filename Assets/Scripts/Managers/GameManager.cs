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
                    DontDestroyOnLoad(go);
                    _gm = go.GetComponent<GameManager>();
                }
            }
            return _gm;
        }
    }

    private readonly UIManager _ui = new();
    private Player _player;
    private SetCoin _setCoin = new SetCoin();
    public static UIManager UI => GM._ui;
    public static Player Player => GM._player;

    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameClear;

    public int coinCount = 0;

    private void Awake()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs\\Characters\\Ghost");
        GameObject _ghost = Instantiate(go);
    }

    private void Start()
    {
        _setCoin.SetCoins();
        GM._ui.ShowEndPanel();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
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
}
