using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int StartingHealth { get; }
    int Health { get; set; }
    bool DamageImmunity { get; }

    void TakeDamage(int _damage);
}
