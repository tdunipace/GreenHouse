using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController:MonoBehaviour 
{
    private Camera m_Camera;
    private Rigidbody rb;
    private Transform camTrans;
    private Rigidbody camRb;

    private Vector3 moveValues;
    private Vector3 lookValues;
    private Vector3 oldLook = Vector3.zero;

    public float moveSpeed;
    public float rotationSpeed;
    public float lookSensativity;
    public float lookDrift;

    PlayerInput playerInput;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction clickAction;

    void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        camTrans = m_Camera.gameObject.GetComponent<Transform>();
        camRb = m_Camera.gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Turn
        //Do this on transform I guess
        rb.AddTorque(new Vector3(0.0f, Mathf.Lerp((lookValues.y * rotationSpeed * Time.deltaTime), 0.0f, 0.5f), 0.0f));

        //Movement
        Vector3 curSpeed = moveValues * moveSpeed;
        rb.AddForce(curSpeed * Time.deltaTime);

        //Camera Pitch
        if (lookValues.x != 0)
        {
            float newPitch = Mathf.Lerp((-lookValues.x * lookSensativity * Time.deltaTime), 0.0f, 0.5f);
            //camTrans.Rotate(Mathf.Lerp(newLook.x, 0.0f, lookDrift), 0.0f, 0.0f);
            camRb.AddTorque(newPitch, 0.0f, 0.0f, ForceMode.Impulse);
            Debug.Log("You looked somewhere");
        }
        else
        {
            lookValues = Vector3.zero;
            Debug.Log("You didn't look");
        }
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        moveValues = new Vector3(v.x,0.0f,v.y);

        //Debug.Log("You moved with" + v.ToString());
    }

    // delta only updates when the mouse moves
    public void OnLook(InputValue value)
    {
        var v = value.Get<Vector2>();
        lookValues = new Vector3(v.y, v.x, 0.0f);
    }

    public void OnAttack()
    {
        Debug.Log("You left clicked or attacked.");
    }
}
