using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAnimationHelper : MonoBehaviour
{
    public void DoSplashAnimation()
    {
        GetComponent<Animator>().SetTrigger("DoAnimation");
    }
}
