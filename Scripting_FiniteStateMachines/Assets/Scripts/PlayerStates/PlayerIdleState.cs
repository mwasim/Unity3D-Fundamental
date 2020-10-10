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
public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController_FSM player)
    {
        player.SetExpression(player.idleSprite);
    }

    public override void OnCollisionEnter(PlayerController_FSM player)
    {

    }

    public override void Update(PlayerController_FSM player)
    {
        if (Input.GetButtonDown("Jump"))
        {
            player.Rigidbody.AddForce(Vector3.up * player.jumpForce);

            player.TransitionToState(player.JumpingState);
        }

        if (Input.GetButtonDown("Duck"))
        {            
            player.TransitionToState(player.DuckingState);
        }

        if (Input.GetButtonDown("SwapWeapon"))
        {
            //if (!_isJumping && !_isDucking && !_isSpinning) //if player is not in any of these commented states, the player must be in the Idle state that's why this check is added here

            var usingWeapon01 = player.weapon01.gameObject.activeInHierarchy;

            player.weapon01.gameObject.SetActive(usingWeapon01 == false);
            player.weapon02.gameObject.SetActive(usingWeapon01);
        }
    }
}
