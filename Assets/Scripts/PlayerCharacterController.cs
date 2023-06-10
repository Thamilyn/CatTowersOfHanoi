using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{

    [SerializeField]
    private float mouseSensitivity;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float dampTime;

    public float grabRange = 5f;

    private Vector3 dampVelocity;
    private Vector3 verticalVelocity;
    private bool jump;

    private CharacterController controller;
    private Animator animator;
    private GameObject grabCube;
    private Transform grabTransform;
    private Transform leaveObjectTransform;
    private GameObject ppArea;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        grabTransform = transform.GetChild(4).transform;
        leaveObjectTransform = transform.GetChild(5).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }

        // Update animator
        if (animator)
        {
            var localVelocity = transform.worldToLocalMatrix.MultiplyVector(controller.velocity);

            animator.SetFloat("Speed", localVelocity.z / speed);
            animator.SetFloat("Strafe", localVelocity.x / speed);
            animator.SetFloat("VerticalSpeed", localVelocity.y);
        }


        if (grabCube != null)
        {
            grabCube.transform.position = grabTransform.position;
        }


        if (Input.GetButton("Fire2") && grabCube != null)
        {
            grabCube.transform.position = leaveObjectTransform.position;
            grabCube.transform.parent = null;
            grabCube.GetComponent<Rigidbody>().useGravity = true;
            //grabCube.GetComponent<Rigidbody>().AddForce(grabTransform.up * -2f, ForceMode.Impulse);
            grabCube = null;
        }
    }

    private void FixedUpdate()
    {
        // Move the X and Z
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        var xzVelocity = Vector3.Scale(controller.velocity, new Vector3(1, 0, 1));
        var desiredVelocity = controller.isGrounded 
            ? ((transform.forward * vertical) + (transform.right * horizontal)) * speed 
            : xzVelocity;
        desiredVelocity = Vector3.SmoothDamp(xzVelocity, desiredVelocity, ref dampVelocity, dampTime);

        // Move Vertically
        verticalVelocity += Physics.gravity * Time.fixedDeltaTime;
        if (controller.isGrounded)
        {
            verticalVelocity.y = -1;
            // Jumping
            if (jump)
            {
                verticalVelocity.y = jumpForce;
            }
        }

        // Perform the movement
        controller.Move((verticalVelocity + desiredVelocity) * Time.fixedDeltaTime);
        animator.SetBool("Ground", controller.isGrounded);
        // Debug.Log("Velocity " + controller.velocity + " Grounded: " + controller.isGrounded);

        var mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * Time.fixedDeltaTime * mouseSensitivity, 0);

        jump = false;
    }

    public void DoJump()
    {
        jump = true;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.collider.CompareTag("InteractButton"))
        {
            if (Input.GetButton("Fire1"))
            {
                grabCube = hit.collider.gameObject;
                grabCube.GetComponent<Rigidbody>().useGravity = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PPArea"))
        {
            ppArea = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PPArea"))
        {
            ppArea = null;
        }
    }
}
