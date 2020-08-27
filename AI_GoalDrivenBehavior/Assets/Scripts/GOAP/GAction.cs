using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    Each Action has Name, and cost
 */
public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;
    public GameObject target;
    public string targetTag; //find based on the tag
    public float duration = 0; //how long
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> preConditionsDic; //values populated by the inspector in WorldState[] above will be put into these dictionaries
    public Dictionary<string, int> afterEffectsDic;

    public WorldStates agentBeliefs;

    public GInventory inventory;
    public WorldStates beliefs;

    public bool running = false;

    public GAction()
    {
        //initialize dictionaries
        preConditionsDic = new Dictionary<string, int>();
        afterEffectsDic = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();

        //populate pre-conditions dictionary
        if (preConditions != null)
        {
            foreach (var w in preConditions)
            {
                preConditionsDic.Add(w.key, w.value);
            }
        }

        //populate affer-effects dictionary
        if (afterEffects != null)
        {
            foreach (var w in afterEffects)
            {
                afterEffectsDic.Add(w.key, w.value);
            }
        }

        inventory = GetComponent<GAgent>().inventory;
        beliefs = GetComponent<GAgent>().beliefs;
    }

    //Helper methods
    public bool IsAchievable => true;    

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (var pc in preConditionsDic)
        {
            if (!conditions.ContainsKey(pc.Key)) return false;
        }

        return true;
    }

    //abstract methods
    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
