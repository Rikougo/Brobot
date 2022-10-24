using System;
using System.Collections.Generic;
using Controllers;
using Utils;

namespace AI
{
    [Serializable]
    public class Environment : ICloneable
    {
        private EnvironmentState m_values;
        public EnvironmentState Values => m_values;

        public Environment()
        {
            m_values = new EnvironmentState();
        }

        public bool CanDoAction(GameAction p_action, Entity actor)
        {
            foreach (KeyValuePair<string, int> l_value in p_action.PreConditions)
            {
                if (m_values.GetValueOrDefault(FixKey(l_value.Key, actor), 0) != l_value.Value) return false;
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
                // m_values = new EnvironmentState(this.m_values)
            };

            return l_new;
        }

        string FixKey(string key, Entity actor)
        {
            return key.Replace("%", actor.Type.ToString());
        }
    }
}