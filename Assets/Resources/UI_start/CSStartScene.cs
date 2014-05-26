using UnityEngine;
using System.Collections;

public class CSStartScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Clicked(tk2dUIItem clicedItem) {
		Debug.Log("Clicked:" + clicedItem);
	}

	void OnDown()
	{
		Debug.Log("check");
	}

#region ButtonHandler
	void TestSomething()
	{
		Debug.Log("Test Something");
		Application.LoadLevel("BattleScene");
	}
#endregion
}
