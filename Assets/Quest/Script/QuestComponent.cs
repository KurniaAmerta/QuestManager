using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Quest
{
    public class QuestComponent : MonoBehaviour
    {
        [SerializeField] string _buildingName;
        [SerializeField] string _message;

        private void OnTriggerEnter(Collider other)
        {
            if (!QuestManager.instance.isBuildingDone(_buildingName))
            {
                gameObject.SetActive(false);
                QuestManager.instance.Building(_buildingName);
                Toast.instance.ShowToast(_message, 1.75f);
            }
            else {
                Toast.instance.ShowToast("Quest already clear", 1.75f);
            }
        }
    }
}
