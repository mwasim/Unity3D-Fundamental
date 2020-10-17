﻿using UnityEngine;[CreateAssetMenu(fileName = "Spell.asset", menuName = "Spell")]public class Spell : AttackDefinition{    public Projectile Projectile;    public float ProjectileSpeed;    public void Cast(GameObject caster, Vector3 hotSpot, Transform target, int layer)    {        Projectile proj = Instantiate(Projectile, hotSpot, Quaternion.identity);        proj.gameObject.layer = layer;        proj.Fire(this, caster, target);    }}