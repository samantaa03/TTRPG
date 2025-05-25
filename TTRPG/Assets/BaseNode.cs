using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node {

	public virtual string getDialogText()
	{
		return "";
	}

	public virtual Sprite getSprite()
	{
		return null;
	}
	public virtual ABILITY getAbility()
	{
		return ABILITY.AWARENESS;
	}
	public virtual float getDifficultyCheck()
	{
		return 10.0f;
	}
}