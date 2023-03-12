using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest.MainMenu
{
    public class QuestItem : MonoBehaviour
    {
        [SerializeField] Text _questName;
        ScriptableQuest _questData;

        public void Setup(string name, ScriptableQuest data)
        {
            _questName.text = name;
            _questData = data;
        }

        public void ShowQuest() {
            MainMenuManager.instance.ShowDetail(_questData);
        }
    }
}
