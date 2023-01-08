using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2Int pos;
    public UnitType type;
    public virtual void Init(Vector2Int pos)
    {
        this.pos = pos;
    }
    public virtual void Get()
    {

    }
    public virtual void UpdateUnit()
    {

    }
}
public enum UnitType
{
    player,
    stone,
    plant
}
