using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidModeButton : MonoBehaviour
{
    public GameManager _gameManager;
    public Toggle toggle;
    public Color offColor;
    public Color onColor;

    public void TurnOn()
    {
        GetComponent<Image>().color = onColor;
        _gameManager.kidGameMode = true;
    }
    public void TurnOff()
    {
        GetComponent<Image>().color = offColor;
        _gameManager.kidGameMode = false;
    }

    public void ChangeKidMode()
    {
        if (toggle.isOn)
        {
            GetComponent<Image>().color = onColor;
            _gameManager.kidGameMode = true;
        }
        else
        {
            GetComponent<Image>().color = offColor;
            _gameManager.kidGameMode = false;
        }
    }


}
