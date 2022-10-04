using System;
using System.Collections.Generic;

[Serializable]
public class Environment : ICloneable
{
    private Dictionary<string, int> m_values;

    public Dictionary<string, int> Values => m_values;

    public Environment()
    {
        m_values = new Dictionary<string, int>();
    }

    bool CanDoAction(GameAction p_action)
    {
        foreach (KeyValuePair<string, int> l_value in p_action.m_preconditions)
        {
            if (m_values[l_value.Key] != l_value.Value) return false;
        }

        return true;
    }

    void CommitAction(GameAction p_action)
    {
        foreach (KeyValuePair<string, int> l_value in p_action.m_postconditions)
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
            m_values = new Dictionary<string, int>(this.m_values)
        };

        return l_new;
    }
}