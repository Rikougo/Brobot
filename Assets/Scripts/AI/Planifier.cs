using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace AI
{
    public class Planifier
    {
        public struct PlannedAction
        {
            public GameAction action;
            public bool self;
        }
        public List<GameAction> AllActions { get; set; }
        public Entity Agent{ get; set; }
        private List<PlannedAction> m_plan = new List<PlannedAction>();
        private Environment m_environment = new Environment();

        public Environment Environment => m_environment;

        public PlannedAction? NextAction
        {
            get
            {
                return m_plan.Count > 0 ? m_plan[0] : null;
            }
        }

        public void SeeAction(Entity actor, GameAction action)
        {
            PlannedAction? next = NextAction;
            if(action == next?.action && next?.self == (actor == Agent))
            {
                m_plan.RemoveAt(0);
                m_environment.CommitAction(action, actor);
            }
            else if (actor != Agent)
            {
                PlanWithAction(action, actor);
            }
            else
            {
                m_plan.Clear();     
                m_environment.CommitAction(action, actor);
            }
        }

        List<GameAction> ListAllActions()
        {
            return AllActions;
        }

        struct SearchNode
        {
            public Environment environment;
            public PlannedAction? action;
            public int? previous;
        }
        void PlanWithAction(GameAction action, Entity player)
        {
            List<GameAction> all_actions = ListAllActions();
            List<SearchNode>[] explorations = {new List<SearchNode>{new SearchNode{environment = m_environment}}, new List<SearchNode>{new SearchNode{environment = m_environment.CloneFromAction(action, player)}}};
            m_environment = explorations[1][0].environment;
            for(int i = 0; i < explorations[1].Count && i < 1000; ++i) //TODO : limite de profondeur?
            {
                foreach(GameAction a in all_actions)
                {
                    foreach(Entity actor in new Entity[2]{Agent, player})
                    {
                        if(!explorations[1][i].environment.CanDoAction(a, actor)) continue;
                        if(!explorations[0][i].environment.CanDoAction(a, actor))
                        {
                            //TODO : found!
                            m_plan.Clear();
                            m_plan.Add(new PlannedAction{action = a, self = actor == Agent});
                            int current = i;
                            while(explorations[1][current].previous != null)
                            {
                                m_plan.Add(explorations[1][current].action.Value);
                                current = explorations[1][current].previous.Value;
                            }
                            m_plan.Reverse();
                            return;
                        }
                        for(int j = 0; j < 2; ++j)
                        {
                            explorations[j].Add(new SearchNode{
                                environment = explorations[j][i].environment.CloneFromAction(a, actor),
                                action = new PlannedAction{action = a, self = actor == Agent},
                                previous = i
                                });
                        }
                    }
                }
            }
            Debug.Log("Fail");
        }
    }
}
