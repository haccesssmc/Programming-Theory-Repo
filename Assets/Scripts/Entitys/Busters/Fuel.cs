using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Buster
{
    [SerializeField] float engineBustMult = 1.5f;
    [SerializeField] float rotationBustMult = 1.2f;
    [SerializeField] float engineBustTime = 30;

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
