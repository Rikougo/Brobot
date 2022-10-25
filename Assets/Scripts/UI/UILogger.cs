using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI;
using Controllers;
using TMPro;
using UnityEngine;

public class UILogger : MonoBehaviour
{
    public TextMeshProUGUI prefab;
    public int maxLine = 6;

    private List<TextMeshProUGUI> m_currentChildren;

    private void Awake()
    {
        m_currentChildren = new List<TextMeshProUGUI>();
    }

    private void Start()
    {
        this.PrintHelp();
    }

    public void PrintHelp()
    {
        this.AddMessage("$> Welcome to the first Level. You can move your red agent by using Z/Q/S/D, I own the yellow one.");
        this.AddMessage("$> You can switch between cameras using A/E.");
        this.AddMessage("$> Finally you can interact with SPACE, and press ? to print the help again.");
    }

    public void OnAction(Entity p_from, GameAction p_action)
    {
        this.AddMessage($"$> {p_from.Name} did {p_action.Title}.");
    }

    private void AddMessage(string p_message)
    {
        TextMeshProUGUI l_object = Instantiate(prefab, this.transform);
        l_object.text = p_message;

        m_currentChildren.Add(l_object);

        if (m_currentChildren.Count > maxLine)
        {
            Destroy(m_currentChildren.First().gameObject);

            m_currentChildren.RemoveAt(0);
        }
    }
}