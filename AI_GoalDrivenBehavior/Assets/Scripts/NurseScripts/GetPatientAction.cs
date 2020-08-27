using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatientAction : GAction
{
    public override bool PostPerform()
    {       
        return true;
    }

    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient();

        if (target == null) return false; //if target is not found, the plan fails

        return true;
    }
}
