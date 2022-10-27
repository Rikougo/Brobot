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
            bool just_as_planned = false;
            if(action == next?.action && next?.self == (actor == Agent))
            {
                m_plan.RemoveAt(0);
                just_as_planned = true;
            }
            if(m_plan.Count == 0 || !just_as_planned)
            {
                m_plan = PlanWithAction(action, actor, m_environment);
            }
            m_environment.CommitAction(action, actor);
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
        List<PlannedAction> PlanWithAction(GameAction action, Entity player, Environment environment, int max_iter=1000)
        {
            List<GameAction> all_actions = ListAllActions();
            List<SearchNode>[] explorations = {new List<SearchNode>{new SearchNode{environment = environment}}, new List<SearchNode>{new SearchNode{environment = environment.CloneFromAction(action, player)}}};
            for(int i = 0; i < explorations[1].Count && max_iter > 0; ++i, --max_iter)
            {
                foreach(GameAction a in all_actions)
                {
                    foreach(Entity actor in new Entity[2]{Agent, player})
                    {
                        if(!explorations[1][i].environment.CanDoAction(a, actor))
                        {
                            continue;
                        }
                        if(!explorations[0][i].environment.CanDoAction(a, actor))
                        {
                            var plan = new List<PlannedAction>();
                            plan.Add(new PlannedAction{action = a, self = actor == Agent});
                            int current = i;
                            while(explorations[1][current].previous != null)
                            {
                                plan.Add(explorations[1][current].action.Value);
                                current = explorations[1][current].previous.Value;
                            }
                            plan.Reverse();
                            if(actor == Agent)
                            {
                                plan.AddRange(PlanWithAction(a, player, explorations[1][i].environment, max_iter));
                            }
                            return plan;
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
            return new List<PlannedAction>();
        }
    }
}
