using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Skills;


[CustomEditor(typeof(SkillManager))]
internal class SkillManagerInspector : Editor {

	bool showingDefaultSkills = false;
	bool showingSwordSkills = false;
	bool showingOtherSkills = false;

	public override void OnInspectorGUI()
	{
		SkillManager sm = target as SkillManager;
		sm.Init();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Skill Managment: " + sm.skillList.Count);

		if (GUILayout.Button("removeAll"))
		{
			sm.skillList.Clear();
		}
		if (GUILayout.Button("reorder"))
		{
			sm.ReorderSkills();
		}
		if (GUILayout.Button("save"))
		{
			sm.Save();
		}
		if (GUILayout.Button("load"))
		{
			sm.Load();
		}

		EditorGUILayout.EndHorizontal();

		 // EditorGUILayout.LabelField("Number of Total Skills: " + sm.skillList.Count);
		Dictionary<string,List<Skill>> skillListDic = new Dictionary<string,List<Skill>>();
		List<DefaultSkill> defaultSkills = new List<DefaultSkill>();
		List<SwordSkill> swordSkills = new List<SwordSkill>();
		List<Skill> otherSkills = new List<Skill>();

		 // SkillTypeToCreate.GetValues();

	 	foreach (string type in Enum.GetNames(typeof(SkillTypeToCreate)))
	 	{
	 		List<Skill> tmpSkills = new List<Skill>();
	 		skillListDic.Add(type, tmpSkills);
	 	}

		foreach (Skill oneSkill in sm.skillList)
		{
		 	foreach (string type in Enum.GetNames(typeof(SkillTypeToCreate)))
		 	{
		 		// Type tmpType = Type.GetType("System.MonoType." + type, true);
		 		if (oneSkill.GetType().ToString() == "Skills." + type) {
		 			skillListDic[type].Add(oneSkill);
		 		}
		 	}
		}
		 



		 // EditorGUILayout.LabelField("Total DefaultSkills: " + defaultSkills.Count.ToString());
		 // EditorGUILayout.LabelField("Total Skills: " + sm.skillList.Count.ToString());

		// sm.ReorderSkills();

 		Assembly asm = typeof(Skill).Assembly;
		// Type type = asm.GetType(namespaceQualifiedTypeName);
	 	foreach (string type in Enum.GetNames(typeof(SkillTypeToCreate)))
	 	{
	 		Type tmpType = asm.GetType("Skills." + type, true);
	 		// Debug.Log("Success: " + tmpType);

			EditorGUILayout.BeginHorizontal();
			sm.skillShowDic[type] = EditorGUILayout.Foldout(sm.skillShowDic[type], type + ": " + skillListDic[type].Count);
			if (GUILayout.Button("Add"))
			{
				Skill newSkill = (Skill)Activator.CreateInstance(tmpType);
				newSkill.skillName = "new " + type;
				newSkill.description = "";
				newSkill.cost = 0;
				newSkill.type = 0;
				// EditorUtility.SetDirty(newSkill);
				// DontDestroyOnLoad(newSkill);
				sm.skillList.Add(newSkill);	
				sm.ReorderSkills();
			}
			EditorGUILayout.EndHorizontal();

			 if (sm.skillShowDic[type]) {
			 	EditorGUI.indentLevel = 1;
				foreach (Skill skill in skillListDic[type])
				{
					EditorGUILayout.BeginHorizontal();

					skill.showing = EditorGUILayout.Foldout(skill.showing, skill.skillName);
					if (GUILayout.Button("-"))
					{
						sm.skillList.Remove(skill);
						sm.ReorderSkills();
					}

					EditorGUILayout.EndHorizontal();


					if (skill.showing) {
						EditorGUI.indentLevel += 1;
						skill.skillName = EditorGUILayout.TextField("Name: ", skill.skillName);
						skill.description = EditorGUILayout.TextField("Description: ", skill.description);
						skill.cost = EditorGUILayout.IntField("Cost: ", skill.cost);
						skill.gi = EditorGUILayout.IntField("Gi: ", skill.gi);
						skill.sal = EditorGUILayout.IntField("Sal: ", skill.sal);
						EditorGUI.indentLevel -= 1;
						EditorGUILayout.Space();	

					}

				}


			 	EditorGUI.indentLevel = 0;
			 }	
	 	}


	}
}
