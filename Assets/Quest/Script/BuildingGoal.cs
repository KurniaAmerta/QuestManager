using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quest
{
    public class BuildingGoal : Quest.QuestGoal
    {

        public override string GetDescription()
        {
            return $"Build a {_building}";
        }

        public override void Initialize()
        {
            base.Initialize();

            EventManager.Instance.AddListener<BuildingGameEvent>(OnBuilding);
        }

        private void OnBuilding(BuildingGameEvent eventInfo)
        {
            if (eventInfo._buildingName == _building)
            {
                _currentAmount++;
                Evaluate();
            }
        }


    }
}
