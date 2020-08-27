using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToWaitingRoomAction : GAction
{
    public override bool PostPerform()
    {
        GWorld.Instance.World.ModifyState("PatientIsWaiting", 1);
        GWorld.Instance.AddPatient(gameObject);
        beliefs.ModifyState("atHospital", 1);

        return true;
    }

    public override bool PrePerform()
    {        
        return true;
    }
}
