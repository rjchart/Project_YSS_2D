using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSceneController : MonoBehaviour {

	public tk2dUIItem weaponButton;
	public tk2dUIItem batonButton;

	public tk2dUILayout prefabItem;
	public tk2dUIScrollableArea ScrollableArea;
	float itemStride;
	float scrollRate = 0.0f;
	public float enlargeRate = 1.3f;

	List<string> testItemNames = new List<string> {"Test00", "Test01", "Test02", "Test03", "Test04", "Test05"};
	List<Transform> testItemSelected = new List<Transform>();

	public int selectedItem;

	void OnEnable() {
		ScrollableArea.OnScroll += OnScroll;
	}
	
	void OnDisable() {
		ScrollableArea.OnScroll -= OnScroll;
	}

	// Use this for initialization
	void Start () {
		//testObject.transform.localScale = new Vector3(2.0f,1.0f,1.0f);
		ScrollableArea.transform.gameObject.SetActive(false);
		weaponButton.transform.gameObject.SetActive(true);
		batonButton.transform.gameObject.SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(ScrollableArea.contentContainer.transform.childCount);
	}

	void BackButtonClicked(){
		if (ScrollableArea.transform.gameObject.activeSelf == true){

			ScrollableArea.transform.gameObject.SetActive(false);

			weaponButton.transform.gameObject.SetActive(true);
			batonButton.transform.gameObject.SetActive(true);
		}
	}

	int ItemSelected(tk2dUIItem item){

		//Debug.Log(item.transform.Find("WeaponName").GetComponent<tk2dTextMesh>().text);
		int selected = testItemSelected.IndexOf(item.transform.parent);
		Debug.Log(selected);

		ScrollableArea.Value = ((float)selected + 0.5f) * scrollRate;

		selectedItem = selected;
		return selectedItem;
	}

	void WeaponSelect(){

		weaponButton.transform.gameObject.SetActive(false);
		batonButton.transform.gameObject.SetActive(false);

		ScrollableArea.transform.gameObject.SetActive(true);

		if (ScrollableArea.contentContainer.transform.childCount != 0) return;

		prefabItem.transform.parent = null;
		prefabItem.transform.gameObject.SetActive(false);
		
		float y = -2.0f;
		itemStride = (prefabItem.GetMaxBounds() - prefabItem.GetMinBounds()).y;
		//Debug.Log(itemStride);
		
		foreach (string weaponName in testItemNames) {
			tk2dUILayout layout = Instantiate(prefabItem) as tk2dUILayout;
			layout.transform.parent = ScrollableArea.contentContainer.transform;
			layout.transform.localPosition = new Vector3(0, y, 0);

			y -= (float) (itemStride * 1.3f);
			
			layout.transform.gameObject.SetActive(true);
			layout.transform.Find("WeaponButton/WeaponName").GetComponent<tk2dTextMesh>().text = weaponName;

			testItemSelected.Add(layout.transform);
		}
		ScrollableArea.ContentLength = Mathf.Abs(y - 2.0f);
	}

	void OnScroll(tk2dUIScrollableArea scrollableArea) {

		scrollRate = (1.0f / (float)testItemSelected.Count);
		//Debug.Log(scrollRate);

		for (int i = 0; i < testItemSelected.Count; i++) {
			if (((scrollRate * i) <= ScrollableArea.Value) && ((scrollRate * (i + 1)) > ScrollableArea.Value)){
				//Debug.Log(ScrollableArea.Value);
				testItemSelected[i].localScale = new Vector3(enlargeRate,enlargeRate,1);
			}
			else{
				//Debug.Log(block.enlarged);
				testItemSelected[i].localScale = new Vector3(1, 1, 1);
			}
		}
	}
}
