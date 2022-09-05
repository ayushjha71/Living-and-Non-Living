using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //Player Properties
    [SerializeField]float speed = 5f;
    [SerializeField] float jumpForce;
    [SerializeField] public float gravity;
    [SerializeField] float cameraSensitivity;
    Vector3 jumpVelocity;
    float cameraUpandDown = 0f;
    float currentCameraRotation = 0f;
    float lookSensitivity = 8f;
    float xRotation;
    Rigidbody rb;
    [SerializeField] Vector3 velocity = Vector3.zero;
    Vector3 rotation = Vector3.zero;
    [SerializeField] Animator characterAnimator;

    //Controller Properties
    [SerializeField] FixedJoystick joystick;
    public FixedTouchField touchfield;
    [SerializeField] GameObject fpsCmaera;
    [SerializeField] bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheckPosition;
    public GameObject fixedJoystickPanel;


    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            fixedJoystickPanel.SetActive(true);
        }
        else
        {
            fixedJoystickPanel.SetActive(false);
        }
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float xMovement;
        float yMovment;
        if (Application.platform == RuntimePlatform.Android)
        {
        
            xMovement = joystick.Horizontal;
           yMovment = joystick.Vertical;
        }
        else
        {
          
            xMovement = Input.GetAxis("Horizontal");
           yMovment = Input.GetAxis("Vertical");
        }
        

        

        Vector3 mHorizontal = transform.right * xMovement;
        Vector3 mVertical = transform.forward * yMovment;

        //final velocity
        Vector3 finalVelocity = (mHorizontal + mVertical).normalized * speed;
       
        //movement function
        Move(finalVelocity);

        


        isGrounded = Physics.CheckSphere(groundCheckPosition.position, 0.2f, groundLayer);

        if (isGrounded && jumpVelocity.y < 0f)
        {
            //isJumping = false;
            jumpVelocity.y = 0f;
        }

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //isJumping = true;
                Jump();
            }
        }
        else
        {
            jumpVelocity.y += gravity * Time.deltaTime;
        }

        characterAnimator.SetBool("Grounded", isGrounded);
    }

    private void FixedUpdate()
    {
        if (velocity != Vector3.zero)
        {
            characterAnimator.SetBool("Idle", false);
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        else
        {
            characterAnimator.SetBool("Idle", true);
        }
        characterAnimator.SetBool("Grounded", true);
        //chararcterAnimator.SetFloat("Vel",velocity)
        if(Application.platform == RuntimePlatform.Android)
        {
            float mouseX = 0;
            float mouseY = 0;

            mouseX = touchfield.TouchDist.x;
            mouseY = touchfield.TouchDist.y;

            mouseX *= cameraSensitivity;
            mouseY *= cameraSensitivity;

            //player left right rotation
            Vector3 rotationVector = new Vector3(0, mouseX, 0);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotationVector));
            //this.gameObject.transform.Rotate(rotationVector);


            // clamping the up down rotation and applying limit 
            xRotation -= mouseY * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -70, 70);

            // rotatin the camera if there is a camera object in the player prefab
            if (fpsCmaera != null)
            {

                fpsCmaera.transform.localEulerAngles = new Vector3(xRotation, 0, 0);
            }
        }
        else
        {
            //rotation
            float yrotation = Input.GetAxis("Mouse X");
            Vector3 rotationVector = new Vector3(0, yrotation, 0) * lookSensitivity;

            //Rotate Function
            Rotate(rotationVector);

            //camera roatation up and down
            float xRotation = Input.GetAxis("Mouse Y") * lookSensitivity;

            // camera rotate function
            CameraRotation(xRotation);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));


            if (fpsCmaera != null)
            {
                currentCameraRotation -= cameraUpandDown;
                currentCameraRotation = Mathf.Clamp(currentCameraRotation, -60, 60);

                fpsCmaera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0, 0);
            }


        }


        if (isGrounded)
        {
            rb.AddForce(jumpVelocity, ForceMode.Impulse);
        }



    }

    void Move(Vector3 finalVelocity)
    {
        velocity = finalVelocity;
    }

    void Rotate(Vector3 rotationVector)
    {
        rotation = rotationVector;
    }

    void CameraRotation(float cameraUpandDownRotation)
    {
        cameraUpandDown = cameraUpandDownRotation;
    }

    public void Jump()
    {
        jumpVelocity.y = jumpForce;
    }

}
