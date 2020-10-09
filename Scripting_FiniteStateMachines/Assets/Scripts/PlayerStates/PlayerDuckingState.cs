using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    FSM has
    1. Context
    2. Abstract State
    3. Concrete implementation of the Abstract state

    We know the player can be in one of the below 4 states, each of these should be represented in code with Concrete State
    1. Idle
    2. Ducking
    3. Jumping
    4. Spinning

    Definitions:
    - Context: - Maintains an instance of a concrete state as the current state
    - Abstract State - Defines and interface which encapsulates behaviors common to all concrete states
    - Concrete State - Implements behaviors specific to a particular state of context
    
 */
public class PlayerDuckingState : PlayerBaseState //Concrete implementation
{
    public override void EnterState(PlayerController_FSM player)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(PlayerController_FSM player)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(PlayerController_FSM player)
    {
        throw new System.NotImplementedException();
    }
}
