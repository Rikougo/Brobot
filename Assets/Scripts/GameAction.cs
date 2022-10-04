using System;
using System.Collections.Generic;

[Serializable]
public struct GameAction
{
    public Dictionary<string, int> m_preconditions;
    public Dictionary<string, int> m_postconditions;

    public string m_title;
}