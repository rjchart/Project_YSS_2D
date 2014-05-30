using UnityEngine;
using System.Collections;

public enum SkillTypeToCreate
{
	DefaultSkill,
	SwordSkill
}

[System.Serializable]
public class Skill {

	public int cost = 0;
	public string skillName = "";
	public string description = "";
	public bool showing = false;

	public int gi = 50;
	public int sal = 50;
	public int type = 0;

}




