using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class HandLight : MonoBehaviour
{
    public bool isLight;
    private float _lightBattary = 1000f;
    private float _full_lightBattary = 1000f;
    private float _charge = 300f;
    public LayerMask ghostLayer;

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
        return _lightBattary / _full_lightBattary;
    }

    public void ChargeBattary()
    {
        _lightBattary = _lightBattary + _charge > _full_lightBattary ? _full_lightBattary : _lightBattary + _charge;
    }

    public void LightRaycast()
    {
        if (isLight)
        {
            RaycastHit[] hits = new RaycastHit[10];
            int numhit = Physics.RaycastNonAlloc(transform.position, transform.forward, hits, 10f, ghostLayer);
            if (numhit > 0)
            {
                Debug.Log("충돌한 오브젝트 : " + hits[0].collider.gameObject.name);
                hits[0].collider.gameObject.GetComponent<Ghost>().SpeedDown();
            }
        }


    }
}
