using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : SingleTion<GameManager>
{
    public MapManager mapManager;

    public GamePlay gamePlay;
    public GameStateFSM gameStateFSM;
    public Camera camara;
    private float camaraSize;
    public int age;
    public int turn;
    private GetCoinPool getCoinPool;
    public int level;
    public Sprite a, b, c, d;
    public SpriteRenderer background;
    public int explosiveCount;
    public Boom boomEff;
    public AudioClip boom, coin, bk1, bk2, win, button;
    public bool isDClip;
    public float CamaraSize
    {
        get { return camaraSize; }
        set
        {
            camaraSize = Mathf.Clamp(value, 3.5f, 6f);
            camara.orthographicSize = camaraSize;
        }
    }
    public AudioSource all, clip;

    public void PlayEff(string name)
    {
        switch (name)
        { 
            case"boom":
                clip.PlayOneShot(boom);
                break;
            case "coin":
                clip.PlayOneShot(coin);
                break;
            case "win":
                clip.PlayOneShot(win);
                break;
            case "Button":
                clip.PlayOneShot(button);
                break;
            default:
                break;
        }
    }
    public void ChangeMusic()
    {
        if (isDClip)
        {
            all.clip=bk2;
            all.Play();
            isDClip =false;
        }
        else
        {
            all.clip=bk1;
            all.Play();
            isDClip = true;
        }
    }
    internal void GetCoinBack(GetCoin getCoin)
    {
        getCoinPool.Release(getCoin);
    }

    internal void ChangeMusic(float arg0)
    {
        all.volume = arg0;
    }

    internal void ChangeEff(float arg0)
    {
        clip.volume = arg0;
    }

    internal void GetStoneEff(Stone stone)
    {
        Instantiate(boomEff, stone.transform.position, Quaternion.identity);
    }


    internal void PleyEffect(string v, int i = 0)
    {
        if (v == "Coin")
        {
            var go=getCoinPool.Get();
            go.Init(i, mapManager.player.transform.position);
        }
    }



    internal void NextTurn()
    {
        TurnUP();
        mapManager.UpdateTurn();
        for (int i = 0; i <ShopManager.Instance. plantInsNumber; i++)
        {
            mapManager.CreatNewPlant();
        }
    }

    internal void Win()
    {
        gameStateFSM.SwitchToState(3);
        GameRoot.Instance.ShowWinPanel();
    }

    private void TurnUP()
    {
        turn++;
        if (turn >= 12)
        {
            age++;
            turn = 1;
            if (age >= 80) Lose();
            if((age%5)==0) mapManager.SetStone(1);
            if (((age / 10) - 3) > level)
            {
                LevelUp();
            }
        }
        GameRoot.Instance.ReflashMainPanel();
        switch (turn)
        {
            case 1: 
            case 2:
            case 3:
                background.sprite = a;
                    break;
            case 4:
            case 5:
            case 6:
                background.sprite = b;
                break;
            case 7:
            case 8:
            case 9:
                background.sprite = c;
                break;
            case 10:
            case 11:
            case 12:
                background.sprite = d;
                break;
            default:
                break;
        }
    }

    private void Lose()
    {
        GameRoot.Instance.ShowLosePanel();
    }

    internal void BackPlay()
    {
        gameStateFSM.SwitchToState(1);
        GameRoot.Instance.uiManager.Pop();
    }
    internal void PauseGame()
    {
        gameStateFSM.SwitchToState(2);
        GameRoot.Instance.ShowPausePanel();
    }
    internal void ShowShopMenu()
    {
        gameStateFSM.SwitchToState(2);
        GameRoot.Instance.ShowShopPanel();
    }
    private void LevelUp()
    {
        level++;
    }

    protected override void Awake()
    {
        base.Awake();
        mapManager = GetComponent<MapManager>();
        if (gamePlay == null)
            gamePlay = new GamePlay();
        gamePlay.Enable();
        gamePlay.Play.Move.performed += PlayerMoveListener;
        //gamePlay.Play.CamaraFar.performed += CamaraFar;
        //gamePlay.Play.CamaraNear.performed += CamaraNear;

        gameStateFSM = new GameStateFSM(1);
        age = 30;
        turn = 1;
        background=GameObject.Find("BackGroundSprite").GetComponent<SpriteRenderer>();
        getCoinPool=GetComponent<GetCoinPool>();
        getCoinPool.Init();
        Time.timeScale = 1;
    }

    private void PlayerMoveListener(InputAction.CallbackContext obj)
    {
        if (gameStateFSM.CurrentState == 1)
        {
            PlyerMove(obj.ReadValue<Vector2>());
        } 
    }

    private void PlyerMove(Vector2 vector2)
    {
        if (vector2.x != 0 && vector2.y != 0)
        {
            return;
        }
        else
        {
            if(vector2.x < 0)
            {
                mapManager.PlayerMove(new Vector2Int(-1, 0));
            }
            else if(vector2.x > 0)
            {
                mapManager.PlayerMove(new Vector2Int(1, 0));
            }
            else
            {
                if(vector2.y < 0)
                {
                    mapManager.PlayerMove(new Vector2Int(0,-1));

                }
                else
                {
                    mapManager.PlayerMove(new Vector2Int(0,1));
                }
            }
        }
    }

    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        mapManager.Init();
        SetCamara();
        GameRoot.Instance.ShowMainPanel();
        GameRoot.Instance.ReflashMainPanel();
    }

    private void SetCamara()
    {
        camara = GameObject.Find("MainCamera").GetComponent<Camera>();
        camara.transform.position = new Vector3(mapManager.x / 2f, mapManager.y / 2f, -10);
        CamaraSize = camara.orthographicSize;
    }
}
