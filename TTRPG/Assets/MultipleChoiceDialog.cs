using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XNode;

public class MultipleChoiceDialog : BaseNode {

    [Input] public string entry;
    [Output] public string a;
    [Output] public string b;

    [TextArea(7, 20)]
    public string DialogText;
    public Sprite DialogImage;

    public override string getDialogText()
    {
        return DialogText;
    }

    public override Sprite getSprite()
    {
        return DialogImage;
    }

}