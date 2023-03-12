
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "quest_", menuName = "ScriptableObjects/Quest/Quest", order = 1)]
public abstract class ScriptableQuest : ScriptableObject
{
    public string nameQuest;
    public string description;
    public float time;
    public int order;

    public abstract bool IsDone();
    public abstract int Progress();

    public int reward;
    public List<ScriptableTaskQuest> taskQuest;
}
