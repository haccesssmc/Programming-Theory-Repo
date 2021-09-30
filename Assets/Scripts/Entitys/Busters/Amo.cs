using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the Buster class
public class Amo : Buster
{
    [SerializeField] int rocketsCount = 5;

    // ABSTRACTION
    // implemented an abstract method in the children class
    protected override void GiveBust(Player player)
    {
        player.rockets += rocketsCount;
        player.health = player.healthMax;
        player.amo = player.amoMax;
    }
}
