using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SkillManager))]
internal class SkillManagerInspector : Editor {

	bool showingDefaultSkills = false;
	bool showingSwordSkills = false;
	bool showingOtherSkills = false;

	public override void OnInspectorGUI()
	{

		 SkillManager sm = target as SkillManager;

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

		 List<DefaultSkill> defaultSkills = new List<DefaultSkill>();
		 List<SwordSkill> swordSkills = new List<SwordSkill>();
		 List<Skill> otherSkills = new List<Skill>();
		 foreach (Skill oneSkill in sm.skillList)
		 {
		 	if (oneSkill.GetType() == typeof(DefaultSkill))
		 	{
		 		defaultSkills.Add((DefaultSkill)oneSkill);
		 	}

		 	else if (oneSkill.GetType() == typeof(SwordSkill))
		 	{
		 		swordSkills.Add((SwordSkill)oneSkill);
		 	}

		 	else
		 	{
		 		otherSkills.Add(oneSkill);
		 	}
		 }

		 // EditorGUILayout.LabelField("Total DefaultSkills: " + defaultSkills.Count.ToString());
		 // EditorGUILayout.LabelField("Total Skills: " + sm.skillList.Count.ToString());

		// sm.ReorderSkills();

		EditorGUILayout.BeginHorizontal();
		showingDefaultSkills = EditorGUILayout.Foldout(showingDefaultSkills, "DefaultSkills: " + defaultSkills.Count);
		if (GUILayout.Button("Add"))
		{
			DefaultSkill newDefaultSkill = new DefaultSkill();
			newDefaultSkill.skillName = "new defaultSkill";
			newDefaultSkill.description = "";
			newDefaultSkill.cost = 0;
			newDefaultSkill.type = 0;
			// EditorUtility.SetDirty(newDefaultSkill);
			// DontDestroyOnLoad(newDefaultSkill);
			sm.skillList.Add(newDefaultSkill);
			sm.ReorderSkills();
		}
		 EditorGUILayout.EndHorizontal();
		 if (showingDefaultSkills) {
		 	EditorGUI.indentLevel = 1;
			foreach (DefaultSkill defaultSkill in defaultSkills)
			{
				EditorGUILayout.BeginHorizontal();

				defaultSkill.showing = EditorGUILayout.Foldout(defaultSkill.showing, defaultSkill.skillName);
				if (GUILayout.Button("-"))
				{
					sm.skillList.Remove(defaultSkill);
					sm.ReorderSkills();
				}

				EditorGUILayout.EndHorizontal();


				if (defaultSkill.showing) {
					EditorGUI.indentLevel += 1;
					defaultSkill.skillName = EditorGUILayout.TextField("Name: ", defaultSkill.skillName);
					defaultSkill.description = EditorGUILayout.TextField("Description: ", defaultSkill.description);
					defaultSkill.cost = EditorGUILayout.IntField("Cost: ", defaultSkill.cost);
					defaultSkill.gi = EditorGUILayout.IntField("Gi: ", defaultSkill.gi);
					defaultSkill.sal = EditorGUILayout.IntField("Sal: ", defaultSkill.sal);
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}

			}


		 	EditorGUI.indentLevel = 0;
		 }

		 EditorGUILayout.BeginHorizontal();
		showingSwordSkills = EditorGUILayout.Foldout(showingSwordSkills, "SwordSkills:" + swordSkills.Count);
		if (GUILayout.Button("Add"))
		{
			SwordSkill newSwordSkill = new SwordSkill();
			newSwordSkill.skillName = "new SwordSkill";
			newSwordSkill.description = "";
			newSwordSkill.cost = 0;
			newSwordSkill.type = 1;
			// EditorUtility.SetDirty(newSwordSkill);
			sm.skillList.Add(newSwordSkill);
			sm.ReorderSkills();
		}
		 EditorGUILayout.EndHorizontal();

		if (showingSwordSkills) {
			EditorGUI.indentLevel = 1;
			foreach (SwordSkill swordSkill in swordSkills)
			{
				EditorGUILayout.BeginHorizontal();

				swordSkill.showing = EditorGUILayout.Foldout(swordSkill.showing, swordSkill.skillName);
				if (GUILayout.Button("-"))
				{
					sm.skillList.Remove(swordSkill);
					sm.ReorderSkills();
				}

				EditorGUILayout.EndHorizontal();


				if (swordSkill.showing) {
					EditorGUI.indentLevel += 1;
					swordSkill.skillName = EditorGUILayout.TextField("Name: ", swordSkill.skillName);
					swordSkill.description = EditorGUILayout.TextField("Description: ", swordSkill.description);
					swordSkill.cost = EditorGUILayout.IntField("Cost: ", swordSkill.cost);
					swordSkill.gi = EditorGUILayout.IntField("Gi: ", swordSkill.gi);
					swordSkill.sal = EditorGUILayout.IntField("Sal: ", swordSkill.sal);
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}
			}


			EditorGUI.indentLevel = 0;
		}


		 EditorGUILayout.BeginHorizontal();
		showingOtherSkills = EditorGUILayout.Foldout(showingOtherSkills, "OtherSkills: " + otherSkills.Count);
		if (GUILayout.Button("Add"))
		{
			Skill newOtherSkill = new Skill();
			newOtherSkill.skillName = "new otherSkill";
			newOtherSkill.description = "";
			newOtherSkill.cost = 0;
			// EditorUtility.SetDirty(newOtherSkill);
			sm.skillList.Add(newOtherSkill);
			sm.ReorderSkills();
		}
		 EditorGUILayout.EndHorizontal();

		 if (showingOtherSkills) {
		 	EditorGUI.indentLevel = 1;
			foreach (Skill otherSkill in otherSkills)
			{
				EditorGUILayout.BeginHorizontal();

				otherSkill.showing = EditorGUILayout.Foldout(otherSkill.showing, otherSkill.skillName);
				if (GUILayout.Button("-"))
				{
					sm.skillList.Remove(otherSkill);
					// Skill.DestroyImmediate(otherSkill);
					sm.ReorderSkills();
				}

				EditorGUILayout.EndHorizontal();


				if (otherSkill.showing) {
					EditorGUI.indentLevel += 1;
					otherSkill.skillName = EditorGUILayout.TextField("Name: ", otherSkill.skillName);
					otherSkill.description = EditorGUILayout.TextField("Description: ", otherSkill.description);
					otherSkill.cost = EditorGUILayout.IntField("Cost: ", otherSkill.cost);
					otherSkill.gi = EditorGUILayout.IntField("Gi: ", otherSkill.gi);
					otherSkill.sal = EditorGUILayout.IntField("Sal: ", otherSkill.sal);
					otherSkill.type = 2;
					EditorGUI.indentLevel -= 1;
					EditorGUILayout.Space();	

				}

			}


		 	EditorGUI.indentLevel = 0;
		 }

	}
}
