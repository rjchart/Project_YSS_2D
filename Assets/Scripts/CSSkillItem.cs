﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSSkillItem : MonoBehaviour {
	public string skillName;
	public bool enable;
	public List <GameObject> buttonList;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnableStateWithIndex(int index) {
		GameObject button = buttonList[index];
		button.SetActive(true);
	}

	public void DisableStateWithIndex(int index) {
		GameObject button = buttonList[index];
		button.SetActive(false);
	}

	public void EnableOnlyStateWithIndex(int index) {
		int i = 0;
		foreach (GameObject button in buttonList) {
			if (index == i)
				button.SetActive(true);
			else 
				button.SetActive(false);
			i++;
		}
	}

	public void SetEnable() {
		enable = true;
		EnableOnlyStateWithIndex(1);
	}

	public void SetDisable() {
		enable = false;
		EnableOnlyStateWithIndex(0);
	}

	public void ChangeAllItemImage(string name) {

		Component[] values = gameObject.GetComponentsInChildren(typeof(tk2dSprite));
		for (int i = 0; i < values.Length; i++) {
			tk2dSprite child = values[i] as tk2dSprite;
			child.SetSprite(name);
		}

		skillName = name;

	}

	public bool IsMouseInItemBox() {
		Vector3 getPos = ScreenToWorld(Input.mousePosition);
		if (getPos.x > transform.position.x && getPos.x < transform.position.x + transform.lossyScale.x) {
			if (getPos.y < transform.position.y && getPos.y > transform.position.y - transform.lossyScale.y) {
				Debug.Log("compare: " + getPos.y + ", next: " + transform.position.y + ", box: " + transform.lossyScale.y);
				return true;
			}
		}
		return false;
	}

	Vector3 ScreenToWorld( Vector2 screenPos ) {
	    // Create a ray going into the scene starting 
	    // from the screen position provided 
	    Ray ray = Camera.main.ScreenPointToRay( screenPos );
	    RaycastHit hit;

	    // ray hit an object, return intersection point
	    if( Physics.Raycast( ray, out hit ) )
	       return hit.point;

	    // ray didn't hit any solid object, so return the 
	    // intersection point between the ray and 
	    // the Y=0 plane (horizontal plane)
	    float t = -ray.origin.y / ray.direction.y;
	    return ray.GetPoint( t );
	}
}