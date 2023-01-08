using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameRoot : MonoBehaviour
{
    public UIManager uiManager;
    public SceneController sceneController;
    public static GameRoot Instance { get; private set; }
    [SerializeField]
    private MainPanel mainPanel;
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        if (uiManager == null)
            uiManager = new UIManager();
        uiManager.canvas=UIHelper.Instance.FindCanvas();
        if (sceneController == null)
            sceneController = new SceneController();
        //DontDestroyOnLoad(gameObject);
    }
    public void ShowMainPanel()
    {
        mainPanel = new MainPanel();
        uiManager.Push(mainPanel);
    }
    public void ReflashMainPanel()
    {
        mainPanel.Reflash(ShopManager.Instance.coin,GameManager.Instance. age,GameManager.Instance.turn);
    }

    internal void ShowPausePanel()
    {
        PausePanel pausePanel = new PausePanel();
        uiManager.Push(pausePanel);
    }

    internal void ShowShopPanel()
    {
        ShopPanel shopPanel = new ShopPanel();
        uiManager.Push(shopPanel);
    }

    internal void ShowWinPanel()
    {
        WinPanel winPanel = new WinPanel();
        uiManager.Push(winPanel);
    }

    internal void ShowLosePanel()
    {
        LosePanel losePanel = new LosePanel();  
        uiManager.Push(losePanel);
    }
}
