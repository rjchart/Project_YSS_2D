using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSceneController : MonoBehaviour {

	public tk2dUILayout prefabItem;
	public tk2dUILayout prefabSkillSlot;
	public tk2dUILayout prefabDraggedItem;
	public tk2dUILayout testItem;

	float itemStride = 0;
	float slotStride = 0;

	// Manually set up a scrollable area by working out offsets manually
	public tk2dUIScrollableArea skillItemScrollableArea;
	public tk2dUIScrollableArea skillSlotScrollableArea;
	
	public List<string> currentSkillItemSlots;
	public List<string> currentSkillMatchs;

	tk2dUIItem draggedItem;
	// Internal lists for caching
	List<CSSkillItem> cachedContentItems = new List<CSSkillItem>();
	List<CSSkillItem> unusedContentItems = new List<CSSkillItem>();
	List<CSSkillItem> unusedContentSlots = new List<CSSkillItem>();

	int firstCachedItem = -1;

	int maxVisibleItems = 0;
	int maxVisibleSlots = 0;

	List<string> skillItemNames = new List<string> { "UI_skill_iconA01", "UI_skill_iconA02", "UI_skill_iconA03", "UI_skill_iconB01", "UI_skill_iconB02", "UI_skill_iconB03"};
	List<string> skillSlotNames = new List<string> { "UI_skill_arrow01", "UI_skill_arrow02", "UI_skill_arrow03", "UI_skill_arrow04", "UI_skill_arrow05", "UI_skill_arrow06", "UI_skill_arrow07", "UI_skill_arrow08", "UI_skill_arrowD01", "UI_skill_arrowD02", "UI_skill_arrowD03", "UI_skill_arrowD04", "UI_skill_arrowD05", "UI_skill_arrowD06", "UI_skill_arrowD07", "UI_skill_arrowD08" };

	void OnEnable() {
		skillItemScrollableArea.OnScroll += OnScroll;
	}

	void OnDisable() {
		skillItemScrollableArea.OnScroll -= OnScroll;
	}

	// Use this for initialization
	void Start () {

		// Disable the prefab item
		// don't want it visible when the game is running, as it is in the scene
		// prefabItem.transform.parent = null;
		// DoSetActive( prefabItem.transform, false );


		// How many items do we need to buffer?
		itemStride = (prefabItem.GetMaxBounds() - prefabItem.GetMinBounds()).y;
		// maxVisibleItems = Mathf.CeilToInt(skillItemScrollableArea.VisibleAreaLength / itemStride) + 1;
		// maxVisibleSlots = Mathf.CeilToInt(skillSlotScrollableArea.VisibleAreaLength / slotStride) + 1;

		skillItemScrollableArea.ContentLength = skillItemNames.Count * 1.3f * itemStride + .3f;

		Debug.Log("itemStride: " + prefabItem.GetMaxBounds() + ", area: " + prefabItem.GetMinBounds());
		InitializeSkillSlotSetting();
		InitializeSkillItemSetting();
	
	}
	
	// Update is called once per frame
	void Update () {

		// float getY = Input.mousePosition.y+15;
		// float getX = Input.mousePosition.x+15;
		// Vector3 getPoint = ScreenToWorld(Input.mousePosition);
		// Debug.Log("x,y: " + getPoint);
		// if(draggedItemPosition != null) {
		// 	//Give it a 15 pixel space from the mouse pointer to allow the Player to click stuff and not hit the button we are dragging.
		// 	getPoint.Set(getPoint.x, getPoint.y, -1);
		// 	draggedItemPosition.transform.position = getPoint;
		// }
	}

	void SkillUIDown (tk2dUIItem downItem) {
		// Debug.Log("done down skill");
		// tk2dUILayout item = Instantiate(prefabDraggedItem) as tk2dUILayout;
		// item.transform.parent = skillItemScrollableArea.contentContainer.transform;
		// item.transform.localPosition = downItem.transform.localPosition;
		downItem.transform.localPosition = new Vector3(downItem.transform.localPosition.x, downItem.transform.localPosition.y, -1.0f);
		draggedItem = downItem;
		// item.transform.localPosition = downItem.transform.localPosition;
	}

	void SkillUIUp (tk2dUIItem upItem) {
		Debug.Log("up");
		if (draggedItem)
			draggedItem.transform.localPosition = new Vector3(0, 0, -.5f);

		bool isOk = false;
		CSSkillItem getItem = null;
		foreach (CSSkillItem item in unusedContentSlots) {
			if (item.IsMouseInItemBox() && item.enable) {
				isOk = true;
				getItem = item;
				break;
			}
		}
		if (isOk && getItem) {
			Debug.Log("Gacha!!");
			// Transform childLayout = getLayout.transform.Find("DroppedSkill");
			// childLayout.gameObject.SetActive(true);

			CSSkillItem getSkill = upItem.transform.parent.GetComponent<CSSkillItem>();
			getItem.EnableOnlyStateWithIndex(2);
			getItem.ChangeAllItemImage(getSkill.skillName);
			// ChangeItemImage(childLayout, getSkill.skillName);
		}
		else
			Debug.Log("Nope!!");


	}

	void OnScroll(tk2dUIScrollableArea scrollableArea) {
		// UpdateListGraphics();
	}

	// Vector3 ScreenToWorld( Vector2 screenPos ) {
	//     // Create a ray going into the scene starting 
	//     // from the screen position provided 
	//     Ray ray = Camera.main.ScreenPointToRay( screenPos );
	//     RaycastHit hit;

	//     // ray hit an object, return intersection point
	//     if( Physics.Raycast( ray, out hit ) )
	//        return hit.point;

	//     // ray didn't hit any solid object, so return the 
	//     // intersection point between the ray and 
	//     // the Y=0 plane (horizontal plane)
	//     float t = -ray.origin.y / ray.direction.y;
	//     return ray.GetPoint( t );
	// }

	// void ChangeItemImage(tk2dUILayout layout, string name) {

	// 	Component[] values = layout.GetComponentsInChildren(typeof(tk2dSprite));
	// 	for (int i = 0; i < values.Length; i++) {
	// 		tk2dSprite child = values[i] as tk2dSprite;
	// 		child.SetSprite(name);
	// 	}
	// }

	// void ChangeItemImage(Transform layout, string name) {

	// 	Component[] values = layout.GetComponentsInChildren(typeof(tk2dSprite));
	// 	for (int i = 0; i < values.Length; i++) {
	// 		tk2dSprite child = values[i] as tk2dSprite;
	// 		child.SetSprite(name);
	// 	}

	// 	CSSkillItem getSkill = layout.gameObject.GetComponent<CSSkillItem>();
	// 	getSkill.skillName = name;

	// }

	// bool IsMouseInItemBox(tk2dUILayout layout) {
	// 	Vector3 getPos = ScreenToWorld(Input.mousePosition);
	// 	if (getPos.x > layout.transform.position.x && getPos.x < layout.transform.position.x + layout.transform.lossyScale.x) {
	// 		if (getPos.y < layout.transform.position.y && getPos.y > layout.transform.position.y - layout.transform.lossyScale.y) {
	// 			Debug.Log("compare: " + getPos.y + ", next: " + layout.transform.position.y + ", box: " + layout.transform.lossyScale.y);
	// 			return true;
	// 		}
	// 	}
	// 	return false;
	// }

	void SkillSlotClicked(tk2dUIItem item) {
		CSSkillItem getItem = item.transform.parent.gameObject.GetComponent<CSSkillItem>();
		getItem.EnableOnlyStateWithIndex(1);
	}

	void InitializeSkillSlotSetting() {
		slotStride = (prefabSkillSlot.GetMaxBounds() - prefabSkillSlot.GetMinBounds()).y;
		skillSlotScrollableArea.ContentLength = skillSlotNames.Count * 1.3f * slotStride + .3f;

		int maxSlotNumber = skillSlotNames.Count;

		List<string> unregisteredSkillSlotNames = new List<string>(skillSlotNames);

		foreach (string slotName in currentSkillItemSlots)
		{
			unregisteredSkillSlotNames.Remove(slotName);
		}

		Debug.Log("List unregisteredSkillSlotNames: " + unregisteredSkillSlotNames.Count);

		// Buffer the prefabs that we will need	
		float y = -.3f;
		foreach (string slotName in currentSkillItemSlots) {
			tk2dUILayout layout = Instantiate(prefabSkillSlot) as tk2dUILayout;
			layout.transform.parent = skillSlotScrollableArea.contentContainer.transform;
			layout.transform.localPosition = new Vector3(0, y, 0);
			y -= (float) (itemStride * 1.3);

			// ChangeItemImage(layout.transform, slotName);

			CSSkillItem getSkill = layout.gameObject.GetComponent<CSSkillItem>();
			getSkill.ChangeAllItemImage(slotName);
			getSkill.SetEnable();

			unusedContentSlots.Add( getSkill );
		}

		foreach (string slotName in unregisteredSkillSlotNames) {
			tk2dUILayout layout = Instantiate(prefabSkillSlot) as tk2dUILayout;
			layout.transform.parent = skillSlotScrollableArea.contentContainer.transform;
			layout.transform.localPosition = new Vector3(0, y, 0);
			y -= (float) (itemStride * 1.3);

			// ChangeItemImage(layout.transform, slotName);

			// Transform childLayout = layout.transform.Find("AddSkillButton");
			// childLayout.gameObject.SetActive(true);

			CSSkillItem getSkill = layout.gameObject.GetComponent<CSSkillItem>();
			getSkill.ChangeAllItemImage(slotName);
			getSkill.EnableOnlyStateWithIndex(0);

			unusedContentSlots.Add( getSkill );
		}
	}

	void InitializeSkillItemSetting() {
		itemStride = (prefabItem.GetMaxBounds() - prefabItem.GetMinBounds()).y;
		skillItemScrollableArea.ContentLength = skillItemNames.Count * 1.3f * itemStride + .3f;

		// Buffer the prefabs that we will need	
		float y = -.3f;
		foreach (string itemName in skillItemNames) {
			tk2dUILayout layout = Instantiate(prefabItem) as tk2dUILayout;
			layout.transform.parent = skillItemScrollableArea.contentContainer.transform;
			layout.transform.localPosition = new Vector3(0, y, 0);
			y -= (float) (itemStride * 1.3);

			CSSkillItem getSkill = layout.gameObject.GetComponent<CSSkillItem>();
			getSkill.ChangeAllItemImage(itemName);
			// ChangeItemImage(layout.transform, itemName);

			unusedContentItems.Add( getSkill );
		}	
	}

	void SaveCurrentState() {
	}
}
