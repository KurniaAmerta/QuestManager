using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "taskQuest_Int_", menuName = "ScriptableObjects/TaskQuest/TaskQuestInt", order = 3)]
public class ScriptableTaskQuestInt : ScriptableTaskQuest
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
