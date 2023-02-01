using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    [Header("Crouch")]
    public float uncrouchControllerHeight = 2;
    public float crouchControllerHeight = 1.5f;
    public float uncrouchCameraHeight = 1.5f;
    public float crouchCameraHeight = 1;
    public float crouchSpeedModifier = 0.5f;
    public float crouchTime = 1;

    [Header("Movement")]
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float jumpSpeed = 10;
    public float midAirSpeedModifier = 0.8f;
    public float gravity = 20;

    [Header("Looking")]
    public GameObject cameraRef;
    public static float mouseXSensitivity = 2;
    public static float mouseYSensitivity = 2;
    public static bool mouseXInverted = false;
    public static bool mouseYInverted = false;

    [Header("Physics")]
    public float pushForce;

    [Header("SpaceZone")]
    public static bool inSpace = false;
    public float spaceSpeed = 0.2f;
    public float dampen = 0.05f;
    public float maxSpeed = 10.0f;
    public float velocity = 0.0f;

    public GameObject fireExtinguisher;
    public ParticleSystem particles;
    
    [Header("Saving")]
    public bool loadPlayer = true;

    private CharacterController controllerRef;
    private Transform transformRef;
    private Vector3 moveVector = Vector3.zero;
    private float speed;
    private float cameraOffset = 0;
    private Vector3 crouchingVelocityV3 = Vector3.zero;
    private float crouchingVelocityF = 0;
    private bool isCrouching = false;

    void Start() {
        transformRef = GetComponent<Transform>();
        controllerRef = GetComponent<CharacterController>();
        inSpace = false;

        if (loadPlayer) {
            LoadPlayer();
        }
    }

    public static float CalculateVelocity(float velocity, float spaceSpeed, float dampen, float maxSpeed){
        if (Input.GetKey(KeyCode.Mouse1)) {
            velocity += spaceSpeed;
        } else {
            velocity -= dampen;
        }

        if (velocity < 0) {
            velocity = 0;
        }
        if (velocity > maxSpeed) {
            velocity = maxSpeed;
        }

        return velocity;
    }

    public void MovePlayer() {
        if (inSpace) {
            //Space Movement//
            
            //enable fireextinguisher
            if (!fireExtinguisher.activeSelf) {
                fireExtinguisher.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                this.particles.Play(true);
            }
            if(Input.GetKeyUp(KeyCode.Mouse1)) {
                this.particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }

            //calculate speed
            this.velocity = CalculateVelocity(this.velocity, this.spaceSpeed, this.dampen, this.maxSpeed);

            //move player
            moveVector = transformRef.TransformDirection(new Vector3(0, 0, -this.velocity));
            controllerRef.Move(moveVector * Time.deltaTime);

        } else {
            //Normal Movement//

            //disable fireExtinguisher
            if (fireExtinguisher.activeSelf) {
                this.velocity = 0.0f;
                fireExtinguisher.SetActive(false);
            }

            //Player crouch
            if (Input.GetKey(KeyboardController.crouchKey)) {
                //Signal the player is crouching
                isCrouching = true;
                //Update controller height and camera
                controllerRef.height = Mathf.SmoothDamp(controllerRef.height, crouchControllerHeight, ref crouchingVelocityF, crouchTime / 10);
                cameraRef.transform.localPosition = Vector3.SmoothDamp(cameraRef.transform.localPosition, new Vector3(0, crouchCameraHeight, 0), ref crouchingVelocityV3, crouchTime / 10);
            } else {
                //Check if anything above player
                if (SpaceAbovePlayer(new Vector3(0, crouchControllerHeight, 0))) { //Middle
                    if (SpaceAbovePlayer(new Vector3(0.5f, crouchControllerHeight, 0))) { //Left Top
                        if (SpaceAbovePlayer(new Vector3(-0.5f, crouchControllerHeight, 0))) { //Right Top
                            if (SpaceAbovePlayer(new Vector3(0, crouchControllerHeight, 0.5f))) { //Left Bottom
                                if (SpaceAbovePlayer(new Vector3(0, crouchControllerHeight, -0.5f))) { //Right Bottom
                                    //Signal the player is no longer crouching
                                    isCrouching = false;
                                    //Update controller height and camera
                                    controllerRef.height = Mathf.SmoothDamp(controllerRef.height, uncrouchControllerHeight, ref crouchingVelocityF, crouchTime / 10);
                                    cameraRef.transform.localPosition = Vector3.SmoothDamp(cameraRef.transform.localPosition, new Vector3(0, uncrouchCameraHeight, 0), ref crouchingVelocityV3, crouchTime / 10);
                                }
                            }
                        }
                    }
                }
            }

            //Update controller collider center
            controllerRef.center = new Vector3(0, controllerRef.height / 2, 0);

            //Get speed
            if (Input.GetKey(KeyboardController.runKey)) {
                speed = runSpeed;
            } else {
                speed = walkSpeed;
            }

            //Get input
            float xInput = Input.GetAxis("Horizontal");
            float yInput = Input.GetAxis("Vertical");

            //Check if grounded
            if (controllerRef.isGrounded == true) {
                //Move player
                moveVector = transformRef.TransformDirection(new Vector3(xInput, -0.1f, yInput));

                //Check for jump key down
                if (Input.GetKeyDown(KeyboardController.jumpKey)) {
                    moveVector.y = jumpSpeed;
                }
            } else {
                //Allow mid air movement but change it by the lost variable
                speed = speed * midAirSpeedModifier;
                moveVector = transformRef.TransformDirection(new Vector3(xInput, moveVector.y, yInput));
            }

            //Apply movement modifiers (excluding mid air modifier)
            if (isCrouching == true) {
                speed = speed * crouchSpeedModifier;
            }

            //Apply gravity
            moveVector = new Vector3(moveVector.x, moveVector.y - gravity * Time.deltaTime, moveVector.z);

            //Apply speed
            moveVector = new Vector3(moveVector.x * speed, moveVector.y, moveVector.z * speed);

            //Move the controller
            controllerRef.Move(moveVector * Time.deltaTime);
        }
    }

    private bool SpaceAbovePlayer(Vector3 pos) {
        return !Physics.Raycast(transform.position + pos, Vector3.up, uncrouchControllerHeight - crouchControllerHeight + 0.1f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore);
    }

    public void MoveCamera() {
        //Camera looking
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");

        //Invert mouse x
        float rotateX;
        if (mouseXInverted == false) {
            rotateX = xInput * mouseXSensitivity;
        } else {
            rotateX = xInput * -mouseXSensitivity;
        }

        //Invert mouse y
        float rotateY;
        if (mouseYInverted == false) {
            rotateY = yInput * mouseYSensitivity;
        } else {
            rotateY = yInput * -mouseYSensitivity;
        }

        //Get rotations and add the mouse inputs (x and y are inverted)
        float bodyYRotation = transform.rotation.eulerAngles.y + rotateX;
        float cameraXRotation = cameraRef.transform.localRotation.eulerAngles.x - rotateY;

        //Add mouse input to the camera offset variable
        cameraOffset = cameraOffset + rotateY;

        //Clamp the mouse value
        if (cameraOffset > 80) {
            cameraXRotation = 280;
            cameraOffset = 80;
        } else if (cameraOffset < -80) {
            cameraXRotation = 80;
            cameraOffset = -80;
        }

        //Apply rotation to camera and player
        SetCameraAngle(new Vector2(cameraXRotation, bodyYRotation));
    }

    //Player physics
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;

        //Make sure the rigidbody exists and is not kinematic
        if ((body != null) && (body.isKinematic == false)) {
            //Get push direction
            Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            //Apply the force
            body.velocity = pushDirection * pushForce * speed;
        }
    }

    //Set player location
    public void SetLocation(Vector3 newLocation) {

        controllerRef.enabled = false;
        transform.position = newLocation;
        controllerRef.enabled = true;
    }

    //Set camera angle
    public void SetCameraAngle(Vector2 newAngle) {
        transform.rotation = Quaternion.Euler(0, newAngle.y, 0);
        cameraRef.transform.localRotation = Quaternion.Euler(newAngle.x, 0, 0);
    }
    
    public void SavePlayer() {
        print("player data saved...");
        SaveSystem.Save(transform);
    }

    public void LoadPlayer() {
        print("player data loading...");
        if (!SaveSystem.IsSaved()) {
            SaveSystem.Save(transform);
            
        }

        SetLocation(SaveSystem.GetPlayerLocation());
    }

    public void ResetPlayer() {
        //TEMP CODE NEEDS TO BE REPLACED WITH CHECKPOINT SYSTEM'S LAST CHECKPOINT
        SetLocation(SaveSystem.GetPlayerLocation());
        GameReferenceGetter.pickUpControllerRef.DropItem(true);
    }
}