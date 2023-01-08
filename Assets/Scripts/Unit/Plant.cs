using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Unit
{
    public Unit nextLevel;
    public int coin;
    public int life;
    public int currentLife;
    public override void UpdateUnit()
    {
        base.UpdateUnit();
        currentLife++;
        if(currentLife >= life)
        {
            if(nextLevel != null)
            {
                MapManager.Instance.SetPlant(pos, nextLevel);
                Dead();
            }
            else
            {
                Dead();
            }
        }
    }
    public override void Get()
    {
        base.Get();
        ShopManager.Instance.GetCoin(coin);
        GameRoot.Instance.ReflashMainPanel();
        Dead();
    }
    private void Dead()
    {
        GameManager.Instance.mapManager.units.Remove(this);
        Destroy(this.gameObject);
    }
}
