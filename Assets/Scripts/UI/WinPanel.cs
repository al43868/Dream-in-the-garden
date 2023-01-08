using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : BasePanel
{
    private static string _name = "WinPanel";
    private static string _path = "WinPanel";
    private static UIType _type = new UIType(_name, _path);
    public WinPanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        GameManager.Instance.PlayEff("win");
        GameManager.Instance.all.Stop();
        UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "TM").text = "TotalMoney: " + ShopManager.Instance.allCoin;
        UIHelper.Instance.AddOrGetComponentInChild<TMP_Text>(panelObj, "Mounth").text =((GameManager.Instance.age-30)*12+ GameManager.Instance.turn-1).ToString()+ "months passed,He was finally on the trip of his dreams!";
    }

}
