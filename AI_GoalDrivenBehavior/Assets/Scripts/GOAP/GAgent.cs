using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SubGoal
{
    public Dictionary<string, int> subGoals;
    public bool remove; //after the goal is satisfied, it should be removed

    public SubGoal(string goalName, int priority, bool r)
    {
        subGoals = new Dictionary<string, int>
        {
            { goalName, priority }
        };

        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;

    // Start is called before the first frame update
    protected void Start()
    {
        GAction[] acts = GetComponents<GAction>();

        foreach (var act in acts)
        {
            actions.Add(act);
        }
    }

    bool invoked = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            var distanceToTarget = Vector3.Distance(currentAction.target.transform.position, transform.position); //actual distance
            //if (currentAction.agent.hasPath && distanceToTarget < 2f) //currentAction.agent.remainingDistance < 0.5f sometimes causes problems, so we're using distanceToTarget
            if (currentAction.agent.hasPath && distanceToTarget < 2f) //currentAction.agent.remainingDistance < 0.5f sometimes causes problems, so we're using distanceToTarget
            {
                if (!invoked)
                {
                    Invoke(nameof(CompleteAction), currentAction.duration);
                    invoked = true;
                }
            }

            return;
        }

        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goals orderby entry.Value descending select entry;

            foreach (var g in sortedGoals)
            {
                actionQueue = planner.Plan(actions, g.Key.subGoals, beliefs);

                if (actionQueue != null)
                {
                    currentGoal = g.Key;
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goals.Remove(currentGoal);
            }

            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();

            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && !string.IsNullOrWhiteSpace(currentAction.targetTag))
                    currentAction.target = GameObject.FindWithTag(currentAction.targetTag);

                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    currentAction.agent.SetDestination(currentAction.target.transform.position); //Set agent's destination
                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }

    private void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();

        invoked = false;
    }
}
