using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

public class CSInputCheck : CSUnit {

	public GameObject prefAttack;
	private GameObject _savedAttack;

	public bool _isEnableInput = true;
	public bool _saveFireButton = false;

	// public Animator animator;
	float speed = 5;
	public float _attack_move = 0;

	private float axis = 0;
	private bool fire = false;

	private AnimatorStateInfo currentBaseState;
	private bool _isFinishAttack = false;

	// static int standState = Animator.StringToHash("Base Layer.stand");
	static int moveState = Animator.StringToHash("Base Layer.move");
	// static int swing01State = Animator.StringToHash("Base Layer.swing01");
	// static int swing02State = Animator.StringToHash("Base Layer.swing02");
	// static int swing03State = Animator.StringToHash("Base Layer.swing03");
	// static int swing04State = Animator.StringToHash("Base Layer.swing04");
	// static int swing05State = Animator.StringToHash("Base Layer.swing05");

	// Use this for initialization
	void Start () {
		// animator.SetBool("TriggerFire", false);
	}
	
	// Update is called once per frame
	void Update () {
		DefaultUpdate();
		axis = Input.GetAxis("Horizontal");
		fire = Input.GetKeyDown("j");
		currentBaseState = animator.GetCurrentAnimatorStateInfo(0);

		// if (!_isEnableInput) {
		// 	if (fire)
		// 		_saveFireButton = true;
		// 	return;
		// }

		// _unitData.axis = axis;

		// Debug.Log("debug: " + axis);
		CheckMovement();
		CheckFire();

	}

	public override void DefaultUpdate() {
		base.DefaultUpdate();
	}

	void CheckMovement() {
		if (axis != 0) {
			animator.SetBool("isMove", true);
			if (currentBaseState.nameHash == moveState) 
			{
				Vector3 velocity = Vector3.right * axis * speed;
				transform.Translate(velocity * Time.deltaTime);
				ChangeDirection(axis);
			}

		}
		else 
			animator.SetBool("isMove", false);


		if (_attack_move != 0) {
			Debug.Log("attack_move: " + _attack_move);
			Vector3 velocity;
			if (transform.localScale.x > 0)
				velocity = Vector3.right * _attack_move;
			else 
				velocity = Vector3.left * _attack_move;
			transform.Translate(velocity * Time.deltaTime);
			// ChangeDirection(axis);
		}
	}

	void CheckFire() {
		if (fire) {
			if (currentBaseState.IsTag("default")) {
				PerformNextAttack();
			}
			else 
			if (_isFinishAttack) {
				PerformNextAttack();
			}
			else {
				_saveFireButton = true;
			}
		}
	}

	void FinishAttack() {	
		if (_saveFireButton) {
			Debug.Log("finish check");
			PerformNextAttack();
		}
		else {
			_saveFireButton = false;
			_isFinishAttack = true;
		}
		Destroy(_savedAttack);

		return;
	}

	void PerformNextAttack() {
		_saveFireButton = false;
		_isFinishAttack = false;
		ChangeDirection(axis);
		animator.SetTrigger("PerformAttack");
	}

	void StartAttack() {
		GameObject tmp = Instantiate(prefAttack, transform.position, Quaternion.identity) as GameObject;
		ChangeObjectScale(tmp);
		tmp.transform.parent = transform;
		_savedAttack = tmp;
	}


}
