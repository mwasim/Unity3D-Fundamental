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
public abstract class PlayerBaseState //Abstract State
{
    /*
        NOTE: We can pass any params depending upon what we intend to do with these params
     */
    public abstract void EnterState(PlayerController_FSM player); //PlayerController_FSM is acting as a Context for the State Machine

    public abstract void Update(PlayerController_FSM player);

    public abstract void OnCollisionEnter(PlayerController_FSM player);
}
