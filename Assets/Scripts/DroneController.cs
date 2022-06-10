using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DroneController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float strafeSpeed;
    [SerializeField] private float verticalSpeed;




    private float forwardInput;
    private float turnInput;
    private float strafeInupt;
    private float verticalInput;

    private Vector3 startPos;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        startPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(strafeInupt*strafeSpeed, verticalInput  * verticalSpeed, forwardInput*forwardSpeed) * Time.fixedDeltaTime, ForceMode.Force);
        rb.AddRelativeTorque(0, turnInput * turnSpeed * Time.fixedDeltaTime, 0, ForceMode.Force);
    }


    //Inputs
    public void OnTurn(InputValue value)
    {
        turnInput = value.Get<float>();
        Debug.Log("TURN: " + turnInput);
    }

    public void OnReset()
    {
        rb.transform.position = startPos;
    }

    public void OnStrafe(InputValue value)
    {
        strafeInupt = value.Get<float>();
        Debug.Log("Strafe: " + strafeInupt);
    }

    public void OnVerticalMove(InputValue value)
    {
        verticalInput = value.Get<float>();
        Debug.Log("Vertical: " + verticalInput);
    }

    public void OnForward(InputValue value)
    {
        forwardInput = value.Get<float>();
        Debug.Log("Forward: " + forwardInput);
    }
    // Update is called once per frame
    
}
