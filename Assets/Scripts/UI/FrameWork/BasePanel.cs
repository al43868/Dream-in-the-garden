using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel 
{
    public UIType uiType;
    public GameObject panelObj;
    public BasePanel(UIType uitype)
    {
        uiType = uitype;
    }
    public virtual void OnStart() 
    {
        UIHelper.Instance.AddOrGetComponent<CanvasGroup>(panelObj).interactable = true;
    }
    public virtual void OnEnable()
    {
        UIHelper.Instance.AddOrGetComponent<CanvasGroup>(panelObj).interactable = true;
    }
    public virtual void OnDisable()
    {
        UIHelper.Instance.AddOrGetComponent<CanvasGroup>(panelObj).interactable =false ;
    }
    public virtual void OnDestory()
    {
        UIHelper.Instance.AddOrGetComponent<CanvasGroup>(panelObj).interactable = false ;
    }

}
