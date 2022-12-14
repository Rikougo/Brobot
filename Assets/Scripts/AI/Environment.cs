using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
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
            m_values["PlayerisPlayer"] = 1;
            m_values["AgentisAgent"] = 1;
        }

        public bool HasPreconditions(EnvironmentState p_preconditions)
        {
            foreach (KeyValuePair<string, int> l_value in p_preconditions)
            {
                if (m_values.GetValueOrDefault(l_value.Key, 0) != l_value.Value) return false;
            }

            return true;
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
            Environment l_new = new Environment();
            {
                foreach(var kv in m_values)
                {
                    l_new.m_values[kv.Key] = kv.Value;
                }
            };

            return l_new;
        }

        string FixKey(string key, Entity actor)
        {
            return key.Replace("%", actor.Type.ToString());
        }
    }
}