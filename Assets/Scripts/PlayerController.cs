using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController:MonoBehaviour 
{
    private Camera m_Camera;
    private Transform playerTrans;
    private Transform camTrans;

    private Vector3 moveValues;
    private Vector3 lookValues;

    public float moveSpeed;
    public float rotationSpeed;
    public float lookSensativity;

    PlayerInput playerInput;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction clickAction;

    public LayerMask layerMask;

    void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        playerTrans = gameObject.GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();

        //camRb = m_Camera.gameObject.GetComponent<Rigidbody>();
        camTrans = m_Camera.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Turn Body
        //Do this on transform I guess
        playerTrans.Rotate(new Vector3(0.0f, Mathf.Lerp((lookValues.y * rotationSpeed * Time.deltaTime), 0.0f, 0.8f), 0.0f));

        //Movement
        Vector3 curSpeed = moveValues * moveSpeed;
        playerTrans.Translate(curSpeed * Time.deltaTime);

        //Camera Pitch
        float newPitch = Mathf.Lerp((-lookValues.x * lookSensativity * Time.deltaTime), 0.0f, 0.9f);
        camTrans.Rotate(newPitch, 0.0f, 0.0f);

        //Trying to stop it flipping, doesn't give a shit!
/*        float rotationX = camTrans.localRotation.x;
        if (rotationX < 90.0f && rotationX > -90.0f)
            camTrans.Rotate(newPitch, 0.0f, 0.0f);
        else if (rotationX < -90.0f)
            camTrans.Rotate(-90.0f, 0.0f, 0.0f);
        else if (rotationX > 90.0f)
            camTrans.Rotate(90.0f, 0.0f, 0.0f);
        else
            camTrans.Rotate(newPitch, 0.0f, 0.0f);*/
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
        RaycastHit hit;
        if(Physics.Raycast(m_Camera.transform.position, m_Camera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            hit.transform.GetComponent<Animator>().SetTrigger("isClicked");
        }
        else
        {
            Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
        //Debug.Log("You left clicked or attacked.");
    }
}
