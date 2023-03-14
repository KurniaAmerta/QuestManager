using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quest
{
    public class QuestWindow : MonoBehaviour
    {
        [SerializeField] private Text titleTxt;
        [SerializeField] private Text descriptionTxt;
        [SerializeField] private GameObject goalPrefab;
        [SerializeField] private Transform goalContent;
        [SerializeField] private Text xpTxt;
        [SerializeField] private Text coinTxt;

        public void Initialize(Quest quest)
        {
            titleTxt.text = quest.Information._name;
            descriptionTxt.text = quest.Information._description;

            GameObject goalObj, countObj, skipObj;

            for (int i=0; i< goalContent.childCount; i++) {
                Destroy(goalContent.GetChild(i).gameObject);
            }

            foreach (var goal in quest.Goals)
            {
                goalObj = Instantiate(goalPrefab, goalContent);
                goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

                countObj = goalObj.transform.Find("Count").gameObject;
                skipObj = goalObj.transform.Find("Skip").gameObject;

                if (goal._completed)
                {
                    countObj.SetActive(false);
                    skipObj.SetActive(false);

                    //goalObj.transform.Find("Done").gameObject.SetActive(true);
                }
                else
                {
                    countObj.GetComponent<Text>().text = goal._currentAmount + "/" + goal._requiredAmount;

                    skipObj.GetComponent<Button>().onClick.AddListener(delegate
                    {
                        goal.Skip();

                        countObj.SetActive(false);
                        skipObj.SetActive(false);
                        //goalObj.transform.Find("Done").gameObject.SetActive(true);
                    });
                }

                coinTxt.text = quest.Reward._currency.ToString() + " coin";
            }
        }

        public void CloseWindow()
        {
            gameObject.SetActive(false);

            for (int i = 0; i < goalContent.childCount; i++)
            {
                Destroy(goalContent.GetChild(i).gameObject);
            }
        }
    }
}
