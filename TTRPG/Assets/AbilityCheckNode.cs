using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class AbilityCheckNode : BaseNode {

    [Input] public string entry;
    [Output] public string success;
    [Output] public string failed;

    [TextArea(7, 20)]
    public string DialogText;
    public Sprite DialogImage;
    public float difficultyCheckValue;
    public ABILITY abilityCheck;

    public override string getDialogText()
    {
        return DialogText;
    }

    public override Sprite getSprite()
    {
        return DialogImage;
    }
    public override ABILITY getAbility()    
    {
        return abilityCheck;   
    }
    public override float getDifficultyCheck()
    {
        return difficultyCheckValue;
    }
}