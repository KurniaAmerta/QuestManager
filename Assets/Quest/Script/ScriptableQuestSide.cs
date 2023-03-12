using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "questSide_", menuName = "ScriptableObjects/Quest/Side", order = 2)]
public class ScriptableQuestSide : ScriptableQuest
{
    public override bool IsDone()
    {
        return false;
    }

    public override int Progress()
    {
        return 0;
    }
}
