using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnnemy
{
    int hitPoint { get; }
    int moveSpeed { get; set; }

    public void TakeDamage(int amount);
}
