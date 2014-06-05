using UnityEngine;
using System.Collections;

public class BackgroundSceneController : MonoBehaviour {
	public GameObject camera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("mouseX: " + Input.mousePosition.x + "mouseY: " + Input.mousePosition.y);
		GameObject sceneCam = GameObject.Find("Main Camera");

		Vector3 newPosition = ScreenToWorld(Input.mousePosition);
		if (newPosition.y > 2)
			newPosition = new Vector3(0, 2, -10);
		else if (newPosition.y < -2)
			newPosition = new Vector3(0, -2, -10);
		else 
			newPosition = new Vector3(0, newPosition.y, -10);
		Debug.Log("x: " + newPosition.x + ", y: " + newPosition.y);
		camera.transform.position = newPosition;
	}

	Vector3 ScreenToWorld( Vector2 screenPos ) {
	    // Create a ray going into the scene starting 
	    // from the screen position provided 
	    Debug.Log("camera: " + Camera.main);
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
