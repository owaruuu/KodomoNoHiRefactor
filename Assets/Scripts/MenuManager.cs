using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenuObject;

    public void OpenMenu(GameObject menuObject)
    {
        menuObject.SetActive(true);
    }

    public void CloseMenu(GameObject menuObject)
    {
        menuObject.SetActive(false);
    }

    public void CloseMainMenu()
    {
        MainMenuObject.SetActive(false);
    }
}
