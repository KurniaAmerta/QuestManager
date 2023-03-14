using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace Quest
{
    public class Quest : ScriptableObject
    {
        [System.Serializable]
        public struct Info
        {
            public string _name;
            public Sprite _icon;
            public string _description;
        }

        [Header("Info")] public Info Information;

        [System.Serializable]
        public struct Stat
        {
            public int _currency;
        }

        [Header("Reward")] public Stat Reward = new Stat { _currency = 10 };

        public bool _completed { get; protected set; }
        public QuestCompletedEvent _questCompleted;

        public abstract class QuestGoal : ScriptableObject
        {
            protected string _description;
            public int _currentAmount { get; protected set; }
            public int _requiredAmount = 1;
            public string _building;

            public bool _completed { get; private set; }
            [HideInInspector] public UnityEvent _goalCompleted;

            public void Reset(int amount)
            {
                _currentAmount = amount;
            }

            public virtual string GetDescription()
            {
                return _description;
            }

            public virtual void Initialize()
            {
                _completed = false;
                _goalCompleted = new UnityEvent();
            }

            protected void Evaluate()
            {
                if (_currentAmount >= _requiredAmount)
                {
                    Complete();
                }
            }

            private void Complete()
            {
                _completed = true;
                _goalCompleted.Invoke();
                _goalCompleted.RemoveAllListeners();
            }

            //charge player
            public void Skip()
            {
                Complete();
            }
        }

        public List<QuestGoal> Goals;

        public void Initialize()
        {
            _completed = false;
            _questCompleted = new QuestCompletedEvent();

            foreach (var goal in Goals)
            {
                goal.Initialize();
                goal._goalCompleted.AddListener(delegate { CheckGoals(); });
            }
        }

        private void CheckGoals()
        {
            _completed = Goals.All(g => g._completed);
            if (_completed)
            {
                //give reward
                _questCompleted.Invoke(this);
                _questCompleted.RemoveAllListeners();

                if (PlayerData.instance.IsQuestReady) {
                    PlayerData.instance.AddCoin(Reward._currency);
                }
            }
        }
    }

    public class QuestCompletedEvent : UnityEvent<Quest> { }

#if UNITY_EDITOR
    [CustomEditor(typeof(Quest))]

    public class QuestEditor : Editor
    {
        SerializedProperty m_QuestInfoProperty;
        SerializedProperty m_QuestStatProperty;

        List<string> m_QuestGoalType;
        SerializedProperty m_QuestGoalListProperty;

        [MenuItem("Asset/Quest", priority = 0)]
        public static void CreateQuest()
        {
            var newQuest = CreateInstance<Quest>();
            ProjectWindowUtil.CreateAsset(newQuest, "quest.asset");
        }

        private void OnEnable()
        {
            m_QuestInfoProperty = serializedObject.FindProperty(nameof(Quest.Information));
            m_QuestStatProperty = serializedObject.FindProperty(nameof(Quest.Reward));

            m_QuestGoalListProperty = serializedObject.FindProperty(nameof(Quest.Goals));

            var lookup = typeof(Quest.QuestGoal);
            m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies().
                SelectMany(assembly => assembly.GetTypes()).
                Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup)).
                Select(type => type.Name).ToList();
        }

        public override void OnInspectorGUI()
        {
            var child = m_QuestInfoProperty.Copy();
            var depth = child.depth;
            child.NextVisible(true);

            EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
            while (child.depth > depth)
            {
                EditorGUILayout.PropertyField(child, true);
                child.NextVisible(false);
            }

            child = m_QuestStatProperty.Copy();
            depth = child.depth;
            child.NextVisible(true);

            EditorGUILayout.LabelField("Quest Reward", EditorStyles.boldLabel);
            while (child.depth > depth)
            {
                EditorGUILayout.PropertyField(child, true);
                child.NextVisible(false);
            }

            int choice = EditorGUILayout.Popup("Add new Quest Goal", -1, m_QuestGoalType.ToArray());

            if (choice != -1)
            {
                var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);

                AssetDatabase.AddObjectToAsset(newInstance, target);

                m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
                m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1).objectReferenceValue = newInstance;
            }

            Editor ed = null;
            int toDelete = -1;

            for (int i = 0; i < m_QuestGoalListProperty.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();

                var item = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
                SerializedObject obj = new SerializedObject(item.objectReferenceValue);

                Editor.CreateCachedEditor(item.objectReferenceValue, null, ref ed);

                ed.OnInspectorGUI();
                EditorGUILayout.EndVertical();

                if (GUILayout.Button("-", GUILayout.Width(32)))
                {
                    toDelete = i;
                }
                EditorGUILayout.EndHorizontal();
            }

            if (toDelete != -1)
            {
                var item = m_QuestGoalListProperty.GetArrayElementAtIndex(toDelete).objectReferenceValue;
                DestroyImmediate(item, true);

                m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
                m_QuestGoalListProperty.DeleteArrayElementAtIndex(toDelete);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}