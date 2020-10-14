using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedForce : MonoBehaviour, IAttackable
{
    public float forceToAdd;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        //vector from target position to our position
        var forceDirection = transform.position - attacker.transform.position;

        //add little lift on Y-axis
        forceDirection.y += 0.5f;

        forceDirection.Normalize(); //normalize

        //add force to the rigidbody
        _rigidbody.AddForce(forceDirection * forceToAdd);
    }    
}
