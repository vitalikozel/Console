using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogMessage
{
    public int CurrectEmotion;
    public string text;

    public DialogMessage(int currectEmotion, string text)
    {
        CurrectEmotion = currectEmotion;
        this.text = text;
    }
}
