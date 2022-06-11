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
    [SerializeField] private float tiltAngleFwd =25f;
    [SerializeField] private float tiltAngleStrafe = 25f;
    [SerializeField] private float tiltSpeed = 10f;
    [SerializeField] private bool useHover= true;
    [SerializeField] private float hoverStrength;
    [SerializeField] private bool useTransitionalDamping = true;
    [SerializeField] private float dampingStrengthSide;
    [SerializeField] private float dampingStrengthFwd;
    [SerializeField] private float basePitch;
    [SerializeField] private float pitchFactor;
    [SerializeField] private Transform droneCam;




    private float forwardInput;
    private float turnInput;
    private float strafeInupt;
    private float verticalInput;
    private AudioSource source;
    public GroundDetect groundDetect;

    private Vector3 startPos;
    private Quaternion startRot;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        source = GetComponentInChildren<AudioSource>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {

    }

    void FixedUpdate()
    {
        if (groundDetect.flying)
        {
            rb.AddRelativeForce(new Vector3(strafeInupt * strafeSpeed, verticalInput * verticalSpeed, forwardInput * forwardSpeed) * Time.fixedDeltaTime, ForceMode.Force);
            rb.AddRelativeTorque(0, -turnInput * turnSpeed * Time.fixedDeltaTime, 0, ForceMode.Force);

            //hover
            if(useHover && verticalInput ==0)
            {
                float verticalSpeed = rb.velocity.y;
                float correction = verticalSpeed > 0 ?   -hoverStrength :  hoverStrength;
                rb.AddForce(0, correction * Time.fixedDeltaTime, 0, ForceMode.Force);
            }

            //transitional damping
            if(useTransitionalDamping)
            {
                if(strafeInupt == 0)
                {
                    float sideSpeed = rb.velocity.x*2;
                    float correction = verticalSpeed > 0 ? Mathf.Min(sideSpeed, dampingStrengthSide) : Mathf.Max(-sideSpeed, -dampingStrengthSide);
                    
                    rb.AddForce( -correction * Time.fixedDeltaTime, 0, 0, ForceMode.VelocityChange);
                }
                if(forwardInput == 0)
                {
                    float forwardSpeed = rb.velocity.z*2;
                    float correction = verticalSpeed > 0 ? Mathf.Min(forwardSpeed, dampingStrengthFwd) : Mathf.Max(-forwardSpeed, -dampingStrengthFwd);
                    rb.AddForce(0, 0, -correction * Time.fixedDeltaTime, ForceMode.VelocityChange);

                }
            }
            //rotation correction
            Quaternion target = Quaternion.Lerp(rb.rotation, Quaternion.Euler(tiltAngleFwd * forwardInput, rb.rotation.eulerAngles.y, -tiltAngleStrafe * strafeInupt), tiltSpeed*Time.fixedDeltaTime);
            rb.MoveRotation(target);

        }
        else
        {
            rb.AddRelativeForce(new Vector3(0, verticalInput * verticalSpeed, 0) * Time.fixedDeltaTime, ForceMode.Force);
        }

        source.pitch = basePitch+ ( Mathf.Abs(verticalInput) + Mathf.Max(Mathf.Abs(strafeInupt) + Mathf.Abs(forwardInput)))/2f*pitchFactor;
        droneCam.transform.rotation = Quaternion.Euler(0, droneCam.rotation.eulerAngles.y, 0);
        
    }

  

    //Inputs
    public void OnTurn(InputValue value)
    {
        turnInput = value.Get<float>();
      //  Debug.Log("TURN: " + turnInput);
    }

    public void OnReset()
    {
        rb.transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.rotation = startRot;
    }

    public void OnStrafe(InputValue value)
    {
        strafeInupt = value.Get<float>();
       // Debug.Log("Strafe: " + strafeInupt);
    }

    public void OnVerticalMove(InputValue value)
    {
        verticalInput = value.Get<float>();
        //Debug.Log("Vertical: " + verticalInput);
    }

    public void OnForward(InputValue value)
    {
        forwardInput = value.Get<float>();
       // Debug.Log("Forward: " + forwardInput);
    }
    // Update is called once per frame
    
}
