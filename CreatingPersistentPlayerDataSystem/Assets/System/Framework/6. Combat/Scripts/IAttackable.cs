using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    int OnAttack(GameObject attacker, Attack attack);
}
