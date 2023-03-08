using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    [SerializeField] Text _questName;

    public void Setup(string name) {
        _questName.text = name;
    }
}
