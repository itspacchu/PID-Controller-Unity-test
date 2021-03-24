using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private bool breaking;
    private float currentSteerAngle;
    private float steerAngle;
    public float avgAppliedTorque;
    public float RbSpeedValue;

    public float AIhori;
    public float AIverti;


    [SerializeField] private Vector3 CenterOfMass;
    
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem skidSmoke;

    [SerializeField] private Rigidbody CarRB;
    [SerializeField] private float breakforce;
    [SerializeField] private float smokeFriction;
    [SerializeField] private float motorForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] public WheelCollider FR;
    [SerializeField] public WheelCollider FL;
    [SerializeField] public WheelCollider BR;
    [SerializeField] public WheelCollider BL;

    [SerializeField] private Transform tFR;
    [SerializeField] private Transform tFL;
    [SerializeField] private Transform tBR;
    [SerializeField] private Transform tBL;

    [SerializeField] private TrailRenderer TrailRendererL;
    [SerializeField] private TrailRenderer TrailRendererR;


    private void Start()
    {
        CarRB = GetComponent<Rigidbody>();
        TrailRendererL.emitting = false;
        TrailRendererR.emitting = false;

    }

    void FixedUpdate() // Rigidbody simulation
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        SkidSmoke();
    }


    private void HandleMotor()
    {
        FR.motorTorque = verticalInput * motorForce * Time.deltaTime;
        FL.motorTorque = verticalInput * motorForce * Time.deltaTime;
        BR.motorTorque = verticalInput * motorForce * Time.deltaTime;
        BL.motorTorque = verticalInput * motorForce * Time.deltaTime;

        avgAppliedTorque = (FR.motorTorque + FL.motorTorque) * 0.5f;
        RbSpeedValue = Mathf.Round(Mathf.Abs(CarRB.velocity.magnitude));

        // Apply restoring force to avoid car from flipping over
        CarRB.centerOfMass = CenterOfMass;
        CarRB.AddForce(Vector3.down * -4f  * RbSpeedValue); //Negative Feedback

        if (breaking)
        {
            ApplyBreaking();
        }
    }

    private void ApplyBreaking()
    {
        FR.brakeTorque = breaking ? breakforce : 0f;
        FL.brakeTorque = breaking ? breakforce : 0f ;


    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal") + AIhori;
        verticalInput = Input.GetAxis("Vertical") + AIverti;
        //breaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        FR.steerAngle = currentSteerAngle;
        FL.steerAngle = currentSteerAngle;

    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FR, tFR);
        UpdateSingleWheel(FL, tFL);
        UpdateSingleWheel(BR, tBR);
        UpdateSingleWheel(BL, tBL);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider,Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void SkidSmoke()
    {
        WheelHit hitBL;
        //WheelHit hitBR;
        var emission = skidSmoke.emission;

        if (BL.GetGroundHit(out hitBL))
        {
            if (Mathf.Abs(hitBL.sidewaysSlip) > .11f)
            {
                emission.rateOverTime = 30f;
                trailRender(true);
            }
            else if(Mathf.Abs(hitBL.sidewaysSlip) < 0.11f){
                emission.rateOverTime = 0f;
                trailRender(false);
            }
        }

    }

    private void trailRender(bool val)
    {
        TrailRendererL.emitting = val;
        TrailRendererR.emitting = val;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(CenterOfMass, 0.1f);
    }
}

