using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Quest
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance { get; private set; }

        [SerializeField] private GameObject _questPrefab;
        [SerializeField] private Transform _questContent;
        [SerializeField] private GameObject _questHolder;
        [SerializeField] bool test;
        public List<Quest> _currentQuest;

        private void Awake()
        {
            instance = this;

            GameObject questObj;
            foreach (var quest in _currentQuest)
            {   
                quest.Initialize();
                quest._questCompleted.AddListener(OnQuestCompleted);

                questObj = Instantiate(_questPrefab, _questContent);
                questObj.GetComponent<Image>().sprite = quest.Information._icon;

                questObj.GetComponent<Button>().onClick.AddListener(delegate
                {
                    _questHolder.GetComponent<QuestWindow>().Initialize(quest);
                    _questHolder.SetActive(true);
                });

                ResetQuest(false);
            }
        }

        public void ResetQuest(bool isSave = true)
        {
            foreach (var quest in _currentQuest)
            {
                foreach (var goal in quest.Goals)
                {
                    goal.Reset(0);
                }
            }

            if (isSave) {
                PlayerData.instance.SaveData(PlayerData.QUESTDATE, PlayerData.instance.DataPlayer.questDate);
                PlayerData.instance.SaveData(PlayerData.QUEST, "");
            }
        }

        public void Building(string buildingName, bool isSave = true)
        {
            EventManager.Instance.QueueEvent(new BuildingGameEvent(buildingName));
            if (isSave) {
                PlayerData.instance.SetQuestReady();
                PlayerData.instance.ProgressQuest(buildingName);
            }
        }

        private void OnQuestCompleted(Quest quest)
        {
            _questContent.GetChild(_currentQuest.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
        }

        public bool isBuildingDone(string building) {            
            return _currentQuest.Find(x => x.Goals.Find(y => y._completed && y._building == building)) != null;
        }
    }
}
