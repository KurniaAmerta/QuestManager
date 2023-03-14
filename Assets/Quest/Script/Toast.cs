using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class Toast : MonoBehaviour
    {
        public static Toast instance { get; set; }

        [SerializeField] Text messageTxt;

        List<MessageData> allMessage = new List<MessageData>();

        float waitTime = 0;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            instance = this;
        }

        private void Update()
        {
            waitTime += Time.deltaTime;
            if (allMessage.Count > 0)
            {
                messageTxt.text = allMessage[0].message;

                if (waitTime >= allMessage[0].time)
                {
                    allMessage.RemoveAt(0);
                    messageTxt.text = "";
                    waitTime = 0;
                }
            }
            else {
                waitTime = 0;
            }
        }

        public void ShowToast(string _message, float _time)
        {
            allMessage.Add(new MessageData
            {
                message = _message,
                time = _time
            });
        }

        [System.Serializable]
        class MessageData
        {
            public string message;
            public float time;
        }
    }
}
