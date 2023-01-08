using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    private static string _name = "PausePanel";
    private static string _path = "PausePanel";
    private static UIType _type = new UIType(_name, _path);
    public PausePanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "BackButton").onClick.AddListener(Back);
        UIHelper.Instance.AddOrGetComponentInChild<Slider>(panelObj, "all").onValueChanged.AddListener(ChageMusic);
        UIHelper.Instance.AddOrGetComponentInChild<Slider>(panelObj, "clip").onValueChanged.AddListener(ChageEff);
        UIHelper.Instance.AddOrGetComponentInChild<Button>(panelObj, "Change").onClick.AddListener(ChangeMusic);
    }

    private void ChangeMusic()
    {
        GameManager.Instance.PlayEff("Button");

        GameManager.Instance.ChangeMusic();
    }

    private void ChageMusic(float arg0)
    {

        GameManager.Instance.ChangeMusic(arg0);
    }
    private void ChageEff(float arg0)
    {
        GameManager.Instance.ChangeEff(arg0);
    }

    private void Back()
    {
        GameManager.Instance.PlayEff("Button");

        GameManager.Instance.BackPlay();
    }
}
