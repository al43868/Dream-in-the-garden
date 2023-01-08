using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : BasePanel
{
    private static string _name = "LosePanel";
    private static string _path = "LosePanel";
    private static UIType _type = new UIType(_name, _path);
    public LosePanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
        GameManager.Instance.all.Stop();
    }
}
