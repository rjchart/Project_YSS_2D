using UnityEngine;
using System.Collections;

namespace Skills {

	public enum SkillTypeToCreate
	{
		DefaultSkill,
		SwordSkill,
		Skill
	}

	public class Skill {

		public int cost = 0;
		public string skillName = "";
		public string description = "";
		public bool showing = false;

		public int gi = 50;
		public int sal = 50;
		public int type = 0;

	}

	public class DefaultSkill : Skill {


	}

	public class SwordSkill : Skill {


	}
	
}