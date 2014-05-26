using UnityEngine;
using UnityEditor;
using System.Collections;

public class ItemDatabaseManager : EditorWindow {

	[MenuItem("RPG Tutorial Tools/Item Database Manager")]
	static void Init() {

		ItemDatabaseManager window = (ItemDatabaseManager)EditorWindow.CreateInstance(typeof(ItemDatabaseManager));
		window.Show();
	}

	public enum ItemTypeToCreate
	{
		Weapon,
		Armor,
		Consumable,
		KeyItem
	}

	public ItemTypeToCreate currentItemTypeToCreate = ItemTypeToCreate.Weapon;

	string newItemName = "";
	string newItemDescription = "";
	int newItemCost = 0;

	int newWeaponDamage = 0;
	int newArmorDefense = 0;
	int newConsumableCharges = 0;

	public ItemManager itemManager;

	void OnGUI()
	{

		itemManager = EditorGUILayout.ObjectField(itemManager, typeof(ItemManager)) as ItemManager;
		if (itemManager != null)
		{
			currentItemTypeToCreate = (ItemTypeToCreate)EditorGUILayout.EnumPopup(currentItemTypeToCreate); 

			newItemName = EditorGUILayout.TextField("Item Name: ", newItemName);
			newItemDescription = EditorGUILayout.TextField("Description: ", newItemDescription);
			newItemCost = EditorGUILayout.IntField("Cost: ", newItemCost);
			switch(currentItemTypeToCreate)
			{
				case ItemTypeToCreate.Weapon:
					newWeaponDamage = EditorGUILayout.IntField("Damage: ", newWeaponDamage);
					break;

				case ItemTypeToCreate.Armor:
					newArmorDefense = EditorGUILayout.IntField("Defense: ", newArmorDefense);
					break;

				case ItemTypeToCreate.Consumable:
					newConsumableCharges = EditorGUILayout.IntField("Charge: ", newConsumableCharges);
					break;

				case ItemTypeToCreate.KeyItem:
					break;
						
			}

			if (GUILayout.Button("Add New Item"))
			{
				switch(currentItemTypeToCreate)
				{
					case ItemTypeToCreate.Weapon:
						Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance<Weapon>();
						newWeapon.name = newItemName;
						newWeapon.description = newItemDescription;
						newWeapon.cost = newItemCost;
						itemManager.itemList.Add(newWeapon);
						break;

					case ItemTypeToCreate.Armor:
						Armor newArmor = (Armor)ScriptableObject.CreateInstance<Armor>();
						newArmor.name = newItemName;
						newArmor.description = newItemDescription;
						newArmor.cost = newItemCost;
						itemManager.itemList.Add(newArmor);
						break;

					case ItemTypeToCreate.Consumable:
						Consumable newConsumable = (Consumable)ScriptableObject.CreateInstance<Consumable>();
						newConsumable.name = newItemName;
						newConsumable.description = newItemDescription;
						newConsumable.cost = newItemCost;
						itemManager.itemList.Add(newConsumable);
						break;

					case ItemTypeToCreate.KeyItem:
						Item newItem = (Item)ScriptableObject.CreateInstance<Item>();
						newItem.name = newItemName;
						newItem.description = newItemDescription;
						newItem.cost = newItemCost;
						itemManager.itemList.Add(newItem);
						break;
							
				}
			}
		}
	}
}
