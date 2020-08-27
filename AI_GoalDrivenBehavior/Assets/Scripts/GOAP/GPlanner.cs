using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent; //parent of this node
    public float cost;
    public Dictionary<string, int> states;
    public GAction action;

    public Node(Node theParent, float theCost, Dictionary<string, int> allStates, GAction theAction)
    {
        parent = theParent;
        cost = theCost;
        action = theAction;

        states = new Dictionary<string, int>(allStates); //we need copy of allStates not reference to it that's why we've created new dictionary instead of assing it       
    }
}

public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goals, WorldStates states)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach (var action in actions)
        {
            if (action.IsAchievable)
                usableActions.Add(action);
        }

        var leaves = new List<Node>();
        var start = new Node(null, 0, GWorld.Instance.World.States, null);

        bool success = BuildGraph(start, leaves, usableActions, goals);

        if (!success)
        {
            Debug.Log("NO PLAN");

            return null;
        }

        Node cheapest = null;
        foreach (var leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }


        var result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }

            n = n.parent;
        }


        Queue<GAction> queue = new Queue<GAction>();
        foreach (var a in result)
        {
            queue.Enqueue(a);
        }

        //So, as we've reached here, obviously we've a plan
        Debug.Log("The Plan is: ");
        foreach (var a in queue)
        {
            Debug.Log($"Q: {a.actionName}");
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach (var a in usableActions)
        {
            if (a.IsAchievableGiven(parent.states))
            {
                var currentState = new Dictionary<string, int>(parent.states);

                foreach (var effect in a.afterEffectsDic)
                {
                    if (!currentState.ContainsKey(effect.Key))
                    {
                        currentState.Add(effect.Key, effect.Value);
                    }
                }

                var node = new Node(parent, parent.cost + a.cost, currentState, a);

                if(IsGoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else //path not found
                {
                    var subset = ActionSubset(usableActions, a);
                    var found = BuildGraph(node, leaves, subset, goal); //Recursive call, and usableActions getting smaller and smaller into subset

                    if (found)
                    {
                        foundPath = true;
                    }
                }
            }
        }

        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> usableActions, GAction actionToBeRemoved)
    {
        List<GAction> subset = new List<GAction>();

        foreach (var a in usableActions)
        {
            if (!a.Equals(actionToBeRemoved))
            {
                subset.Add(a);
            }         
        }

        return subset;
    }

    private bool IsGoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> currentState)
    {
        foreach (var g in goal)
        {
            if (!currentState.ContainsKey(g.Key)) return false;
        }

        return true;
    }
}
