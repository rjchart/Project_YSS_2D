#pragma strict

var animator:Animator;

function Start () {
	// animator = GetComponent("Animator") as Animator;
}

function Update () {

	if (Input.GetKeyDown("j")) {
		animator.SetBool("TriggerFire", true);
	}

}