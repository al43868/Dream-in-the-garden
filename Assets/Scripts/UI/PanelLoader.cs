using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLoader : SingleTion<PanelLoader>
{
    public List<GameObject> panels = new();

    internal GameObject FindPanel(UIType type, Transform transform)
    {
        foreach (var item in panels)
        {
            if (item.name == type.Name)
            {
                var go= GameObject.Instantiate(item.transform,transform);
                return go.gameObject;
            }
        }
        return null;
    }
}
