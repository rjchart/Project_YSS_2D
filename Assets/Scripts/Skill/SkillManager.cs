using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveDataForSkill 
{
	public int cost;
	public string skillName;
	public string description;

	public int gi;
	public int sal;

	public int type;
}

[Serializable]
public class SkillManager : MonoBehaviour {

	public List<Skill> skillList = new List<Skill>();
	[SerializeField]
	public List<SaveDataForSkill> saveList = new List<SaveDataForSkill>();

	public List<DefaultSkill> defaultSkills = new List<DefaultSkill>();
	public List<SwordSkill> swordSkills = new List<SwordSkill>();
	public List<Skill> otherSkills = new List<Skill>();


	public void Awake() {
		Load();
	}

	public void Save() 
	{


		var bf = new BinaryFormatter();
		// var ms = new MemoryStream();
		FileStream file = File.Open(Application.persistentDataPath + "/SkillInfo.dat", FileMode.Create);
		// bf.Serialize(ms, skillList);
		// Skill getget = skillList[0];
		// Debug.Log("skill" + getget.skillName);
		SaveDatas();

		bf.Serialize(file, saveList);
		Debug.Log("saved");

	}

	public void Load()
	{
		if (File.Exists(Application.persistentDataPath + "/SkillInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SkillInfo.dat", FileMode.Open);
			saveList = (List<SaveDataForSkill>)bf.Deserialize(file);
			LoadDatas();
		}
	}

	public void SaveDatas() 
	{
		saveList.Clear();
		foreach (Skill skill in skillList) {
			if (!skill)
				continue;
			SaveDataForSkill tmpSave = new SaveDataForSkill();
			tmpSave.cost = skill.cost;
			tmpSave.skillName = skill.skillName;
			tmpSave.description = skill.description;
			tmpSave.gi = skill.gi;
			tmpSave.sal = skill.sal;
			tmpSave.type = skill.type;
			Debug.Log("type:" + skill.type);

			saveList.Add(tmpSave);
		}
	}

	public void LoadDatas()
	{
		if (skillList.Count > 0) {
			foreach (Skill removeSkill in skillList) {
				if (!removeSkill)
					continue;
				switch ((int)removeSkill.type) 
				{
					case 0:
						DefaultSkill.DestroyImmediate(removeSkill);
						break;
					case 1:
						SwordSkill.DestroyImmediate(removeSkill);
						break;
					default:
					Skill.DestroyImmediate(removeSkill);
						break;
				}
			}
		}
		skillList.Clear();
		foreach (SaveDataForSkill skill in saveList) {
			Skill tmpSkill = ScriptableObject.CreateInstance<Skill>();
			switch ((int)skill.type) 
			{
				case 0:
					tmpSkill = (Skill)ScriptableObject.CreateInstance<DefaultSkill>();
					break;
				case 1:
					tmpSkill = (Skill)ScriptableObject.CreateInstance<SwordSkill>();
					break;
				default:
				break;
			}
			tmpSkill.cost = skill.cost;
			tmpSkill.skillName = skill.skillName;
			tmpSkill.description = skill.description;
			tmpSkill.gi = skill.gi;
			tmpSkill.sal = skill.sal;
			tmpSkill.showing = false;
			tmpSkill.type = skill.type;
			skillList.Add(tmpSkill);
		}

		ReorderSkills();
	}

	public void ReorderSkills()
	{
		defaultSkills.Clear();
		swordSkills.Clear();
		otherSkills.Clear();
		 foreach (Skill oneSkill in skillList)
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
	}
}
