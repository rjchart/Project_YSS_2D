using UnityEngine;
using UnityEditor;
using System.Collections;
using Skills;

public class SkillDatabaseManager : EditorWindow {

	[MenuItem("RPG Tutorial Tools/Skill Database Manager")]
	static void Init() {

		SkillDatabaseManager window = (SkillDatabaseManager)EditorWindow.CreateInstance(typeof(SkillDatabaseManager));
		window.Show();
	}

	// public enum SkillTypeToCreate
	// {
	// 	DefaultSkill,
	// 	SwordSkill
	// }

	public SkillTypeToCreate currentSkillTypeToCreate = SkillTypeToCreate.DefaultSkill;

	string newSkillName = "";
	string newSkillDescription = "";
	int newSkillCost = 0;

	public SkillManager skillManager;

	void OnGUI()
	{

		skillManager = EditorGUILayout.ObjectField("Skill Manager", skillManager, typeof(SkillManager), false) as SkillManager;
		if (skillManager != null)
		{
			currentSkillTypeToCreate = (SkillTypeToCreate)EditorGUILayout.EnumPopup(currentSkillTypeToCreate); 

			newSkillName = EditorGUILayout.TextField("Skill Name: ", newSkillName);
			newSkillDescription = EditorGUILayout.TextField("Description: ", newSkillDescription);
			newSkillCost = EditorGUILayout.IntField("Cost: ", newSkillCost);
			switch(currentSkillTypeToCreate)
			{
				case SkillTypeToCreate.DefaultSkill:
					break;

				case SkillTypeToCreate.SwordSkill:
					break;
						
			}

			if (GUILayout.Button("Add New Skill"))
			{
				switch(currentSkillTypeToCreate)
				{
					case SkillTypeToCreate.DefaultSkill:
						DefaultSkill newDefaultSkill = new DefaultSkill();
						newDefaultSkill.skillName = newSkillName;
						newDefaultSkill.description = newSkillDescription;
						newDefaultSkill.cost = newSkillCost;
						newDefaultSkill.type = (int)currentSkillTypeToCreate;
						skillManager.skillList.Add(newDefaultSkill);
						break;

					case SkillTypeToCreate.SwordSkill:
						SwordSkill newSwordSkill = new SwordSkill();
						newSwordSkill.skillName = newSkillName;
						newSwordSkill.description = newSkillDescription;
						newSwordSkill.cost = newSkillCost;
						newSwordSkill.type = (int)currentSkillTypeToCreate;
						skillManager.skillList.Add(newSwordSkill);
						break;

							
				}
			}
		}
	}
}
