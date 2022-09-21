using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnStartGameDelegate();
    public static event OnStartGameDelegate OnStartGameEvent;

    public delegate void OnAddPointsDelegate(string notifText, string notifNumText);
    public static event OnAddPointsDelegate OnAddPointsEvent;

    [Header("Setup")]
    public MenuManager _menuManager;
    public UIManager _uiManager;
    public Player _player;
    public SaveManager saveManager;
    public AudioManager audioManager;
    public Waterfall _waterfall;

    [Header("Game State")]
    public bool playing;
    public bool updatePoints;
    public bool gotOutOfMedalMenu;
    public bool kidGameMode;
    public Animator MedalAnimator;
    public Animator MedalMainMenuAnimator;

    [Header("GameStart")]
    public GameObject startingPosition;
    public GameObject offscreenPosition;
    public GameObject cameraStartingPosition;

    public bool showCornerAnimation;
    public bool firstTimePlaying;
    public bool cameraGotToPosition;
    public AnimationCurve camAnimCurve;
    public float camAnimCurveTime;
    public float cameraMovementSpeed;
    public float dampSpeed;
    public bool waitingForFirstClick;
    public bool madeFirstClick;

    [Header("Player Movement")]
    public float randomMovementTime;   

    [Header("Charge")]
    public float charge;
    public float maxCharge;
    public float minCharge;
    public float startingCharge;
    public float amountToAdd;

    [Header("decrease")]
    public float amountToDecrease;
    public float amountToDecreaseOnMissClick;
    public float maxAmountToDecrease;
    public float minAmountToDecrease;
    public float decreaseStep;
    public float timeToStartDecreasing;
    public float decreaseTimer;

    [Header("Combo")]
    public int combo;
    public float chargeComboMultiplier;
    public int maxCombo;
    public float timeToKillCombo;
    public float comboTimer;

    [Header("Points")]
    public float totalPoints;
    public float pointsWaiting;
    public float currentVisualPoints;
    public float playerClickPoints;

    [Header("Text Points")]
    public string playerText;

    [Header("Dificultad")]
    public float gameTimeLimit;
    public float gameSessionTimer;
    public float numberOfDificulties;
    public float currentDificultyMultplier = 1f;
    public float timeBetweenMovementsMultiplier;
    public float currentTimeBetweenMovementMultiplier = 1f;
    [Space]
    public float playerMovementStep;
    public float currentPlayerMovementMultiplier = 1f;
    [Space]
    public float moveDistanceStep;
    public float currentMoveDistanceMultiplier = 0f;
    [Space]
    public float chargeDecreaseMultiplierStep;
    public float currentChargeDecreaseMultiplier;
    public float chargeDecreaseOnMissClickStep;
    public float currentChargeDecreaseOnMissClickMultiplier;

    [Header("Dificultad Kodomo")]
    public float gameTimeLimitKodomo;
    public float gameSessionTimerKodomo;
    public float numberOfDificultiesKodomo;
    public float currentDificultyMultplierKodomo = 1f;
    public float timeBetweenMovementsMultiplierKodomo;
    public float currentTimeBetweenMovementMultiplierKodomo = 1f;
    [Space]
    public float playerMovementStepKodomo;
    public float currentPlayerMovementMultiplierKodomo = 1f;
    [Space]
    public float moveDistanceStepKodomo;
    public float currentMoveDistanceMultiplierKodomo = 0f;
    [Space]
    public float chargeDecreaseMultiplierStepKodomo;
    public float currentChargeDecreaseMultiplierKodomo;
    public float chargeDecreaseOnMissClickStepKodomo;
    public float currentChargeDecreaseOnMissClickMultiplierKodomo;

    [Header("Dificulty Steps")]
    public float firstDificultyStep;
    public float secondDifStep;
    public float thirdDifStep;
    public float fourthDifStep;

    [Header("Level Bools")]
    public bool gotToSecondLevel;
    public bool gotToThirdLevel;
    public bool gotToFourthLevel;
    public bool gotToFifthLevel;

    [Header("Musica")]
    public AudioClip musicaStartGame;
    public AudioClip musicaGameplay;
    public AudioClip musicaLose;
    public AudioClip musicaMainMenu;
    public AudioClip musicaWin;

    [Header("Sonidos")]
    public AudioClip audioClipPerder;
    public AudioClip audioClipClickPlayer;
    public AudioClip audioClipMissclick;
    public AudioClip[] audioClipsMissClick;
    public AudioClip audioClipFlyToSky;
    public AudioClip audioClipWinReward;
    public AudioClip audioClipLevelNotif;

    [Header("Jingles")]
    public AudioClip audioClipWinJingle;
    public AudioClip audioClipLevel5;

    [Header("Lengua")]
    public bool spanish;
    public bool japanese;
    public string reberuString;

    [Header("Textos Spanish")]
    public string textoJugarSpanish;
    public string textoSobreKodomoSpanish;
    [Space]
    public string textoClickParaEmpezarSpanish;
    public string textoKeepClickingSpanish;
    public string textoDificultadSpanish;
    public string textoClickEnPlayerSpanish;
    [Space]
    public string textoPerdisteSpanish;
    public string textoBotonRetrySpanish;
    public string textoBotonMainMenuSpanish;
    [Space]
    public string textoGanasteSpanish;
    public string textoBotonPlayAgainSpanish;
    public string textoOldRecordSpanish;
    public string textoNewRecordSpanish;
    public string textoWinPointsNotifSpanish;
    public string textoChargePointsNotifSpanish;
    [Space]
    public string textoVolumenSpanish;
    public string textoIdiomaSpanish;
    public string textoIdiomaEspanolSpanish;
    public string textoIdiomaJaponesSpanish;
    public string textoPrimerNombreSpanish;
    public string textoPrimerCreditoSpanish;
    public string textoSegundoCreditoSpanish;
    public string textoBotonCerrarSpanish;

    [Header("Textos Japones")]
    public string textoJugarJap;
    public string textoSobreKodomoJap;
    [Space]
    public string textoClickParaEmpezarJap;
    public string textoKeepClickingJap;
    public string textoDificultadJap;
    public string textoClickEnPlayerJap;
    [Space]    
    public string textoPerdisteJap;
    [TextArea]
    public string textoBotonRetryJap;
    [TextArea]
    public string textoBotonMainMenuJap;
    [Space]
    public string textoGanasteJap;
    public string textoBotonPlayAgainJap;
    public string textoOldRecordJap;
    public string textoNewRecordJap;
    public string textoWinPointsNotifJap;
    public string textoChargePointsNotifJap;
    [Space]
    public string textoVolumenJap;
    public string textoIdiomaJap;
    public string textoIdiomaEspanolJap;
    public string textoIdiomaJaponesJap;
    public string textoPrimerNombreJap;
    public string textoPrimerCreditoJap;
    public string textoSegundoCreditoJap;
    public string textoBotonCerrarJap;



    void Awake()
    {
        showCornerAnimation = true;
        firstTimePlaying = true;
        //charge = startingCharge;
        decreaseTimer = timeToStartDecreasing;
        amountToDecrease = minAmountToDecrease;
        gameSessionTimer = gameTimeLimit;
        //CalculateGameHalfs();

        InputManager.OnClickedPlayerEvent += ClickedPlayer;
    }

    private void CalculateGameHalfs()
    {
        var halfStep = gameTimeLimit / (numberOfDificulties + 1);

        firstDificultyStep = gameTimeLimit - halfStep;
        secondDifStep = firstDificultyStep - halfStep;
        thirdDifStep = secondDifStep - halfStep;
        fourthDifStep = thirdDifStep - halfStep;
    }

    void Update()
    {        
        if (!playing)
        {
            return;
        }

        //Numero que sube cada menos tiempo queda en el juego
        float i = Mathf.InverseLerp(gameTimeLimit, 0, gameSessionTimer);
        _waterfall.SetWaterfallFrames(i);

        if (combo == maxCombo)
        {
            _player.SetAllInOneMaterialFloat("_OutlineDistortAmount", 1f);
            _player.SetAllInOneMaterialFloat("_OutlineAlpha", 1f);
        }
        else if(combo > maxCombo / 2)
        {
            _player.SetAllInOneMaterialFloat("_OutlineAlpha", 1f);            
        }
        else
        {
            _player.SetAllInOneMaterialFloat("_OutlineAlpha", 0f);
            _player.SetAllInOneMaterialFloat("_OutlineDistortAmount", 0f);
        }
        

        if(charge >= maxCharge)
        {
            _uiManager.swordAllInOneMaterial.SetFloat("_OutlineAlpha", 1f);
        }
        else
        {
            _uiManager.swordAllInOneMaterial.SetFloat("_OutlineAlpha", 0f);
        }

        if (gameSessionTimer > 0)
        {
            if (gameSessionTimer < 10f)
            {

            }

            gameSessionTimer -= Time.deltaTime;
            if (gameSessionTimer < 0)
                gameSessionTimer = 0;

            if(gameSessionTimer < fourthDifStep)
            {
                if (!gotToFifthLevel)
                {
                    gotToFifthLevel = true;
                    ShowLevelNotif(5, true);
                    audioManager.PlaySound(audioClipLevel5);
                }
                currentDificultyMultplier = 5f;
                CalculateDificultyMultipliers();
                _uiManager.UpdateDificultyText(5);
            }
            else if(gameSessionTimer < thirdDifStep)
            {
                if (!gotToFourthLevel)
                {
                    gotToFourthLevel = true;
                    ShowLevelNotif(4, false);
                }
                currentDificultyMultplier = 4f;
                CalculateDificultyMultipliers();
                _uiManager.UpdateDificultyText(4);
            }
            else if (gameSessionTimer < secondDifStep)
            {
                if (!gotToThirdLevel)
                {
                    gotToThirdLevel = true;
                    ShowLevelNotif(3, false);
                }
                currentDificultyMultplier = 3f;
                CalculateDificultyMultipliers();
                _uiManager.UpdateDificultyText(3);
            }
            else if (gameSessionTimer < firstDificultyStep)
            {
                if (!gotToSecondLevel)
                {
                    gotToSecondLevel = true;
                    ShowLevelNotif(2, false);
                }
                currentDificultyMultplier = 2f;
                CalculateDificultyMultipliers();
                _uiManager.UpdateDificultyText(2);
            }
        }
        else
        {
            StartCoroutine(WinCoroutine());
            _waterfall.ResetWaterfallFrames();
            playing = false;
        }

        if(charge <= 0)
        {
            _player.SetAllInOneMaterialFloat("_OutlineAlpha", 0f);
            _waterfall.ResetWaterfallFrames();
            playing = false;
            _player.CancelMovement();       
            _uiManager.ShowLoseMenu();
            audioManager.PlaySound(audioClipPerder);
            audioManager.ChangeMusic(musicaLose);
            
            return;
        }


        if (decreaseTimer <= 0)
        {
            amountToDecrease += decreaseStep * currentChargeDecreaseMultiplier * Time.deltaTime;
            amountToDecrease = Mathf.Clamp(amountToDecrease, 0, maxAmountToDecrease);
            charge -= amountToDecrease * Time.deltaTime;
            charge = Mathf.Clamp(charge, minCharge, maxCharge);
        }
        else
        {
            decreaseTimer -= Time.deltaTime;
        }

        if (comboTimer <= 0)
        {
            combo = 0;
            _uiManager.UpdateComboText(combo);
        }
        else
        {
            comboTimer -= Time.deltaTime;
        }
    }

    public void ShowLevelNotif(float number, bool skull)
    {
        if (spanish)
        {
            if (skull)
            {
                _uiManager.levelNotifText.text = "Level ";
                _uiManager.levelNotifSkull.SetActive(true);
            }
            else
            {
                _uiManager.levelNotifText.text = "Level " + number;
            }

            
        }
        else
        {
            if (skull)
            {
                _uiManager.levelNotifText.text = reberuString + " ";
                _uiManager.levelNotifSkull.SetActive(true);
            }
            else
            {
                _uiManager.levelNotifText.text = reberuString + " " + number;
            }            
        }

        _uiManager.levelNotifAnimator.SetTrigger("DoAnimation");
        audioManager.PlaySound(audioClipLevelNotif);
    }



    [ContextMenu("StartGame")]
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        audioManager.ChangeMusic(musicaStartGame);

        //resetear posicion jugador por si acaso 
        _player.transform.position = offscreenPosition.transform.position;

        //esconder menu
        _menuManager.CloseMainMenu();

        CalculateDificultyMultipliers();       

        yield return new WaitForSeconds(0.1f);

        // move la camara a la starting position
        cameraGotToPosition = false;
        StartCoroutine(MoveCameraToPosition(cameraStartingPosition.transform.position, camAnimCurve, 6.52f, 5));

        while (!cameraGotToPosition)
        {
            yield return null;
        }

        //empezar a mover al jugador al centro, cuando termine
        _player.MovePlayerToCenter();
        if (showCornerAnimation)
        {
            _uiManager.ShowGameTimer();
            showCornerAnimation = false;
        }
        
        _uiManager.ShowDificultyText();
        _uiManager.ShowPoints();
        _uiManager.comboObject.SetActive(true);
        _uiManager.ShowChargeBar();

        while (!PlayerGotToCenter())
        {
            yield return null; 
        }

        yield return new WaitForSeconds(0.1f);

        //mostrar texto
        _uiManager.clickToStartText.SetActive(true);

        waitingForFirstClick = true;
        
        //cuando hago click, esconder texto
        while (!madeFirstClick)
        {
            yield return null;
        }
        //Debug.Log("sali del while esperando first click");

        _uiManager.clickToStartText.SetActive(false);
        audioManager.ChangeMusic(musicaGameplay);

        if (firstTimePlaying)
        {
            StartCoroutine(Utils.LerpCanvasAlpha(_uiManager.keepClickingCanvas, 0, 1, 0.2f, true));
            _uiManager.StartKeepPlayingAnimation();
        }       

        //empezar loop juego
        playing = true;
        charge = startingCharge;

        updatePoints = true;
        _uiManager.MoveLetters();
        _uiManager.UpdateScore();
        _player.MovePlayerAround();
        _player.Swim();
    }

    public void AddCharge()
    {
        decreaseTimer = timeToStartDecreasing;
        amountToDecrease = minAmountToDecrease;
        comboTimer = timeToKillCombo;

        if (combo < maxCombo)
        {
            combo++;
            if(combo > 1)   
                _uiManager.UpdateComboText(combo);
        }

        if (charge >= maxCharge)
        {
            SpawnPositivePopUp("MAX", _uiManager.swordFlameColor);

            return;
        }

        float AmountToAdd = amountToAdd * (combo * chargeComboMultiplier);

        SpawnPositivePopUp("+" + AmountToAdd.ToString(), Color.green);
        charge += AmountToAdd;
        charge = Mathf.Clamp(charge, minCharge, maxCharge);
    }

    public void DecreaseCharge()
    {
        combo = 0;
        _player.MissClickShaderEffect(0.1f);
        _uiManager.UpdateComboText(combo);
        _uiManager.DoChargeBarShakeAnimation();
        audioManager.PlaySounds(audioClipsMissClick, new float[] {.7f,1.2f});
        //_uiManager.SpawnPopUp(amountToDecreaseOnMissClick);
        decreaseTimer = 0f;
        charge -= amountToDecreaseOnMissClick * currentChargeDecreaseOnMissClickMultiplier;
        charge = Mathf.Clamp(charge, minCharge, maxCharge);
    }

    public void SpawnPopUp()
    {   
        Vector2 pos = _uiManager.popUpSpawnPosition.transform.position;
        //_uiManager
        _uiManager.SpawnPopUp("-" + (amountToDecreaseOnMissClick * currentChargeDecreaseOnMissClickMultiplier), pos, Color.red);
    }

    public void SpawnPositivePopUp(string amount, Color color)
    {
        Vector2 pos = _uiManager.popUpSpawnPosition.transform.position;
        _uiManager.SpawnPopUp(amount, pos, color);
    }

    private void AddPoints(string text, string num, float pointsToAdd)
    {
        pointsWaiting += pointsToAdd * combo;
        totalPoints += pointsToAdd * combo;
        _uiManager.CalculateStepMultiplier(pointsWaiting);
        OnAddPointsEvent?.Invoke(text, num);
    }

    private void ClickedPlayer()
    {
        //AddCharge();
        _player.PlayHitEffectAnimation(0.1f);

        var text = spanish ? textoClickEnPlayerSpanish : textoClickEnPlayerJap;

        if(combo > 1)
        {
            if (kidGameMode)
            {
                string tempText = string.Format("{0} x{1}", text, combo.ToString());
                string tempNum = string.Format("+{0}", (playerClickPoints /2) * combo);

                AddPoints(tempText, tempNum, playerClickPoints / 2);
            }
            else
            {
                string tempText = string.Format("{0} x{1}", text, combo.ToString());
                string tempNum = string.Format("+{0}", playerClickPoints * combo);

                AddPoints(tempText, tempNum, playerClickPoints);
            }           
        }
        else
        {
            if (kidGameMode)
            {
                string tempText = string.Format("{0}", text);
                string tempNum = string.Format("+{0}", playerClickPoints / 2);

                AddPoints(tempText, tempNum, playerClickPoints / 2);
            }else
            {
                string tempText = string.Format("{0}", text);
                string tempNum = string.Format("+{0}", playerClickPoints);

                AddPoints(tempText, tempNum, playerClickPoints);
            }
        }

        audioManager.PlaySound(audioClipClickPlayer, 0.9f);
    }


    /// <summary>
    /// Mueve la camara a una posicion con Lerp y animation curve, recordar resetear a falso el bool antes de llamarla
    /// </summary>
    /// <returns></returns>
    IEnumerator MoveCameraToPosition(Vector3 destination, AnimationCurve animCurve, float startingSize, float endingSize)
    {
        //Vector3 target = cameraStartingPosition.transform.position;
        Vector3 target = destination;

        //save starting position
        Vector3 startPos = Camera.main.transform.position;

        AnimationCurve curve = animCurve;
        float animTime = 0f; 

        while (Camera.main.transform.position != target)
        {
            animTime += Time.deltaTime * cameraMovementSpeed;
            Camera.main.transform.position = Vector3.Lerp(startPos, target, curve.Evaluate(animTime));
            Camera.main.orthographicSize = Mathf.Lerp(startingSize, endingSize, curve.Evaluate(animTime));
            yield return null;
        }

        cameraGotToPosition = true;
    }

    private void CalculateDificultyMultipliers()
    {
        if (kidGameMode)
        {
            currentTimeBetweenMovementMultiplier = 1 + (timeBetweenMovementsMultiplierKodomo * (currentDificultyMultplier - 1));

            currentPlayerMovementMultiplier = 1 + (playerMovementStepKodomo * (currentDificultyMultplier - 1));

            currentMoveDistanceMultiplier = 0.2f + moveDistanceStepKodomo * (currentDificultyMultplier - 1);

            currentChargeDecreaseMultiplier = 1 + (chargeDecreaseMultiplierStepKodomo * (currentDificultyMultplier - 1));

            currentChargeDecreaseOnMissClickMultiplier = 1 + (chargeDecreaseOnMissClickStepKodomo * (currentDificultyMultplier - 1));
        }
        else
        {
            currentTimeBetweenMovementMultiplier = 1 + (timeBetweenMovementsMultiplier * (currentDificultyMultplier - 1));

            currentPlayerMovementMultiplier = 1 + (playerMovementStep * (currentDificultyMultplier - 1));

            currentMoveDistanceMultiplier = 0.2f + moveDistanceStep * (currentDificultyMultplier - 1);

            currentChargeDecreaseMultiplier = 1 + (chargeDecreaseMultiplierStep * (currentDificultyMultplier - 1));

            currentChargeDecreaseOnMissClickMultiplier = 1 + (chargeDecreaseOnMissClickStep * (currentDificultyMultplier - 1));
        }
    }

    public void RetryGame()
    {
        _uiManager.HideLoseMenu();
        ResetValues();
        StartGame();
    }

    public void PlayAgain()
    {
        firstTimePlaying = false;
        _uiManager.HideWinMenu();
        ResetValues();
        StartGame();
    }

    public void GoBackToMainMenu()
    {
        StartCoroutine(GoBackToMainMenuCoroutine());
    }

    IEnumerator GoBackToMainMenuCoroutine()
    {     
        //mandar pescado a abajo
        firstTimePlaying = true;
        showCornerAnimation = true;
        _player.transform.position = offscreenPosition.transform.position;

        //esconder ui
        _uiManager.HideGameplayUI();
        _uiManager.winImageAnimator.SetTrigger("Reset");

        //reset valores ?
        ResetValues();

        //move camara hacia abajo
        cameraGotToPosition = false;
        StartCoroutine(MoveCameraToPosition(new Vector3(0,0,-10), camAnimCurve, 5, 6.52f));

        audioManager.ChangeMusic(musicaMainMenu);

        while (!cameraGotToPosition)
        {
            yield return null;
        }

        //mostrar main menu ui
        _uiManager.ShowMainMenu();

        if(saveManager.GetHighScore() > 0)
        {
            _uiManager.ShowHighScore();
        }
    }

    private void ResetValues()
    {
        _waterfall.ResetWaterfallFrames();

        gotToFifthLevel = false;
        gotToFourthLevel = false;
        gotToThirdLevel = false;
        gotToSecondLevel = false;
        _uiManager.levelNotifSkull.SetActive(false);

        _uiManager.ResetVisualPoints();
        _uiManager.levelNotifSkull.SetActive(false);
        _uiManager.skullImageObject.SetActive(false);
        _uiManager.dificultyNumberText.text = "1";
        _uiManager.comboTextCanvas.alpha = 0f;
        waitingForFirstClick = false;
        cameraGotToPosition = false;
        madeFirstClick = false;
        currentVisualPoints = 0f;
        combo = 0;
        currentDificultyMultplier = 1;
        totalPoints = 0f;
        pointsWaiting = 0f;
        charge = startingCharge;
        decreaseTimer = timeToStartDecreasing;
        amountToDecrease = minAmountToDecrease;
        _uiManager.cornerImage.color = _uiManager.cornerColor;
        gameSessionTimer = gameTimeLimit;
    }

    IEnumerator WinCoroutine()
    {
        bool showMedal = false;

        //checkeo si he ganado la medalla
        if(!saveManager.CheckIfWonMedal())
        {
            showMedal = true;
        }
        
        _player.SetAllInOneMaterialFloat("_OutlineAlpha", 0f);
        audioManager.ChangeMusic(musicaWin, 0.5f);

        //animacion player
        _player.TriggerAnimation("Win");  

        yield return new WaitForSeconds(1f);

        //mover jugador al centro
        _player.MovePlayerToCenter();

        while (!PlayerGotToCenter())
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        //disparar jugador arriba
        _player.MovePlayerToSky();
        audioManager.PlaySound(audioClipFlyToSky);

        yield return new WaitForSeconds(0.3f);

        //mostrar menu solo con "ganaste!"
        updatePoints = false;
        _uiManager.ShowWinMenu();
        audioManager.PlaySound(audioClipWinJingle);
        yield return new WaitForSeconds(0.1f);

        _uiManager.winImageAnimator.SetTrigger("DoAnimation");

        yield return new WaitForSeconds(0.4f);

        //anadir puntos por ganar
        _uiManager.doneAddingPoints = false;
        string notif = spanish ? textoWinPointsNotifSpanish : textoWinPointsNotifJap;

        _uiManager.AddWinPoints(notif, 500);

        while (!_uiManager.doneAddingPoints)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        //anadir puntos de charge
        _uiManager.doneAddingPoints = false;
        notif = spanish ? textoChargePointsNotifSpanish: textoChargePointsNotifJap;
        float points = charge * 5f;
        points = Mathf.Ceil(points);
        _uiManager.AddChargeWinPoints(notif, points);
        _uiManager.swordAllInOneMaterial.SetFloat("_OutlineAlpha", 0f);

        while (!_uiManager.doneAddingPoints)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        //check si hay nuevo record
        if (saveManager.CheckNewHighScore())
        {
            audioManager.PlaySound(audioClipWinReward);
           _uiManager.ShowNewRecordNotif();
           _uiManager.UpdateHighScoreText();
        }
        else
        {
           _uiManager.ShowOldRecordText();
        }

        gotOutOfMedalMenu = false;
        if (showMedal)
        {
            _uiManager.ShowMedalMenu();
            yield return new WaitForSeconds(0.1f);

            MedalAnimator.SetTrigger("DoAnimation");

            //aqui mostrar menu de medalla
        }
        else
        {
            gotOutOfMedalMenu = true;
        }

        while(!gotOutOfMedalMenu)
        {
            yield return null;
        }

        //mostrar los botones de jugar denuevo, menu principal
        _uiManager.ShowWinButtons();

        yield return null;
    }

    private bool PlayerGotToCenter()
    {
        if(_player.transform.position == startingPosition.transform.position)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GetOutOfMedal()
    {
        gotOutOfMedalMenu = true;
    }
}
