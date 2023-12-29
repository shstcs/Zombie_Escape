using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class HandLight : MonoBehaviour
{
    #region Fields

    private bool isLight;
    private float _lightBattary = 1000f;
    private float _fullLightBattary = 1000f;
    private float _charge = 300f;
    public LayerMask ghostLayer;

    #endregion

    #region LifeCycle

    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        if (isLight)
        {
            _lightBattary -= 0.2f;
        }
        if (_lightBattary < 0) gameObject.SetActive(false);
    }

    #endregion

    #region Control Light

    public void LightControl()
    {
        if (_lightBattary > 0)
        {
            isLight = !isLight;
            gameObject.SetActive(isLight);
        }
    }
    public float GetLightBattary()
    {
        return _lightBattary / _fullLightBattary;
    }
    public void ChargeBattary()
    {
        _lightBattary = _lightBattary + _charge > _fullLightBattary ? _fullLightBattary : _lightBattary + _charge;
    }
    public void LightRaycast()
    {
        if (isLight)
        {
            RaycastHit[] hits = new RaycastHit[10];
            int numhit = Physics.RaycastNonAlloc(transform.position, transform.forward, hits, 10f, ghostLayer);
            if (numhit > 0)
            {
                hits[0].collider.gameObject.GetComponent<Ghost>().SpeedDown();
            }
        }
    }

    #endregion
}
