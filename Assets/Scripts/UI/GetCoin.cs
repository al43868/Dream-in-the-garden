using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    public TMP_Text text;
    public void Init(int i,Vector3 pos)
    {
        transform.position = pos;
        text.text = "+ " + i;
        transform.DOMove(new Vector3(transform.position.x + 0.5f, transform.position.y, 0), 0.5f).OnComplete(() => { GameManager.Instance.GetCoinBack(this); });
    }
}
