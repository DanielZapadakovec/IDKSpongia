using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator anim;
        public float moveSpeed = 15;
        private Vector3 moveDir;
        public Rigidbody rigidbody;
		public float turnSpeed = 400.0f;
		public float gravity = 20.0f;

		void Start () {
			anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update (){
			if (Input.GetKey ("w")) {
				anim.SetInteger ("AnimationPar", 1);
			}  else {
				anim.SetInteger ("AnimationPar", 0);
			}

            moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		}

        private void FixedUpdate()
        {
            rigidbody.MovePosition(rigidbody.position + transform.TransformDirection(moveDir) * moveSpeed * Time.deltaTime);
        }
    }
}
