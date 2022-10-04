using System;
using System.Collections.Generic;
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

        bool CanDoAction(GameAction p_action)
        {
            foreach (KeyValuePair<string, int> l_value in p_action.PreConditions)
            {
                if (m_values[l_value.Key] != l_value.Value) return false;
            }

            return true;
        }

        void CommitAction(GameAction p_action)
        {
            foreach (KeyValuePair<string, int> l_value in p_action.PostConditions)
            {
                m_values[l_value.Key] = l_value.Value;
            }
        }

        Environment CloneFromAction(GameAction p_action)
        {
            Environment l_new = this.Clone() as Environment;
            l_new.CommitAction(p_action);

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
    }
}