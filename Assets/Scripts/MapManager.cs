using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : SingleTion<MapManager>
{
    public TileBase backGroundTile;
    public int x, y;
    public Tilemap backgroundMap;
    public Dictionary<Vector2Int, Grid> grids = new();
    public Player playerPrefab;
    public Player player;
    public Stone stone;
    public List<Unit> units = new List<Unit>();
    public List<Plant> plantPrefabs = new List<Plant>();
    public void Init()
    {
        InitMap();
        SetPlayer();
        SetStone(3);
    }

    public void SetStone(int count)
    {
        for (int j = 0;j < count; j++)
        {
            List<Vector2Int> chosegrids = new();
            foreach (var item in grids)
            {
                if (item.Value.OnTheGrid == null)
                {
                    chosegrids.Add(item.Key);
                }
            }
            int i = UnityEngine.Random.Range(0, chosegrids.Count);
            Vector3 pos = new Vector3(chosegrids[i].x + 0.5f, chosegrids[i].y + 0.5f, 0);
            var go = GameObject.Instantiate(stone, pos, Quaternion.identity);
            grids[chosegrids[i]].OnTheGrid = go;
            go.Init(chosegrids[i]);
        }
        
        //for (int i = 0; i < count; i++)
        //{
        //    if (units.Count >= ShopManager.Instance.plantMaxNumber) return;
        //    int x= 0;
        //    int y = 0;
        //    do
        //    {
        //        x= UnityEngine.Random.Range(0, 4);
        //        y = UnityEngine.Random.Range(0, 4);
        //    } while (!SetStone(new Vector2Int(x, y)));
        //}
        //bool SetStone(Vector2Int pos)
        //{
        //    if (!grids.ContainsKey(pos)) return false;
        //    if (grids[pos] == null) return false;
        //    if (grids[pos].OnTheGrid != null)return false;
        //    var go = GameObject.Instantiate(stone, new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0), Quaternion.identity);
        //    grids[pos].OnTheGrid = go;
        //    go.Init(pos);
        //    return true;
        //}
    }

    internal void UpdateTurn()
    {
        for (int i = units.Count-1; i >= 0; i--)
        {
            units[i].UpdateUnit();
        }
    }

    internal bool SetPlant(Vector2Int pos, Unit nextLevel,bool canPlant=false)
    {
        if (!grids.ContainsKey(pos)) return false;
        if (grids[pos] == null) return false;
        if (grids[pos].OnTheGrid != null)
        {
            if (canPlant) return false;
            if (grids[pos].OnTheGrid.type == UnitType.player && grids[pos].OnTheGrid.type == UnitType.stone) return false;
            grids[pos].OnTheGrid = null;
        }
        var go = GameObject.Instantiate(nextLevel, new Vector3(pos.x+0.5f, pos.y+0.5f, 0), Quaternion.identity);
        grids[pos].OnTheGrid = go;
        go.Init(pos);
        units.Add(go);
        return true;
    }

    private void SetPlayer()
    {
        var go = GameObject.Instantiate(playerPrefab, new Vector3(0 + 0.5f, 0 + 0.75f, 0), Quaternion.identity);
        go.pos = new Vector2Int(0, 0);
        grids[Vector2Int.zero].OnTheGrid = go;
        player = go;
    }

    internal void CreatNewPlant()
    {
        if(units.Count>=ShopManager.Instance.plantMaxNumber) return;
        List<Vector2Int> chosegrids = new();
        foreach (var item in grids)
        {
            if (item.Value.OnTheGrid == null)
            {
                chosegrids.Add(item.Key);
            }
        }
        int i = UnityEngine.Random.Range(0, chosegrids.Count);
        Vector3 pos = new Vector3(chosegrids[i].x + 0.5f, chosegrids[i].y + 0.5f, 0);
        var go = GameObject.Instantiate(plantPrefabs[UnityEngine.Random.Range(0, plantPrefabs.Count)], pos, Quaternion.identity);
        grids[chosegrids[i]].OnTheGrid = go;
        go.Init(chosegrids[i]);
        units.Add(go);
        //if (units.Count >= ShopManager.Instance.plantMaxNumber) return;
        //int i = 0;
        //int j = 0;
        //int p = 0;
        //do
        //{
        //    i = UnityEngine.Random.Range(0, 4);
        //    j = UnityEngine.Random.Range(0, 4);
        //    p = UnityEngine.Random.Range(0, plantPrefabs.Count);
        //} while (!SetPlant(new Vector2Int(i, j), plantPrefabs[p],true));
    }

    private void InitMap()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                backgroundMap.SetTile(new Vector3Int(i, j, 0), backGroundTile);
                grids.Add(new Vector2Int(i, j), new Grid());
            }
        }
    }

    public void PlayerMove(Vector2Int vector2)
    {
        Vector2Int targetPos = player.pos + vector2;
        if (grids.ContainsKey(targetPos))
        {
            if (grids[targetPos].OnTheGrid == null)
            {
                grids[player.pos].OnTheGrid = null;
                grids[targetPos].OnTheGrid = player;
                player.Move(targetPos);
            }
            else
            {
                if (grids[targetPos].OnTheGrid.type == UnitType.plant)
                {
                    grids[player.pos].OnTheGrid = null;
                    grids[targetPos].OnTheGrid.Get();
                    grids[targetPos].OnTheGrid = player;
                    player.Move(targetPos);
                }else if(grids[targetPos].OnTheGrid.type == UnitType.stone)
                {
                    if (GameManager.Instance.explosiveCount > 0)
                    {
                        grids[player.pos].OnTheGrid = null;
                        grids[targetPos].OnTheGrid.Get();
                        grids[targetPos].OnTheGrid = player;
                        player.Move(targetPos);
                        GameManager.Instance.explosiveCount--;
                    }

                }
            }
        }
    }
}
public class Grid
{
    public Unit OnTheGrid;
}
