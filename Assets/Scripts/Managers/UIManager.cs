using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject _endPanel;
    private TMP_Text _catchText;
    private TMP_Text _clearText;
    public void ShowEndPanel()
    {
        _endPanel = MakeEndPanel();

        SetTexts();

        _catchText?.gameObject.SetActive(false);
        _clearText?.gameObject.SetActive(false);

        GameManager.GM.OnGameClear += SetClearPanel;
        GameManager.GM.OnGameOver += SetOverPanel;

    }

    private void SetTexts()
    {
        TMP_Text[] texts = _endPanel.GetComponentsInChildren<TMP_Text>();
        _catchText = texts[0];
        _clearText = texts[1];
    }

    private GameObject MakeEndPanel()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs\\UI\\EndPanel");
        return Instantiate(go);
    }

    public void SetClearPanel()
    {
        _clearText?.gameObject.SetActive(true);
    }

    public void SetOverPanel()
    {
        _catchText.gameObject.SetActive(true);
    }
}
