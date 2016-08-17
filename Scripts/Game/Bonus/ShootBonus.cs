using UnityEngine;
using System.Collections;
using System;

public class ShootBonus : IBonus {
    public override void Action()
    {
        GameInstance.GetInstance().GetPlayer().CircleShoot();
    }

    
}
