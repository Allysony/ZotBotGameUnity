using UnityEngine;
using System.Collections;

public class ZotBotController : MonoBehaviour {

// start/stop controls of zotcar
  public bool driveable = false;

  // Wheel Meshes
  // Front
  public Transform frontLeftWheelMesh;
  public Transform frontRightWheelMesh;
  // Rear
  public Transform rearLeftWheelMesh;
  public Transform rearRightWheelMesh;

  // Mid
  public Transform midLeftWheelMesh;
  public Transform midRightWheelMesh;

  public Transform ZotBotBody;

  // Wheel Colliders
  // Front
  public WheelCollider wheelFL;
  public WheelCollider wheelFR;

  // Rear
  public WheelCollider wheelRL;
  public WheelCollider wheelRR;

  // Mid
  public WheelCollider wheelML;
  public WheelCollider wheelMR;

  public float maxTorque = 20f;
  public float brakeTorque = 100f;

  // max wheel turn angle;
  public float maxWheelTurnAngle = 30f; // degrees

  // car's center of mass
  public Vector3 centerOfMass = new Vector3(0f, 0f, 0f); // unchanged

  // PRIVATE

  // acceleration increment counter
  private float torquePower = 0f;

  // turn increment counter
  private float steerAngle = 0f;

  // original wheel positions
  // Front Left
  private float wheelMeshWrapperFLx;
  private float wheelMeshWrapperFLy;
  private float wheelMeshWrapperFLz;
  // Front Right
  private float wheelMeshWrapperFRx;
  private float wheelMeshWrapperFRy;
  private float wheelMeshWrapperFRz;
  // Mid Left
  private float wheelMeshWrapperMLx;
  private float wheelMeshWrapperMLy;
  private float wheelMeshWrapperMLz;
  // Mid Right
  private float wheelMeshWrapperMRx;
  private float wheelMeshWrapperMRy;
  private float wheelMeshWrapperMRz;
  // Rear Left
  private float wheelMeshWrapperRLx;
  private float wheelMeshWrapperRLy;
  private float wheelMeshWrapperRLz;
  // Rear Right
  private float wheelMeshWrapperRRx;
  private float wheelMeshWrapperRRy;
  private float wheelMeshWrapperRRz;

  private float zotBotBodyx;
  private float zotBotBodyy;
  private float zotBotBodyz;

  void Start () {
    GetComponent<Rigidbody>().centerOfMass = centerOfMass;
  }


  // Visual updates
  void Update () {
    if (! driveable) {
      return;
    }

    // SETUP WHEEL MESHES

    // Turn the mesh wheels
    frontLeftWheelMesh.localEulerAngles = new Vector3(0, steerAngle, 0);
    frontRightWheelMesh.localEulerAngles = new Vector3(0, steerAngle, 0);

    ZotBotBody.transform.eulerAngles = new Vector3( 90, steerAngle * 2  , 0);
    // Wheel rotation
    frontLeftWheelMesh.Rotate(wheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    frontRightWheelMesh.Rotate(wheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    midRightWheelMesh.Rotate(wheelMR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    midLeftWheelMesh.Rotate(wheelML.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    rearLeftWheelMesh.Rotate(wheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    rearRightWheelMesh.Rotate(wheelRR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

   ZotBotBody.Translate( 0,wheelRR.rpm / 60  * Time.deltaTime, 0);

  }

  // Physics updates
  void FixedUpdate () {
    if (! driveable) {
      return;
    }

    // CONTROLS - FORWARD & RearWARD
    if ( Input.GetKey(KeyCode.Space) ) {
      // BRAKE
      torquePower = 0f;
      wheelRL.brakeTorque = brakeTorque;
      wheelRR.brakeTorque = brakeTorque;
      wheelML.brakeTorque = brakeTorque;
      wheelMR.brakeTorque = brakeTorque;
    } else {
      // SPEED
      torquePower = maxTorque * Mathf.Clamp( Input.GetAxis("Vertical"), -1, 1 );
      wheelRL.brakeTorque = 0f;
      wheelRR.brakeTorque = 0f;
      wheelML.brakeTorque = 0f;
      wheelMR.brakeTorque = 0f;

    }
    // Apply torque
    wheelRR.motorTorque = torquePower;
    wheelRL.motorTorque = torquePower;
      wheelML.motorTorque = torquePower;
      wheelMR.motorTorque = torquePower;

    // CONTROLS - LEFT & RIGHT
    // apply steering to front wheels
    steerAngle = maxWheelTurnAngle * Input.GetAxis("Horizontal");
    wheelFL.steerAngle = steerAngle;
    wheelFR.steerAngle = steerAngle;

    
  }
}