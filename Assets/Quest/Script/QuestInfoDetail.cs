using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfoDetail : MonoBehaviour
{
    [SerializeField] Text _nameTxt;
    [SerializeField] Text _descriptionTxt;
    [SerializeField] Text _progress;

    public void Setup(ScriptableQuest questData) {
        _nameTxt.text = questData.nameQuest;
        _descriptionTxt.text = questData.description;

        _progress.text = "Progress: "+questData.Progress()+"/"+questData.taskQuest.Count;
    }
}
