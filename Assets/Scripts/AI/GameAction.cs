using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameAction", menuName = "Bro-Bot/GameAction", order = 1)]
    public class GameAction : ScriptableObject
    {
        [SerializeField] private Dictionary<string, int> m_preConditions;
        [SerializeField] private Dictionary<string, int> m_postConditions;

        [SerializeField] private string m_title;

        public string Title => m_title;
        public Dictionary<string, int> PreConditions => m_preConditions;
        public Dictionary<string, int> PostConditions => m_postConditions;
    }
}