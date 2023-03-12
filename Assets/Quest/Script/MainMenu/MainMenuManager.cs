using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Quest.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager instance { get; private set; }

        [Header("Data Player")]
        [SerializeField] Text usernameTxt;
        
        [Header("Data Quest")]
        [SerializeField] Transform _questTrf;
        [SerializeField] QuestItem _questObj;
        [SerializeField] QuestInfoDetail _detailInfo;
        QuestItem _questItem;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            usernameTxt.text = "Welcome "+PlayerData.Ins.DataPlayer.username;

            InstantiateQuest();
        }

        void InstantiateQuest() {
            foreach (var i in QuestManager.instance.AllQuest) {
                _questItem = Instantiate(_questObj, _questTrf);
                _questItem.Setup(i.nameQuest, i);
            }
        }

        public void ShowDetail(ScriptableQuest questData) {
            _detailInfo.Setup(questData);
            _detailInfo.gameObject.SetActive(true);
        }
    }
}
