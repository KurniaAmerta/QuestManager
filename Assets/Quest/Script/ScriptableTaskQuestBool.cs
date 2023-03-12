using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "taskQuest_Bool_", menuName = "ScriptableObjects/TaskQuest/TaskQuestBool", order = 2)]
public class ScriptableTaskQuestBool : ScriptableTaskQuest
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
