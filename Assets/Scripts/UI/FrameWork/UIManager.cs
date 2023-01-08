using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager
{
    public Stack<BasePanel> panelStack;
    public Dictionary<string, GameObject> panelDictionary;
    //private static UIManager _instance;
    public GameObject canvas;
    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if(_instance == null)
    //            _instance = new UIManager();
    //        return _instance;
    //    }
    //}
    public UIManager()
    {
        //_instance = this;
        panelStack = new Stack<BasePanel>();
        panelDictionary = new Dictionary<string, GameObject>();
    }
    public void Push(BasePanel basePanel)
    {
        if (panelStack.Count == 0)
        {
            panelStack.Push(basePanel);
        }
        else if (panelStack.Peek().uiType.Name == basePanel.uiType.Name) 
            return;
        else 
        {
            panelStack.Peek().OnDisable();
            panelStack.Push(basePanel);
        }
        GameObject go = GetSingleObj(basePanel.uiType);
        panelDictionary.Add(basePanel.uiType.Name, go);
        basePanel.panelObj = go;
        basePanel.OnStart();
    }
    public void Pop()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnDisable();
            panelStack.Peek().OnDestory();
            GameObject.Destroy(panelDictionary[panelStack.Peek().uiType.Name]);
            panelDictionary.Remove(panelStack.Peek().uiType.Name);
            panelStack.Pop();
            if (panelStack.Count > 0)
            {
                panelStack.Peek().OnEnable();
            }
        }
    }
    public void Clear()
    {
        while (panelStack.Count > 0)
        {
            Pop();
        }
    }
    public GameObject GetSingleObj(UIType type)
    {
        if (panelDictionary.ContainsKey(type.Name))
        {
            return panelDictionary[type.Name];
        }
        if (canvas == null)
        {
            canvas = UIHelper.Instance.FindCanvas();
        }
        return PanelLoader.Instance.FindPanel(type, canvas.transform);
        //InstantiateAddress(type.Name, canvas.transform);
        //GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(type.Path).Result, canvas.transform );
    }

    private GameObject InstantiateAddress(string name, Transform transform)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(name);
        GameObject go = op.WaitForCompletion();
        var go2 = GameObject.Instantiate(go, transform);
        Addressables.Release(op);
        return go2;
    }
}
