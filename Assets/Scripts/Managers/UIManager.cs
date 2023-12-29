using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Fields

    private GameObject _endPanel;
    private TMP_Text _catchText;
    private TMP_Text _clearText;
    private TMP_Text _returnText;

    private GameObject _uiPanel;
    private Image _lightBattary;
    private Image[] _coins = new Image[5];

    #endregion

    #region Find Canvas 

    public void SetEndPanel()
    {
        _endPanel = GameObject.Find("EndPanel(Clone)");
        if( _endPanel != null)
        {
            SetTexts();

            _catchText?.gameObject.SetActive(false);
            _clearText?.gameObject.SetActive(false);
            _returnText?.gameObject.SetActive(false);

            GameManager.GM.OnGameClear += SetClearPanel;
            GameManager.GM.OnGameOver += SetOverPanel;
        }
    }
    public void SetUIPanel()
    {
        _uiPanel = GameObject.Find("UI(Clone)");
        if(_uiPanel != null)
        {
            _lightBattary = _uiPanel.GetComponentInChildren<Image>();
            GameObject go = _uiPanel.transform.Find("CoinCollectPanel").gameObject;
            _coins = go.GetComponentsInChildren<Image>();
        }
    }

    #endregion

    #region Change UI details

    public void SetLightBattary()
    {
        if(_lightBattary != null)
        {
            _lightBattary.fillAmount = GameManager.Player.HandLight.GetLightBattary();
        }
        
    }
    private void SetTexts()
    {
        TMP_Text[] texts = _endPanel.GetComponentsInChildren<TMP_Text>();
        if(texts != null && texts.Length > 0)
        {
            _catchText = texts[0];
            _clearText = texts[1];
            _returnText = texts[2];
        }
    }
    public void SetClearPanel()
    {
        _clearText?.gameObject.SetActive(true);
        _returnText?.gameObject.SetActive(true);
    }
    public void SetOverPanel()
    {
        _catchText?.gameObject.SetActive(true);
        _returnText?.gameObject.SetActive(true);
    }
    public void SetCoin(int coinCount)
    {
        _coins[coinCount].color = new Color(1, 1, 1);
    }

    #endregion
}
