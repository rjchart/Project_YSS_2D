using UnityEngine;
using System.Collections;

public class CSSkillSlot : CSSkillItem {
	public new void ChangeSkill(string getName) {
		skillName = getName;
		if (getName == "N/A") {
			return;
		}
		EnableOnlyStateWithIndex(2);
		ChangeAllItemImage(getName);
	}

	public void ChangeSlot(string getName) {
		slotName = getName;
		EnableOnlyStateWithIndex(1);
		ChangeAllItemImage(getName);
	}
}
