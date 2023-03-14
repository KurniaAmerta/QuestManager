using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Quest
{
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager instance { get; private set; }

        [Header("Data Player")]
        [SerializeField] Text _usernameTxt;
        [SerializeField] Text _coinTxt;
        [SerializeField] Button _shopBtn;

        private void Awake()
        {
            instance = this;
            _shopBtn.onClick.AddListener(PlayerData.instance.Buy);
        }

        private void Start()
        {
            _usernameTxt.text = "Welcome "+PlayerData.instance.DataPlayer.username;
        }

        public void SetCoin() {
            _coinTxt.text = PlayerData.instance.DataPlayer.coin.ToString() + " coin";
        }
    }
}
