using UnityEngine;

namespace AstronautPlayer
{
    public class AstronautPlayer : MonoBehaviour
    {
        private Animator anim;
        public float moveSpeed = 15;
        public float jumpForce = 10f; // Sila skoku
        public float turnSpeed = 400.0f;
        public float gravity = 20.0f;
        public float gravityPauseDuration = 0.5f; // Dåžka pauzy gravitácie po skoku

        private Vector3 moveDir;
        private bool isGrounded = true; // Kontrola, èi je hráè na zemi
        public Rigidbody rigidbody;
        public GravityBody gravityBody; // Referencia na GravityBody komponent

        void Start()
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }

        void Update()
        {
            // Kontrola, èi hráè stlaèil kláves pre pohyb
            if (Input.GetKey("w"))
            {
                anim.SetInteger("AnimationPar", 1);
            }
            else
            {
                anim.SetInteger("AnimationPar", 0);
            }

            // Kontrola, èi hráè stlaèil kláves pre skok a èi je na zemi
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            // Pohyb do strán
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            // Otoèenie
            float turn = Input.GetAxis("Horizontal");
            transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        }

        private void FixedUpdate()
        {
            // Pohyb hráèa
            rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }

        private void Jump()
        {
            // Aplikovanie vertikálnej sily pre skok
            rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Nastavíme, že hráè je vo vzduchu
            anim.SetBool("isGrounded", false);

            // Pozastavenie gravitácie pri skoku
            gravityBody.DisableGravity(gravityPauseDuration);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Ak hráè koliduje so zemou, nastavíme, že je na zemi
            if (collision.gameObject.CompareTag("Ground"))
            {
                anim.SetBool("isGrounded", true);
                isGrounded = true;
            }
        }
    }
}