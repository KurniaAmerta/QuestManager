using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using System.Linq;
using System;

namespace Quest
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData instance { get; set; }

        PlayerDataDetail _dataPlayer;

        PlayerQuest[] _playerQuest = new PlayerQuest[] { };

        public PlayerDataDetail DataPlayer => _dataPlayer;

        public PlayerQuest[] PlayerQuest => _playerQuest;

        bool _isQuestReady = false;

        public bool IsQuestReady => _isQuestReady;

        public const string COIN = "CO";
        public const string QUEST = "Quest";
        public const string QUESTDATE = "QuestDate";

        private void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

        public void SetQuestReady() {
            _isQuestReady = true;
        }

        public void ChangeData(PlayerDataDetail data)
        {
            _dataPlayer = data;
            GetCurrency();
            LoadQuest();
        }

        public void GetCurrency() {
            PlayFabClientAPI.GetUserInventory(
            new PlayFab.ClientModels.GetUserInventoryRequest { 

            }, response => {
                if (response.VirtualCurrency.ContainsKey(COIN)) {
                    _dataPlayer.coin = response.VirtualCurrency[COIN];
                    MainMenuManager.instance.SetCoin();
                }
                
            }, error => {
                Debug.LogError("error: "+error.ErrorMessage);
            });
        }

        public void Buy()
        {
            PlayFabClientAPI.PurchaseItem(
            new PlayFab.ClientModels.PurchaseItemRequest
            {
                ItemId = "One",
                VirtualCurrency = "CO",
                Price = 10
            }, response => {
                GetCurrency();
                QuestManager.instance.Building("shop goal");
            }, error => {
                Debug.LogError("error: " + error.ErrorMessage);
            });
        }

        public void LoadQuest() {
            PlayFabClientAPI.GetUserData(new PlayFab.ClientModels.GetUserDataRequest
            {
                PlayFabId = _dataPlayer.playfabId
            }, response =>
            {

                if (response.Data.ContainsKey(QUESTDATE) && DateTime.Compare(DateTime.UtcNow.Date, DateTime.Parse(response.Data[QUESTDATE].Value)) < 1)
                {
                    if (response.Data.ContainsKey(QUEST))
                    {
                        if(!string.IsNullOrEmpty(response.Data[QUEST].Value)) _playerQuest = JsonHelper.FromJson<PlayerQuest>(response.Data[QUEST].Value);

                        QuestManager.instance.ResetQuest(false);

                        foreach (var i in _playerQuest)
                        {
                            for (int j = 0; j < i.progress; j++)
                            {
                                QuestManager.instance.Building(i.id, false);
                            }
                        }
                    }
                }
                else {
                    _dataPlayer.questDate = DateTime.UtcNow.ToLongDateString();
                    _playerQuest = new PlayerQuest[] { };
                    QuestManager.instance.ResetQuest(true);
                }
            }, error =>
            {
                Debug.LogError("error: " + error.ErrorMessage);
            });
        }

        public void ProgressQuest(string key) {
            int index = _playerQuest.ToList().FindIndex(x => x.id == key);
            if (index != -1)
            {
                _playerQuest[index].progress++;
            }
            else {
                Array.Resize(ref _playerQuest, _playerQuest.Length + 1);
                _playerQuest[_playerQuest.GetUpperBound(0)] = new PlayerQuest { id = key, progress = 1 };
            }

            string data = JsonHelper.ToJson<PlayerQuest>(_playerQuest);

            key = QUEST;
            SaveData(key, data);
        }

        public void SaveData(string key, string data) {
            PlayFabClientAPI.UpdateUserData(new PlayFab.ClientModels.UpdateUserDataRequest
            {
                Data = new Dictionary<string, string> {
                    { key, data }
                }
            }, response =>
            {

            }, error =>
            {
                Debug.LogError("error: " + error.ErrorMessage);
            });
        }

        public void AddCoin(int _amount) {
            PlayFabClientAPI.ExecuteCloudScript(new PlayFab.ClientModels.ExecuteCloudScriptRequest
            {
                FunctionName = "addCoin",
                FunctionParameter = new { amount = _amount.ToString() },
                GeneratePlayStreamEvent = true
            }, response =>
            {
                GetCurrency();
            }, error =>
            {
                Debug.LogError("error: " + error.ErrorMessage);
            });
        }
    }
}


[System.Serializable]
public class PlayerDataDetail
{
    public string playfabId;
    public string username;
    public int coin;
    public string questDate;
}

[System.Serializable]
public class PlayerQuest
{
    public string id;
    public int progress;
}
