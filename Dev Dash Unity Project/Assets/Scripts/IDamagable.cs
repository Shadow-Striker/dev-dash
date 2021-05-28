using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    //This interface is for gameObjects that can take damage/have health.
    //Interfaces as you can use them to ensure certain classes have a specific set of variables
    //specified in the interface.
    //If you are looking for a specific variable/function in another gameObject, instead of doing multiple checks for
    // potential classes that contain a specific variable/function, you can just check if the gameObject
    //contains a specific interface, saving a lot of time.

    int StartingHealth { get; }
    int Health { get; set; }
    //This is for when the player gets hit by a car.
    //They are immune from taking damage for a bit to make the game easier.
    bool DamageImmunity { get; }

    void TakeDamage(int _damage);
}
