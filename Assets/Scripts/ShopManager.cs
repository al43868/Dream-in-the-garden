using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : SingleTion<ShopManager>
{
    public int plantMaxNumber;
    public int coin;
    public bool GetStone;
    public int plantInsNumber;
    public int allCoin;
    public Plant Epiphyllum, Pumpkin, GreenOnion;
    public bool haveEpiphyllum, havePumpkin, haveGreenOnion;
    public void GetCoin(int i)
    {
        coin += i;
        allCoin += i;
        GameManager.Instance.PlayEff("coin");
        GameManager.Instance.PleyEffect("Coin", i);
    }

    internal void BuyOneExplosive()
    {
        if (coin >= 50)
        {
            coin -= 50;
            GameManager.Instance.PlayEff("Button");
            GameManager.Instance.explosiveCount++;
            GameRoot.Instance.ReflashMainPanel();
        }
    }

    internal void BuyGetStoneUp()
    {
        if(coin >= 150 && !GetStone)
        {
            coin -= 150; GameManager.Instance.PlayEff("Button");

            GameRoot.Instance.ReflashMainPanel();
            GetStone = true;
        }
    }

    internal void BuyPlantPlus()
    {
        if (coin >= 70)
        {
            coin -= 70; GameManager.Instance.PlayEff("Button");

            plantMaxNumber += 5;
            GameRoot.Instance.ReflashMainPanel();
        }
    }

    internal void BuyPlantIns()
    {
        if (coin >= 100&&plantInsNumber<3)
        {
            coin -= 100; GameManager.Instance.PlayEff("Button");

            plantInsNumber++;
            GameRoot.Instance.ReflashMainPanel();
        }
    }

    internal void Win()
    {
        if (coin >= 1000)
        {
            GameManager.Instance.Win();
        }
    }

    internal void BuyPlant3()
    {
        if (coin >= 80 && !haveGreenOnion)
        {
            coin -= 80; GameManager.Instance.PlayEff("Button");

            GameManager.Instance.mapManager.plantPrefabs.Add(GreenOnion);
            haveGreenOnion = true;
            GameRoot.Instance.ReflashMainPanel();
        }
    }

    internal void BuyPlant2()
    {
        if (coin >= 140 && !haveEpiphyllum)
        {
            coin -= 140; GameManager.Instance.PlayEff("Button");

            GameManager.Instance.mapManager.plantPrefabs.Add(Epiphyllum);
            haveEpiphyllum = true;
            GameRoot.Instance.ReflashMainPanel();
        }
    }

    internal void BuyPlant1()
    {
        if (coin >= 120&&!havePumpkin)
        {
            coin -= 120; GameManager.Instance.PlayEff("Button");

            GameManager.Instance.mapManager.plantPrefabs.Add(Pumpkin);
            havePumpkin = true;
            GameRoot.Instance.ReflashMainPanel();
        }
    }
}
