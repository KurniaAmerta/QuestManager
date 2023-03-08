using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Quest.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] Text usernameTxt;
        [SerializeField] Transform _questTrf;
        [SerializeField] QuestItem _questObj;
        ScriptableQuest[] allQuest = new ScriptableQuest[] { };

        QuestItem _questItem;

        private void Start()
        {
            usernameTxt.text = "Welcome "+PlayerData.Ins.DataPlayer.username;
            allQuest = Resources.LoadAll("ScriptableObjects/AllQuest", typeof(ScriptableQuest)).Cast<ScriptableQuest>().ToArray();
            InstantiateQuest();
        }

        public void InstantiateQuest() {
            foreach (var i in allQuest) {
                _questItem = Instantiate(_questObj, _questTrf);
                _questItem.Setup(i.questName);
            }
        }
    }
}
