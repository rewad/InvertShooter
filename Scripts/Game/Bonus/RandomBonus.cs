using UnityEngine;
using System.Collections;
using System;

public class RandomBonus : IBonus
{
    public override void Action()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < 50)
        {
            Gold[] golds = FindObjectsOfType<Gold>();
            for (int i = 0; i < golds.Length; i++)
            {
                golds[i].Action();
            }
        }
        else if (rand > 80)
        {
            GameInstance.GetInstance().GetPlayer().CircleShoot();
        }
        else
        {
            Enemy[] golds = FindObjectsOfType<Enemy>();
            for (int i = 0; i < golds.Length; i++)
            {
                golds[i].Damage();
            }
        }


    }

}
