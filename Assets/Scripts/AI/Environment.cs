using System;
using System.Collections.Generic;
using Controllers;

namespace AI
{
    [Serializable]
    public class Environment : ICloneable
    {
        private Dictionary<string, int> m_values;

        public Dictionary<string, int> Values => m_values;

        public Environment()
        {
            m_values = new Dictionary<string, int>();
        }

        public bool CanDoAction(GameAction p_action, Entity actor)
        {
            foreach (KeyValuePair<string, int> l_value in p_action.PreConditions)
            {
                if (m_values[FixKey(l_value.Key, actor)] != l_value.Value) return false;
            }

            return true;
        }

        public void CommitAction(GameAction p_action, Entity actor)
        {
            foreach (KeyValuePair<string, int> l_value in p_action.PostConditions)
            {
                m_values[FixKey(l_value.Key, actor)] = l_value.Value;
            }
        }

        public Environment CloneFromAction(GameAction p_action, Entity actor)
        {
            Environment l_new = this.Clone() as Environment;
            l_new.CommitAction(p_action, actor);

            return l_new;
        }

        public object Clone()
        {
            Environment l_new = new Environment
            {
                m_values = new Dictionary<string, int>(this.m_values)
            };

            return l_new;
        }

        string FixKey(string key, Entity actor)
        {
            return key.Replace("%", actor.Type.ToString());
        }
    }
}