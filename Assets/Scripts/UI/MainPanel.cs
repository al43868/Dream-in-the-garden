using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static string _name = "MainPanel";
    private static string _path = "MainPanel";
    private static UIType _type = new UIType(_name, _path);
    private TMP_Text coinText,ageText,timeText,ExNumber;
    public MainPanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "PauseButton").onClick.AddListener(Pause);
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "ShopButton").onClick.AddListener(ShowShop);
        coinText = UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "CoinNumber");
        ageText = UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "AgeNumber");
        timeText = UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "TimeNumber");
        ExNumber= UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "ExNumber");
    }

    private void ShowShop()
    {
        GameManager.Instance.ShowShopMenu();
    }

    private void Pause()
    {
        GameManager.Instance.PlayEff("Button");
        GameManager.Instance.PauseGame();
    }

    internal void Reflash(int coinNumber,int age,int time)
    {
       coinText.text=coinNumber.ToString();
       ageText.text="Age: "+ age;
        ExNumber.text = GameManager.Instance.explosiveCount.ToString();
        switch (time)
        {
            case 1:
                SetTimeText("Jan");
                break;
            case 2:
                SetTimeText("Feb");
                break;
            case 3:
                SetTimeText("Mar");
                break;
            case 4:
                SetTimeText("Apr");
                break;
            case 5:
                SetTimeText("May");
                break;
            case 6:
                SetTimeText("Jun");
                break;
            case 7:
                SetTimeText("Jul");
                break;
            case 8:
                SetTimeText("Aug");
                break;
            case 9:
                SetTimeText("Sept");
                break;
            case 10:
                SetTimeText("Oct");
                break;
            case 11:
                SetTimeText("Nov");
                break;
            case 12:
                SetTimeText("Dec");
                break;
            default:
                break;
        }
    }
    private void SetTimeText(string str)
    {
        timeText.text = str;
    }
}
