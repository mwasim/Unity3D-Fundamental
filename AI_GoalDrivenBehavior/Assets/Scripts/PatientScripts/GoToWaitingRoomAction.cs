using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoomAction : GAction
{
    public override bool PostPerform()
    {
        GWorld.Instance.World.ModifyState("PatientIsWaiting", 0);
        GWorld.Instance.AddPatient(gameObject);

        return true;
    }

    public override bool PrePerform()
    {        
        return true;
    }
}
