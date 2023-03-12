using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "questMain_", menuName = "ScriptableObjects/Quest/Main", order = 2)]
public class ScriptableQuestMain : ScriptableQuest
{
    public List<ScriptableQuestSide> questSide;

    public override bool IsDone()
    {
        return false;
    }

    public override int Progress()
    {
        return 0;
    }
}
