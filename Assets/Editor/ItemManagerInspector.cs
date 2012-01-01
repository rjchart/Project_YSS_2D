using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(ItemManager))]
internal class ItemManagerInspector : Editor {

	bool showingWeapons = false;
	bool showingArmors = false;
	bool showingConsumables = false;
	bool showingKeyItems = false;

	public override void OnInspectorGUI()
	{
		 EditorGUILayout.LabelField("Item Managment");
		 ItemManager im = target as ItemManager;

		 // EditorGUILayout.LabelField("Number of Total Items: " + im.itemList.Count);

		 List<Weapon> weapons = new List<Weapon>();
		 List<Armor> armors = new List<Armor>();
		 List<Consumable> consumables = new List<Consumable>();
		 List<Item> keyitems = new List<Item>();

		 foreach (Item oneItem in im.itemList)
		 {
		 	if (oneItem.GetType() == typeof(Weapon))
		 	{
		 		weapons.Add((Weapon)oneItem);
		 	}

		 	else if (oneItem.GetType() == typeof(Armor))
		 	{
		 		armors.Add((Armor)oneItem);
		 	}

		 	else if (oneItem.GetType() == typeof(Consumable))
		 	{
		 		consumables.Add((Consumable)oneItem);
		 	}

		 	else 
		 	{
		 		keyitems.Add(oneItem);
		 	}
		 }

		 // EditorGUILayout.LabelField("Total Weapons: " + weapons.Count.ToString());
		 // EditorGUILayout.LabelField("Total Items: " + im.itemList.Count.ToString());

		 showingWeapons = EditorGUILayout.Foldout(showingWeapons, "Weapons: " + weapons.Count);
		 if (showingWeapons) {
		 	EditorGUI.indentLevel = 1;
			foreach (Weapon weapon in weapons)
			{
				EditorGUILayout.BeginHorizontal();

				weapon.showing = EditorGUILayout.Foldout(weapon.showing, weapon.name);
				if (GUILayout.Button("-"))
				{
					im.itemList.Remove(weapon);
					Weapon.DestroyImmediate(weapon);
				}

				EditorGUILayout.EndHorizontal();


				if (weapon.showing) {
					EditorGUI.indentLevel += 1;
					weapon.name = EditorGUILayout.TextField("Name: ", weapon.name);
					weapon.description = EditorGUILayout.TextField("Description: ", weapon.description);
					weapon.cost = EditorGUILayout.IntField("Cost: ", weapon.cost);
					weapon.damage = EditorGUILayout.IntField("Damage: ", weapon.damage);
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}

			}

			if (GUILayout.Button("Add New Weapon"))
			{
				Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
				newWeapon.name = "new weapon";
				newWeapon.description = "";
				newWeapon.cost = 0;
				newWeapon.damage = 0;
				im.itemList.Add(newWeapon);
			}

		 	EditorGUI.indentLevel = 0;
		 }

		 showingArmors = EditorGUILayout.Foldout(showingArmors, "Armors:" + armors.Count);
		 if (showingArmors) {
		 	EditorGUI.indentLevel = 1;
			 foreach (Armor armor in armors)
			 {
				EditorGUILayout.BeginHorizontal();

				armor.showing = EditorGUILayout.Foldout(armor.showing, armor.name);
				if (GUILayout.Button("-"))
				{
					im.itemList.Remove(armor);
					Armor.DestroyImmediate(armor);
				}

				EditorGUILayout.EndHorizontal();


				if (armor.showing) {
					EditorGUI.indentLevel += 1;
					armor.name = EditorGUILayout.TextField("Name: ", armor.name);
					armor.description = EditorGUILayout.TextField("Description: ", armor.description);
					armor.cost = EditorGUILayout.IntField("Cost: ", armor.cost);
					armor.defense = EditorGUILayout.IntField("Defense: ", armor.defense);
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}
			 }

			if (GUILayout.Button("Add New Armor"))
			{
				Armor newArmor = (Armor)ScriptableObject.CreateInstance<Armor>();
				newArmor.name = "new Armor";
				newArmor.description = "";
				newArmor.cost = 0;
				newArmor.defense = 0;
				im.itemList.Add(newArmor);
			}

		 	EditorGUI.indentLevel = 0;
		 }

		 showingConsumables = EditorGUILayout.Foldout(showingConsumables, "Consumables:" + consumables.Count);
		 if (showingConsumables) {
		 	EditorGUI.indentLevel = 1;
			 foreach (Consumable consumable in consumables)
			 {
				EditorGUILayout.BeginHorizontal();

				consumable.showing = EditorGUILayout.Foldout(consumable.showing, consumable.name);
				if (GUILayout.Button("-"))
				{
					im.itemList.Remove(consumable);
					Consumable.DestroyImmediate(consumable);
				}

				EditorGUILayout.EndHorizontal();


				if (consumable.showing) {
					EditorGUI.indentLevel += 1;
					consumable.name = EditorGUILayout.TextField("Name: ", consumable.name);
					consumable.description = EditorGUILayout.TextField("Description: ", consumable.description);
					consumable.cost = EditorGUILayout.IntField("Cost: ", consumable.cost);
					consumable.charges = EditorGUILayout.IntField("Charges: ", consumable.charges);
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}
			 }

			if (GUILayout.Button("Add New Consumable"))
			{
				Consumable newConsumable = (Consumable)ScriptableObject.CreateInstance<Consumable>();
				newConsumable.name = "new Consumable";
				newConsumable.description = "";
				newConsumable.cost = 0;
				newConsumable.charges = 0;
				im.itemList.Add(newConsumable);
			}
		 	EditorGUI.indentLevel = 0;
		 }

		 showingKeyItems = EditorGUILayout.Foldout(showingKeyItems, "Key Items:" + keyitems.Count);
		 if (showingKeyItems) {
		 	EditorGUI.indentLevel = 1;
			 foreach (Item item in keyitems)
			 {
				EditorGUILayout.BeginHorizontal();

				item.showing = EditorGUILayout.Foldout(item.showing, item.name);
				if (GUILayout.Button("-"))
				{
					im.itemList.Remove(item);
					Item.DestroyImmediate(item);
				}

				EditorGUILayout.EndHorizontal();


				if (item.showing) {
					EditorGUI.indentLevel += 1;
					item.name = EditorGUILayout.TextField("Name: ", item.name);
					item.description = EditorGUILayout.TextField("Description: ", item.description);
					item.cost = EditorGUILayout.IntField("Cost: ", item.cost);
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}
			 }

			if (GUILayout.Button("Add New Item"))
			{
				Item newItem = (Item)ScriptableObject.CreateInstance<Item>();
				newItem.name = "new Item";
				newItem.description = "";
				newItem.cost = 0;
				im.itemList.Add(newItem);
			}
		 	EditorGUI.indentLevel = 0;
		 }
	}
}
