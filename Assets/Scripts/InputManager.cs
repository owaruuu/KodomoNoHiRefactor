using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void OnClickedPlayerDelegate();
    public static OnClickedPlayerDelegate OnClickedPlayerEvent;

    private bool touchedLastFrame;

    [SerializeField] private GameManager _gameManager;

    void Update()
    {
        if (touchedLastFrame && Input.touchCount > 0)
        {
            if(Input.touches[0].phase == UnityEngine.TouchPhase.Ended)
            {
                touchedLastFrame = false;
            }
        }

        if (!_gameManager.waitingForFirstClick)
        {
            return;
        }

        string tagTemp;

        if (Input.touchCount > 0)
        {
            if(Input.touches[0].phase == UnityEngine.TouchPhase.Began)
            {
                if (!touchedLastFrame)
                {
                    //si aun no hago el primer click
                    if (!_gameManager.madeFirstClick)
                    {
                        touchedLastFrame = true;
                        tagTemp = GetObjectTagWithRay2D(Input.touches[0].position);

                        if (tagTemp != null)
                        {
                            switch (tagTemp)
                            {
                                case "Player":
                                    _gameManager.AddCharge();
                                    OnClickedPlayerEvent?.Invoke();
                                    _gameManager.madeFirstClick = true;
                                    return;
                            }
                        }
                    }
                }               
            }
           
        }else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!_gameManager.madeFirstClick)
            {
                tagTemp = GetObjectTagWithRay2D(Mouse.current.position.ReadValue());

                if (tagTemp != null)
                {
                    switch (tagTemp)
                    {
                        case "Player":
                            _gameManager.AddCharge();
                            OnClickedPlayerEvent?.Invoke();
                            _gameManager.madeFirstClick = true;
                            return;
                    }
                }
            } 
        }     

        if (!_gameManager.playing)
        {
            return;
        }       

        if (Input.touchCount > 0)
        {     
            if(Input.touches[0].phase == UnityEngine.TouchPhase.Began)
            {
                if (!touchedLastFrame)
                {
                    touchedLastFrame = true;

                    var tag = GetObjectTagWithRay2D(Input.touches[0].position);
                    if (tag != null)
                    {
                        switch (tag)
                        {
                            case "Player":
                                _gameManager.AddCharge();
                                OnClickedPlayerEvent?.Invoke();
                                break;
                        }
                    }
                    else
                    {
                        _gameManager.DecreaseCharge();
                        _gameManager.SpawnPopUp();
                    }
                }                
            }
        }else if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            var tag = GetObjectTagWithRay2D(Mouse.current.position.ReadValue());
            if (tag != null)
            {
                switch (tag)
                {
                    case "Player":
                        _gameManager.AddCharge();
                        OnClickedPlayerEvent?.Invoke();
                        break;
                }
            }
            else
            {
                _gameManager.DecreaseCharge();
                _gameManager.SpawnPopUp();
            }
        }
    }

    private string GetObjectTagWithRay2D(Vector3 clickPosition)
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(clickPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if (hit.collider != null)
        {
            return hit.transform.gameObject.tag;
        }
        else
        {
            return null;
        }
    }
}
