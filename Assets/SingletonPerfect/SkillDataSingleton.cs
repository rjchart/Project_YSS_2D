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
public class SkillDataSingleton : Singleton<SkillDataSingleton> {

	[SerializeField]
	public List<Skill> skillList = new List<Skill>();
	[SerializeField]
	public List<SaveDataForSkill> saveList = new List<SaveDataForSkill>();
	[SerializeField]
	public Dictionary<string,string> skillSettingDic = new Dictionary<string,string>();

	public List<DefaultSkill> defaultSkills = new List<DefaultSkill>();
	public List<SwordSkill> swordSkills = new List<SwordSkill>();
	public List<Skill> otherSkills = new List<Skill>();


	public static SkillDataSingleton Instance {
		get {
			return ((SkillDataSingleton)mInstance);
		} set {
			mInstance = value;
		}
	}

	public void Save() 
	{

		// SetSkillInfoDatas();
		SetSkillSettingDatas();

	}

	void SetSkillInfoDatas()
	{
		var bf = new BinaryFormatter();
		FileStream file = new FileStream(Application.persistentDataPath + "/SkillInfo.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite); 
		SaveDatas();

		bf.Serialize(file, saveList);
		Debug.Log("saved");
		file.Close();

	}

	void SetSkillSettingDatas()
	{
		var bf = new BinaryFormatter();
		FileStream file = new FileStream(Application.persistentDataPath + "/SkillSetting.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite); 

		bf.Serialize(file, skillSettingDic);
		Debug.Log("saved Dic");
		file.Close();

	}

	public void Load()
	{
		GetSkillInfoDatas();
		GetSkillSettingDatas();
	}

	void GetSkillInfoDatas()
	{
		string path = Application.persistentDataPath + "/SkillInfo.dat";
		if (File.Exists(path))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
			// File.Open(Application.persistentDataPath + "/SkillInfo.dat", FileMode.Open);
			// saveList.Clear();
			saveList = (List<SaveDataForSkill>)bf.Deserialize(file);
			LoadDatas();
			file.Close();
		}
		Debug.Log("skillInfo loaded");
	}

	void GetSkillSettingDatas()
	{
		string path = Application.persistentDataPath + "/SkillSetting.dat";
		if (File.Exists(path))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
			skillSettingDic = (Dictionary<string,string>)bf.Deserialize(file);
			file.Close();
		}
		else {
			skillSettingDic.Add("UI_skill_arrow01", "N/A");
		}

	}

	public void SaveDatas() 
	{
		saveList.Clear();
		foreach (Skill skill in skillList) {
			SaveDataForSkill tmpSave = new SaveDataForSkill();
			tmpSave.cost = skill.cost;
			tmpSave.skillName = skill.skillName;
			tmpSave.description = skill.description;
			tmpSave.gi = skill.gi;
			tmpSave.sal = skill.sal;
			tmpSave.type = skill.type;

			saveList.Add(tmpSave);
		}
	}

	public void LoadDatas()
	{
		// if (skillList.Count > 0) {
		// 	foreach (Skill removeSkill in skillList) {
		// 		switch ((int)removeSkill.type) 
		// 		{
		// 			case 0:
		// 				DefaultSkill.DestroyImmediate(removeSkill);
		// 				break;
		// 			case 1:
		// 				SwordSkill.DestroyImmediate(removeSkill);
		// 				break;
		// 			default:
		// 			Skill.DestroyImmediate(removeSkill);
		// 				break;
		// 		}
		// 	}
		// }
		skillList.Clear();
		foreach (SaveDataForSkill skill in saveList) {
			Skill tmpSkill = new Skill();
			switch ((int)skill.type) 
			{
				case 0:
					tmpSkill = new DefaultSkill();
					break;
				case 1:
					tmpSkill = new SwordSkill();
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
