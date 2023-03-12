using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance { get; private set; }

    ScriptableQuest[] allQuest = new ScriptableQuest[] { };

    public ScriptableQuest[] AllQuest {
        get {
            if (allQuest.Count() == 0) {
                LoadQuestAll();
            }
            return allQuest;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void LoadQuestAll() {
        allQuest = Resources.LoadAll("ScriptableObjects/AllQuest", typeof(ScriptableQuest)).Cast<ScriptableQuest>().ToArray();
    }
}
