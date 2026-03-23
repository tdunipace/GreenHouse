using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController:MonoBehaviour 
{
    private Camera m_Camera;
    private Rigidbody rb;

    public float moveSpeed;
    public float rotationSpeed;

    public bool heldMoveButton;

    PlayerInput playerInput;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction clickAction;

    void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        moveAction.performed += weMovin;
        moveAction.canceled += weStopin;
    }

    /*    private void OnEnable()
        {
            moveAction.action.Enable();
            lookAction.action.Enable();
            clickAction.action.Enable();
        }

        private void OnDisable()
        {
            moveAction.action.Disable();
            lookAction.action.Disable();
            clickAction.action.Disable();
        }*/

    private void FixedUpdate()
    {
        //heldMoveButton = moveAction.IsPressed();
        GetMovin();
    }

    //All of this because we dont know how to hold a button?
    private void weStopin(InputAction.CallbackContext context)
    {
        heldMoveButton = false;
        Debug.Log("We Stopin");
    }

    private void weMovin(InputAction.CallbackContext context)
    {
        heldMoveButton = true;
        Debug.Log("We Movin");
    }

    public bool GetMovin()
    {
        return heldMoveButton;
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();
        //rb.MovePosition(v);

        Vector3 mInput = new Vector3(v.x,0.0f,v.y);
        rb.AddForce(mInput * moveSpeed);

        Debug.Log("You moved with" + v.ToString());
    }

    public void OnLook(InputValue value)
    {
        Debug.Log("You moved the mouse or look stick.");
    }

    public void OnAttack()
    {
        Debug.Log("You left clicked or attacked.");
    }
}
