using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Unit
{
    public override void Get()
    {
        base.Get();
        GameManager.Instance.GetStoneEff(this);
        GameManager.Instance.PlayEff("boom");
        if (ShopManager.Instance.GetStone)
        {
            ShopManager.Instance.GetCoin(100);
            GameRoot.Instance.ReflashMainPanel();
        }
        Destroy(this.gameObject);
    }
}
