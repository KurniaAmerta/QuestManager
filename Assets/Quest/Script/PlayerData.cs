using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData Ins { get; set; }

        PlayerDataDetail _dataPlayer;

        public PlayerDataDetail DataPlayer => _dataPlayer;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Ins = this;
        }

        public void ChangeData(PlayerDataDetail data)
        {
            _dataPlayer = data;
        }
    }
}


[System.Serializable]
public class PlayerDataDetail
{
    public string playfabId;
    public string username;
}
