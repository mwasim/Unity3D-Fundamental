using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    // Start is called before the first frame update
    private void Start()
    {
        base.Start();

        goals.Add(new SubGoal("treatPatient", 1, true), 3);
        //goals.Add(new SubGoal("hasRegistered", 1, true), 3);
        //goals.Add(new SubGoal("isWaiting", 1, true), 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
