using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatientAction : GAction
{
    private GameObject _resource;

    public override bool PostPerform()
    {
        GWorld.Instance.World.ModifyState("PatientIsWaiting", -1);

        if (target != null)
        {
            target.GetComponent<GAgent>().inventory.AddItem(_resource);
        }

        return true;
    }

    public override bool PrePerform()
    {
        target = GWorld.Instance.RemovePatient();

        if (target == null) return false; //if target is not found, the plan fails

        _resource = GWorld.Instance.RemoveCubicle(); //get reference to the cubicle only when target is set

        if(_resource != null)
        {
            inventory.AddItem(_resource);
        }
        else
        {
            GWorld.Instance.AddPatient(target);
            target = null;

            return false;
        }


        GWorld.Instance.World.ModifyState("FreeCubicle", -1); //reduce the count of cubicles

        return true;
    }
}
