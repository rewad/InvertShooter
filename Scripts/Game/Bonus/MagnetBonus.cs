using UnityEngine;
using System.Collections;
using System;

public class MagnetBonus : IBonus
{
    public override void Action()
    {
        Gold[] golds = FindObjectsOfType<Gold>();
        for(int i =0;i < golds.Length;i++)
        {
            golds[i].Action();
        }
    }
     
}
