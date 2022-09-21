using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LostPointsPopUp : SpawnableObject, IResetable
{
    public Animator animator;
    public TextMeshProUGUI popUpText;

    public void Reset()
    {
    }

    public void StartAnimation()
    {
        animator.SetTrigger("DoAnimation");
    }

    public void SetText(string text)
    {
        popUpText.text = text;
        StartAnimation();
    }

    public void SetColor(Color color)
    {
        popUpText.color = color;
    }

    public void ReturnToPoolAAAAAAAAAAAAAAAAAAAAAAAAAA()
    {
        myPool.ReturnToPool(this.gameObject);
    }
}
