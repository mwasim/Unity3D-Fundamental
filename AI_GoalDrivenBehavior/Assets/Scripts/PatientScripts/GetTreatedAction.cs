using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTreatedAction : GAction
{
    public override bool PrePerform()
    {
        target = inventory.FindItemWithTag("Cubicle");

        if (target == null) return false; //if target is not found, the plan fails

        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.World.ModifyState("Treated", 1);
        beliefs.ModifyState("isCured", 1);
        inventory.RemoveItem(target); //once the patient is treated, remove the cubicle       

        return true;
    }
}
