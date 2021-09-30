using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// INHERITANCE
// inherited from the Buster class
public class Fuel : Buster
{
    [SerializeField] float engineBustMult = 1.5f;
    [SerializeField] float rotationBustMult = 1.2f;
    [SerializeField] float engineBustTime = 30;


    // ABSTRACTION
    // implemented an abstract method in the children class
    protected override void GiveBust(Player player)
    {
        if (player.engineBustTimer <= 0)
        {
            player.enginePower *= engineBustMult;
            player.rotationSpeed *= rotationBustMult;
        }

        player.engineBustTimer += engineBustTime;
    }
}
