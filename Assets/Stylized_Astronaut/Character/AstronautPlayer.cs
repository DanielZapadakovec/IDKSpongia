using UnityEngine;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        private Animator anim;

        [SerializeField]
        private float moveSpeed = 15f;
        [SerializeField]
        private float jumpForce = 10f;
        [SerializeField]
        private float turnSpeed = 400.0f;
        private float gravityPauseDuration = 1f;

        [SerializeField]
        private Transform cameraTransform;

        private Vector3 moveDir;
        private bool isGrounded = true;
        private Rigidbody rigidbody;
        private GravityBody gravityBody;

        private void Start()
        {
            anim = GetComponentInChildren<Animator>();
            rigidbody = GetComponent<Rigidbody>();
            gravityBody = GetComponent<GravityBody>();
        }

        private void Update()
        {
            HandleMovementInput();
            HandleJumpInput();
        }

        private void HandleMovementInput()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Rot�cia pohybu pod�a kamery
            moveDir = new Vector3(horizontalInput, 0, verticalInput);
            moveDir = cameraTransform.TransformDirection(moveDir);
            moveDir.y = 0; // Potla�enie vertik�lnej zlo�ky, aby sme neovplyvnili gravit�ciu
            moveDir.Normalize();

            // Nastavenie anim�cie pod�a pohybu
            if (moveDir.magnitude > 0)
            {
                anim.SetInteger("AnimationPar", 1);
                RotateTowardsMovementDirection();
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            }
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        private void RotateTowardsMovementDirection()
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        private void FixedUpdate()
        {
            // Pohyb hr��a pomocou Rigidbody
            rigidbody.MovePosition(rigidbody.position + moveDir * moveSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            anim.SetBool("isGrounded", false);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            // Pozastavenie gravit�cie na ist� �as
            gravityBody.DisableGravity(gravityPauseDuration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                anim.SetBool("isGrounded", true);
                isGrounded = true;
            }
        }
    }
}