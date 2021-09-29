using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amo : Buster
{
    [SerializeField] int rocketsCount = 5;

    protected override void GiveBust(Player player)
    {
        player.rockets += rocketsCount;
        player.health = player.healthMax;
        player.amo = player.amoMax;
    }
}
