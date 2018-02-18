using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    Collider col;

    private float defaultMoveSpeed;
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float rotationSpeed = 5f;
    public float jumpHeight = 10f;
    public float sprintFOV = 80f; //Field of View for camera when sprinting;
    float maxjumpHeight;

    public bool canJump; // if we want to use jumping
    Vector3 bottom; // holds the bottom of the collider.
    float groundRaycastLength = 0.15f; // used to detect if character is on ground.
    float colliderSkinWidth = 0.15f;
    public string groundLayerMask = "Ground"; //Layer mask to check ground detection.
    bool isJumping;
    bool isColliding;
    Vector3 jumpVector;
    float jumpInterpolation;

    bool sprint;
    float x;
    float y;
    float z;

    //Lerping
    float currJumpLerpTime;
    float jumpLerpTime = 10f;

    Player player;
    Camera playerCamera;
    ThirdPersonOrbit cameraComponent;
    PlayerController playerController;

    // Use this for initialization
    void Start() {

        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GetComponent<Player>();
        playerCamera = player.playerCamera;
        cameraComponent = playerCamera.GetComponent<ThirdPersonOrbit>();
        defaultMoveSpeed = moveSpeed;
    }
  
    // Update is called once per frame
    void Update() {
        if (sprint) {
            cameraComponent.SetFOV(sprintFOV);
            moveSpeed = sprintSpeed;
            Debug.Log("Sprinting now");
        } else {
            cameraComponent.ResetFOV();
            moveSpeed = defaultMoveSpeed;
        }
    }

    private void FixedUpdate() {
        PhysicsMove();
        if ((x > 0 || x < 0) || (z > 0 || z < 0)) { // Only rotate if we are moving. 
            PhysicsRotate(x, z);
        }
    }

    //Call these functions to directly set the movement
    public void SetDirection(Vector3 direction) {
        x = direction.x;
        z = direction.z;
    }
    public void SetSprint(bool sprint) {
        this.sprint = sprint;
    }
    /// <summary>
    /// Movement Method of the player.
    /// </summary>
    void PhysicsMove() {
        Vector3 moveDirection = Vector3.zero;
        
        moveDirection = rb.position + (rb.transform.forward * z * moveSpeed * Time.deltaTime) + (rb.transform.right * x * moveSpeed * Time.deltaTime);

        if (canJump) {
            if (isJumping) {
                if (!isColliding) {
                    moveDirection += InterpolationPhysicsJump();
                } else if (isColliding && !GroundCollisionCheck()) {
                }
            } else if (!GroundCollisionCheck())
                moveDirection -= InterpolationPhysicsJump();
        }

        rb.MovePosition(moveDirection);
    }
    /// <summary>
    /// Jumping Mechanic and a proper smooth lerp
    /// </summary>
    /// <returns></returns>
    Vector3 InterpolationPhysicsJump() {
        if (isJumping && GroundCollisionCheck()) {
            maxjumpHeight = rb.position.y + jumpHeight;
        }
        if (isJumping) {
            currJumpLerpTime += Time.deltaTime;
            if (currJumpLerpTime > jumpLerpTime) {
                currJumpLerpTime = jumpLerpTime;
            }
            float t = currJumpLerpTime / jumpLerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // smooth lerp
            jumpInterpolation = Mathf.Lerp(rb.position.y, maxjumpHeight, t);

            if (jumpInterpolation < maxjumpHeight) {
                jumpVector = new Vector3(0, jumpInterpolation, 0);
            } else {
                isJumping = false;
                currJumpLerpTime = 0.0f;
                maxjumpHeight = 0;
                jumpVector = Vector3.zero;
                jumpInterpolation = 0;
            }
        }
        return jumpVector;
    }
    /// <summary>
    /// Raycast method to check if u are on the ground
    /// </summary>
    /// <returns></returns>
    bool GroundCollisionCheck() {
        bottom = (col.bounds.center - new Vector3(0, (col.bounds.size.y * 0.5f), 0)) + new Vector3(0, colliderSkinWidth, 0); // grabs bottom of the collider + the skin width of the collider
        RaycastHit hit;
        int collisionLayerMask = 1 << LayerMask.NameToLayer(groundLayerMask);
        bool grounded;
        if (Physics.Raycast(bottom, -Vector3.up, out hit, groundRaycastLength, collisionLayerMask)) {
            grounded = true;
        } else {
            grounded = false;
        }
        return grounded;
    }

    /// <summary>
    /// We do a collision check if the object is on the bottom of something. If 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision) {
        if (canJump) {
            if (collision.gameObject.layer != LayerMask.NameToLayer(groundLayerMask)) {
                isJumping = false;
                isColliding = true;
            }
        }
    }
    private void OnCollisionExit(Collision collision) {
        if (canJump) {
            if (collision.gameObject.layer != LayerMask.NameToLayer(groundLayerMask)) {
                isColliding = false;
            }
        }
    }
    /// <summary>
    /// Find target direction, and move towards it
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    void PhysicsRotate(float horizontal, float vertical) {

        Quaternion moveRotation = Quaternion.identity;
        Vector3 targetDirection = new Vector3(horizontal, 0, vertical);
        targetDirection = playerCamera.transform.TransformDirection(targetDirection);
        targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);

        moveRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        Quaternion newDirection = Quaternion.Lerp(rb.rotation, moveRotation, Time.deltaTime * rotationSpeed);
        rb.MoveRotation(newDirection);
    }
}
