using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : GenericBehaviour {

    HumanPlayer player;
    Transform playerCamera;
    public float walkSpeed = 0.15f;                 // Default walk speed.
    public float runSpeed = 1.0f;                   // Default run speed.
    public float sprintSpeed = 2.0f;                // Default sprint speed.
    public float speedDampTime = 0.1f;              // Default damp time to change the animations based on current speed.

    [Tooltip("This is the max speed for the player")]
    public float maxSpeed; 
    private float speed, speedSeeker;               // Moving speed.
    private int jumpBool;                           // Animator variable related to jumping.
    private int groundedBool;                       // Animator variable related to whether or not the player is on ground.
    private bool jump;                              // Boolean to determine whether or not the player started a jump.
    private bool isColliding;                       // Boolean to determine if the player has collided with an obstacle.



    void Start() {
        player = GetComponent<HumanPlayer>();
        playerCamera = player.cam.transform;
        behaviourManager.GetAnim.SetBool(groundedBool, true);

        // Subscribe and register this behaviour as the default behaviour.
        behaviourManager.SubscribeBehaviour(this);
        behaviourManager.RegisterDefaultBehaviour(this.behaviourCode);
        speedSeeker = runSpeed;
    }

    public override void LocalFixedUpdate() {
        // Call the basic movement manager.
        MovementManagement(player.leftHorizontal, player.leftVertical);

    }
    

    void MovementManagement(float horizontal, float vertical) {
        Vector3 targetDirection = Rotating(horizontal, vertical);

        // Set proper speed.
        Vector2 dir = new Vector2(horizontal, vertical);
        speed = Vector2.ClampMagnitude(dir, 1f).magnitude;

        // This is for PC only, gamepads control speed via analog stick.
        speedSeeker += Input.GetAxis("Mouse ScrollWheel");
        speedSeeker = Mathf.Clamp(speedSeeker, walkSpeed, runSpeed);
        speed *= speedSeeker;
        if (behaviourManager.IsSprinting()) {
            speed = sprintSpeed;
        }

        behaviourManager.GetAnim.SetFloat(speedFloat, speed, speedDampTime, Time.deltaTime);

        if(vertical == 0) {
            targetDirection = Vector3.zero;
        }
        if (behaviourManager.GetRigidBody.velocity.magnitude > maxSpeed)
            behaviourManager.GetRigidBody.velocity = behaviourManager.GetRigidBody.velocity.normalized * maxSpeed;
        else
            behaviourManager.GetRigidBody.AddForce(targetDirection * Physics.gravity.magnitude * sprintSpeed * Time.deltaTime, ForceMode.Acceleration);
    }


    // Rotate the player to match correct orientation, according to camera and key pressed.
    Vector3 Rotating(float horizontal, float vertical) {
        // Get camera forward direction, without vertical component.
        Vector3 forward = playerCamera.TransformDirection(Vector3.forward);

        // Player is moving on ground, Y component of camera facing is not relevant.
        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;

        // Lerp current direction to calculated target direction.
        if ((behaviourManager.IsMoving() && targetDirection != Vector3.zero)) {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(behaviourManager.GetRigidBody.rotation, targetRotation, behaviourManager.turnSmoothing);
            behaviourManager.GetRigidBody.MoveRotation(newRotation);
            behaviourManager.SetLastDirection(targetDirection);
        }
        // If idle, Ignore current camera facing and consider last moving direction.
        if (!(Mathf.Abs(horizontal) > 0.9 || Mathf.Abs(vertical) > 0.9)) {
            behaviourManager.Repositioning();
        }

        return targetDirection;
    }

    // Collision detection.
    private void OnCollisionStay(Collision collision) {
        //isColliding = true;
    }
    private void OnCollisionExit(Collision collision) {
        //isColliding = false;
    }
}
