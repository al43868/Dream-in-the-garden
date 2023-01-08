using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : BasePanel
{
    private static string _name = "ShopPanel";
    private static string _path = "ShopPanel";
    private static UIType _type = new UIType(_name, _path);
    public ShopPanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        if (ShopManager.Instance.GetStone) UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy1").gameObject.SetActive(false);
        else UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy1").onClick.AddListener(BuyGetStone);
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy2").onClick.AddListener(BuyaExplosive);
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "BackButton").onClick.AddListener(Back);
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy3").onClick.AddListener(BuyPlantPlus);
        UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "PlantPlusNumber").text = "Plus 5 plants  current:" + ShopManager.Instance.plantMaxNumber;
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy4").onClick.AddListener(BuyPlantUp);
        UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "PlantInsNumber").text = "Plus 1 plant formation  current:" + ShopManager.Instance.plantInsNumber + " / 3";
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Win").onClick.AddListener(Win);
        if (ShopManager.Instance.havePumpkin) UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy5").gameObject.SetActive(false);
        else UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy5").onClick.AddListener(BuyPlant1);
        if (ShopManager.Instance.haveEpiphyllum) UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy6").gameObject.SetActive(false);
        else UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy6").onClick.AddListener(BuyPlant2);
        if (ShopManager.Instance.haveGreenOnion) UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy7").gameObject.SetActive(false);
        else UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Buy7").onClick.AddListener(BuyPlant3);
    }

    private void BuyPlant3()
    {
        ShopManager.Instance.BuyPlant3();
    }

    private void BuyPlant2()
    {
        ShopManager.Instance.BuyPlant2();
    }

    private void BuyPlant1()
    {
        ShopManager.Instance.BuyPlant1();
    }

    private void Win()
    {
        ShopManager.Instance.Win();
    }

    private void BuyPlantUp()
    {
        ShopManager.Instance.BuyPlantIns();
    }
    private void Back()
    {
        GameManager.Instance.BackPlay();
    }
    private void BuyPlantPlus()
    {
        ShopManager.Instance.BuyPlantPlus();
    }
    private void BuyaExplosive()
    {
        ShopManager.Instance.BuyOneExplosive();
    }
    private void BuyGetStone()
    {
        ShopManager.Instance.BuyGetStoneUp();
    }
}
