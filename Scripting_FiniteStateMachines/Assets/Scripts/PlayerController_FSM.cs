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
public class PlayerController_FSM : MonoBehaviour //This class is Context for FSM - and maintains reference for CURRENT STATE
{
    #region Player Variables

    public float jumpForce;
    public Transform head;
    public Transform weapon01;
    public Transform weapon02;

    public Sprite idleSprite;
    public Sprite duckingSprite;
    public Sprite jumpingSprite;
    public Sprite spinningSprite;

    private SpriteRenderer face;
    private Rigidbody rbody;

    public Rigidbody Rigidbody => rbody;

    #endregion

    /*
        FSM - holds reference to the instance of the player's state
     */
    private PlayerBaseState _currentState;
    public PlayerBaseState CurrentState => _currentState; //Expose the Current State as public (read only)

    /*
        FSM - we need instances of the states to transition to
    */
    public readonly PlayerIdleState IdleState = new PlayerIdleState(); //initial state of the FSM
    public readonly PlayerJumpingState JumpingState = new PlayerJumpingState();
    public readonly PlayerDuckingState DuckingState = new PlayerDuckingState();

    private void Awake()
    {
        face = GetComponentInChildren<SpriteRenderer>();
        rbody = GetComponent<Rigidbody>();
        SetExpression(idleSprite);
    }

    private void Start()
    {
        /*
            FSM - Set the initial state
         */
        TransitionToState(IdleState);
    }

    // Update is called once per frame
    void Update()
    {
        /*
            FSM - UPDATE state
         */
        _currentState.Update(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
            FSM - OnCollisionEnter state
            Instead of writing any behavioral code, we're delegating the responsibility to the FSM by calling OnCollisionEnter on the _currentState
         */
        _currentState.OnCollisionEnter(this);
    }


    /*
        FSM - Transitions to a particular state
     */
    public void TransitionToState(PlayerBaseState state)
    {
        _currentState = state;

        _currentState.EnterState(this);
    }

    public void SetExpression(Sprite newExpression)
    {
        face.sprite = newExpression;
    }
}
