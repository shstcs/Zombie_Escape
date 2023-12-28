using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class HandLight : MonoBehaviour
{
    public bool isLight;
    public float LightBattary = 1000f;
    private float _fullLightBattary = 1000f;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        if(isLight)
        {
            LightBattary -= 0.1f;
        }
        if(LightBattary < 0) gameObject.SetActive(false);
    }

    public void LightControl()
    {
        if (LightBattary>0)
        {
            if (isLight)
            {
                isLight = false;
                gameObject.SetActive(false);
                Debug.Log("spotlight off");
            }
            else
            {
                isLight = true;
                gameObject.SetActive(true);
                Debug.Log("spotlight on");
            }
        }
    }

    public float GetLightBattary()
    {
        return LightBattary / _fullLightBattary;
    }
}
