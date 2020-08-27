using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : GAgent
{
    // Start is called before the first frame update
    private void Start()
    {
        base.Start();

        goals.Add(new SubGoal("treatPatient", 1, false), 3); //not removeable as nurse will continue to pick up patient for treatment        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
