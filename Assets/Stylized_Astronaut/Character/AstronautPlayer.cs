using UnityEngine;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        private Animator anim;
        public float moveSpeed = 15;
        public float jumpForce = 10f; // Sila skoku
        public float runningSpeed = 25f;
        public float turnSpeed = 400.0f;
        public float gravity = 20.0f;
        public float gravityPauseDuration = 0.5f; // D�ka pauzy gravit�cie po skoku

        private Vector3 moveDir;
        private bool isGrounded = true; // Kontrola, �i je hr�� na zemi
        public Rigidbody rigidbody;
        public GravityBody gravityBody; // Referencia na GravityBody komponent

        void Start()
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            // Kontrola, �i hr�� stla�il kl�ves pre pohyb
            if (Input.GetKey("w") || Input.GetKey("s"))
            {
                anim.SetInteger("AnimationPar", 1);
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            }

            // Kontrola, �i hr�� stla�il kl�ves pre skok a �i je na zemi
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed = runningSpeed;
            }
            else
            {
                moveSpeed = 5;
            }

            // Pohyb do str�n
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            // Oto�enie
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        }

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("isGrounded", false);

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