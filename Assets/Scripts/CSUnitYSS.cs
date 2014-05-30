using UnityEngine;
using System.Collections;

	enum CurrentState {
		cur_state_stand = 0,
		cur_state_move,
		cur_state_attack
	}
	
public class CSUnitYSS : CSUnit {

	public float speed = 5;
	public float axis = 0;
	// private Animator animator;
	private AnimatorStateInfo currentBaseState;

	// static int standState = Animator.StringToHash("Base Layer.stand");
	// static int moveState = Animator.StringToHash("Base Layer.move");
	// static int swing01State = Animator.StringToHash("Base Layer.swing01");
	// static int swing02State = Animator.StringToHash("Base Layer.swing02");
	// static int swing03State = Animator.StringToHash("Base Layer.swing03");
	// static int swing04State = Animator.StringToHash("Base Layer.swing04");
	// static int swing05State = Animator.StringToHash("Base Layer.swing05");


	// Use this for initialization
	void Start () {
		// animator = GetComponent<Animator>() as Animator;
		// animator.SetBool("TriggerFire", false);
		
	}
	
	// Update is called once per frame
	void Update () {
		// float axis = Input.GetAxis("Horizontal");
		// bool fire = animator.GetBool("TriggerFire");

		// currentBaseState = animator.GetCurrentAnimatorStateInfo(0);

		// // Debug.Log("axis : " + axis);
		// if (axis != 0) {
		// 	animator.SetBool("MoveState", true);
		// 	if (currentBaseState.nameHash == moveState) {
		// 		Vector3 velocity = Vector3.right * axis * speed;
		// 		transform.Translate(velocity * Time.deltaTime);
		// 		ChangeDirection(axis);
		// 	}

		// }
		// else 
		// 	animator.SetBool("MoveState", false);

		// if (fire && currentBaseState.nameHash != swing05State) {
		// 	ChangeDirection(axis);
		// 	animator.SetBool("TriggerFire",false);
		// 	animator.SetBool("MoveState", false);
		// 	animator.SetTrigger("PerformAttack");
		// }

	}

}