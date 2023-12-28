using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IWalkable
{
    [Header("Move")]
    private Rigidbody _rigid;
    private Vector2 _curMoveInput;
    private float moveModifier = 3f;
    private float runModifier = 2f;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private Light _spotLight;
    private bool isLight;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _spotLight = GetComponentInChildren<Light>();
        GameManager.GM.SetPlayer(this);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _spotLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void Move()
    {
        Vector3 dir = transform.forward * _curMoveInput.y + transform.right * _curMoveInput.x;
        dir *= moveModifier;
        dir.y = _rigid.velocity.y;
        _rigid.velocity = dir;
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            _curMoveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _curMoveInput = Vector2.zero;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            _rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            moveModifier *= runModifier;
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            moveModifier /= runModifier;
        }
    }

    public void OnLightInput(InputAction.CallbackContext context)
    {
        Debug.Log("Light Click");
        if (_spotLight == null) return;
        if(context.phase == InputActionPhase.Started)
        {
            if (isLight)
            {
                isLight = false;
                _spotLight.gameObject.SetActive(false);
                Debug.Log("spotlight off");
            }
            else
            {
                isLight = true;
                _spotLight.gameObject.SetActive(true);
                Debug.Log("spotlight on");
            }
        }
    }
}
