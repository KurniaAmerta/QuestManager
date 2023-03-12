using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "taskQuest_", menuName = "ScriptableObjects/TaskQuest/TaskQuest", order = 1)]
public abstract class ScriptableTaskQuest : ScriptableObject
{
    int questCount = 1;
    string nameTask;
    string description;

    public abstract bool IsDone();
    public abstract int Progress();
}
