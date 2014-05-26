using UnityEngine;
using System.Collections;

public class CSUnit : MonoBehaviour {
	protected Animator animator;
	public bool isDefaultDirectionRight = true;
	protected bool isRight = true;
	protected bool isDamage = false;

	void Awake () {
		animator = GetComponent<Animator>() as Animator;
		isRight = isDefaultDirectionRight;
	}

	// Use this for initialization
	void Start () {
		// animator = GetComponent<Animator>() as Animator;
	
	}
	
	// Update is called once per frame
	void Update () {
		DefaultUpdate();
	}

	public virtual void DefaultUpdate() {
		if (isDamage) 
		{
			if (isRight) {
				rigidbody2D.AddForce(Vector3.left * 400f);
			}
			else {
				rigidbody2D.AddForce(Vector3.right * 400f);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag != "Attack")
			return;

		float scale = other.transform.lossyScale.x;
		PerformDamage(scale);
		// rigidbody2D.AddForce(Vector3.right * 1000f);

		// animation.Play();
		animator.Play("hit",0);
	}

	protected void ChangeObjectScale(GameObject changeObject) {
		float ratio_x = Mathf.Abs(changeObject.transform.localScale.x);
		Vector3 setting = changeObject.transform.localScale;
		if (!isRight ^ isDefaultDirectionRight) {
			setting.x = ratio_x;
		}
		else {
			setting.x = -ratio_x;
		}
		// changeObject.GetComponent("CSAttackObject").SendMessage("SetIsRight", false, SendMessageOptions.RequireReceiver);
		changeObject.transform.localScale = setting;

	}

	protected void ChangeDirection(float axis) {
		if (axis == 0)
			return;
		else if (axis > 0)
			isRight = true;
		else
			isRight = false;
		
		ChangeObjectScale(gameObject);
	}

	protected void PerformDamage(float axis) {
		if (isDamage) 
			return;
		Debug.Log("Damage!!");
		isDamage = true;
		ChangeDirection(-axis);
		Invoke("FinishDamage", .1f);
	}

	void FinishDamage() {
		isDamage = false;
	}

}
