using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Script References")]
    public GameManager _gameManager;
    public SaveManager _saveManager;

    [Header("Setup")]
    public GameObject highScoreObject;
    public TextMeshProUGUI highScoreNumberText;
    public GameObject mainMenuObject;

    [Header("ChargeBar")]
    public GameObject ChargeBarObject;
    public GameObject swordFireObject;
    public Animator chargeBarAnimator;
    public Slider ChargeBarSlider;
    public Material swordAllInOneMaterial;
    public Image fillBar;

    [Header("Text")]
    public GameObject clickToStartText;
    public GameObject comboObject;
    public CanvasGroup comboTextCanvas;
    public TextMeshProUGUI comboText;
    public GameObject popUpSpawnPosition;
    public Animator keepClickingAnimator;
    public Animator cornerParentAnimator;
    public CanvasGroup keepClickingCanvas;
    public GameObject informationMenuObject;
    public float pointsStep;
    public bool updatePoints;

    [Header("Medal")]
    public Medal medalScript;
    public Medal medalMainMenuScript;

    [Header("Level Notif")]
    public Animator levelNotifAnimator;
    public TextMeshProUGUI levelNotifText;
    public GameObject levelNotifSkull;

    [Header("Mobile Support")]
    public GameObject mobileMessageObject;
    public GameObject desktopMessageObject;
    public GameObject touchMessageObject;
    public TextMeshProUGUI textoApplicationPlatform;

    [Header("Color")]
    public Gradient gradient;
    public Color cornerColor;
    public Color swordFlameColor;

    [Header("TextAnimation")]
    public GameObject pointsParent;
    public bool moveLetters;
    public Animator[] letters;
    public TextMeshProUGUI[] letterObjects;
    public float animLetterDelay;
    public float waitBetweenWaves;

    public float stepMultiplier;
    public float newStep;

    [Header("Notification")]
    public Animator notificationAnim;
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI notificationNumberText;

    [Header("Dificulty")]
    public GameObject dificultyTextObject;
    public GameObject skullImageObject;
    public GameObject xObject;
    public TextMeshProUGUI dificultyNumberText;

    [Header("Game Timer")]
    public GameObject gameTimerObject;
    public TextMeshProUGUI gameTimerText;
    public Image cornerImage;
    public Animator gameTimerAnimator;
    public bool isUnder10;
    // public TextMeshProUGUI whole;
    //public TextMeshProUGUI decimals;

    [Header("PopUp")]
    public ObjectPool popUpPool;

    [Header("LoseMenu")]
    public GameObject loseMenuObject;
    public GameObject loseImageObject;
    public GameObject loseSparkObject;
    public Animator loseImageAnimator;
    public CanvasGroup retryButtonCanvas;
    public CanvasGroup mainMenuButtonCanvas;
    public TextMeshProUGUI loseMenuText;
    public TextMeshProUGUI retryButtonText;
    public TextMeshProUGUI mainMenuButtonText;

    [Header("WinMenu")]
    public TextMeshProUGUI winImageText;
    public TextMeshProUGUI playAgainButtonText;
    public TextMeshProUGUI mainMenuWinButtonText;
    public Animator cornerAnimator;
    public Animator winImageAnimator;
    public GameObject winMenuObject;
    public GameObject winPointsNotifObject;
    public GameObject MedalMenuObject;
    public GameObject MedalMainMenuObject;
    public TextMeshProUGUI pointsWinText;
    public TextMeshProUGUI oldRecordText;
    public TextMeshProUGUI winPointsNotif;
    public GameObject oldRecordObject;
    public TextMeshProUGUI newRecordText;
    public Animator newRecordAnimator;
    public CanvasGroup playAgainButtonCanvas;
    public CanvasGroup mainMenuButtonWinCanvas;
    public float addPointsWinSpeed;
    public bool doneAddingPoints;
    public float winPointsWaiting;
    public TextMeshProUGUI congratsPointsTextSpanish;
    public TextMeshProUGUI congratsPointsTextJap;
    public TextMeshProUGUI congratsMainMenuPointsTextSpanish;
    public TextMeshProUGUI congratsMainMenuPointsTextJap;
    public GameObject medalButtonObject;

    [Header("Menu Opciones")]
    public GameObject banderaChileObject;
    public GameObject banderaJaponObject;
    public Sprite banderaChile;
    public Sprite banderaChileGrey;
    public Sprite banderaJapon;
    public Sprite banderaJaponGrey;
    public GameObject checkChile;
    public GameObject checkJapon;

    [Header("Cambio de Idioma")]
    public TextMeshProUGUI playText;
    public TextMeshProUGUI sobreKodomoText;
    public GameObject botonKodomoModeObjectSpanish;
    public GameObject botonKodomoModeObjectJap;
    public GameObject cartaKodomoSpanish;
    public GameObject cartaKodomoJap;
    [Space]
    public TextMeshProUGUI clickParaEmpezarText;
    public TextMeshProUGUI keepClickingText;
    public TextMeshProUGUI dificultadText;
    [Space]
    public TextMeshProUGUI volumenText;
    public TextMeshProUGUI idiomaText;
    public TextMeshProUGUI spanishLabelText;
    public TextMeshProUGUI japaneseLabelText;
    public TextMeshProUGUI firstCreditText;
    public TextMeshProUGUI secondCreditText;
    public TextMeshProUGUI botonCerrarText;
    public TextMeshProUGUI primerNombreText;
    [Space]
    public GameObject congratsSpanishObject;
    public GameObject congratsJapObject;
    public GameObject congratsMainMenuSpanishObject;
    public GameObject congratsMainMenuJapObject;

    void Awake()
    {
        swordAllInOneMaterial = swordFireObject.GetComponent<Image>().material;

        GameManager.OnStartGameEvent += ShowStartText;
        GameManager.OnAddPointsEvent += ShowPointNotification;

        comboText.text = "";
        comboTextCanvas.alpha = 0f;
        ChargeBarSlider.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (_saveManager.GetWonMedal())
        {
            medalButtonObject.SetActive(true);
        }
        else
        {
            medalButtonObject.SetActive(false);
        }
    }

    void Update()
    {
        fillBar.color = gradient.Evaluate(_gameManager.charge / _gameManager.maxCharge);
        //ChargeBarSlider.value = _gameManager.charge;

        UpdateGameTimer();
    }

    public void MoveLetters()
    {
        StartCoroutine(MoveLettersCoroutine());
    }

    IEnumerator MoveLettersCoroutine()
    {
        while (_gameManager.updatePoints)
        {
            if (moveLetters)
            {
                foreach (var letter in letters)
                {
                    letter.SetTrigger("DoAnimation");
                    yield return new WaitForSeconds(animLetterDelay);
                }

                yield return new WaitForSeconds(waitBetweenWaves);
            }

            yield return null;
        }
    }

    public void UpdateScore()
    {
        StartCoroutine(UpdateScoreCoroutine());
    }

    IEnumerator UpdateScoreCoroutine()
    {
        while (_gameManager.updatePoints)
        {
            if (_gameManager.pointsWaiting > 0)
            {
                if(_gameManager.totalPoints + _gameManager.pointsWaiting > 99999)
                {
                    yield break;
                }

                moveLetters = true;

                newStep = pointsStep * stepMultiplier;
                if(newStep < pointsStep)
                {
                    newStep = pointsStep;
                }

                float pointsToAdd = Mathf.Ceil(newStep * Time.deltaTime);
                if (pointsToAdd > _gameManager.pointsWaiting)
                {
                    pointsToAdd = _gameManager.pointsWaiting;
                }
                float tempText = _gameManager.currentVisualPoints += pointsToAdd;
                _gameManager.pointsWaiting -= pointsToAdd;

                ParseNumberToAnimatedText(tempText);
            }
            else
            {
                stepMultiplier = 1;
                moveLetters = false;
            }

            yield return new WaitForEndOfFrame();
        }       
    }
    public void CalculateStepMultiplier(float currentPoints)
    {
        stepMultiplier = _gameManager.pointsWaiting / _gameManager.playerClickPoints;
    }

    private void ParseNumberToAnimatedText(float number)
    {
        string numberAsString = number.ToString();

        int letterIndex = 0;

        int length = numberAsString.Length;

        for (int numberIndex = numberAsString.Length - 1; numberIndex >= 0; numberIndex--)
        {
            letterObjects[letterIndex].text = numberAsString[numberIndex].ToString();
            letterIndex++;
        }
    }

    public void ResetVisualPoints()
    {
        int letterIndex = 0;

        for (int numberIndex = letterObjects.Length - 1; numberIndex >= 0; numberIndex--)
        {
            letterObjects[letterIndex].text = "0";
            letterIndex++;
        }
    }

    private void HideStartText()
    {
        clickToStartText.SetActive(false);
    }

    private void ShowStartText()
    {
        clickToStartText.SetActive(true);
    }

    private void ShowPointNotification(string text, string number)
    {
        notificationAnim.SetTrigger("DoAnimation");

        notificationText.text = text;
        notificationNumberText.text = number;
    }

    public void ShowPoints()
    {
        pointsParent.SetActive(true);
    }

    public void ShowChargeBar()
    {
        ChargeBarObject.SetActive(true);
    }

    public void UpdateComboText(float comboCount)
    {
        comboTextCanvas.alpha = 1f;
        string comboString = comboCount.ToString();

        if(comboCount < 1)
        {
            comboTextCanvas.alpha = 0f;
            comboText.text = "";
        }
        else if(comboCount >= 15)
        {
            comboText.color = Color.yellow;
            comboText.text = string.Format("x{0}", comboString);
        }else if(comboCount < 15)
        {
            comboText.color = Color.white;
            comboText.text = string.Format("x{0}", comboString);
        }
    }

    public void ShowDificultyText()
    {
        dificultyTextObject.SetActive(true);
    }

    public void UpdateDificultyText(int number)
    {
        if(number == 5)
        {
            skullImageObject.SetActive(true);
        }

        dificultyNumberText.text =string.Format("{0}", number.ToString());
    }

    public void ShowGameTimer()
    {
        cornerParentAnimator.SetTrigger("DoAnimation");
    }

    public void UpdateGameTimer()
    {
        if(_gameManager.gameSessionTimer > 60f)
        {
            gameTimerText.text = _gameManager.gameSessionTimer.ToString("000.0");
        }
        else if(_gameManager.gameSessionTimer < 10f)
        {
            gameTimerText.text = _gameManager.gameSessionTimer.ToString("0.0");
            cornerImage.color = Color.red;

            if (!isUnder10)
            {
                gameTimerAnimator.SetTrigger("DoAnimation");
                isUnder10 = true;
            }
        }
        else
        {
            gameTimerText.text = _gameManager.gameSessionTimer.ToString("00.0");
        }

        if(_gameManager.gameSessionTimer <= 0)
        {
            cornerImage.color = Color.green;
            gameTimerAnimator.SetTrigger("StopAnimation");
        }
    }

    public void SpawnPopUp(string text, Vector2 pos, Color color)
    {
        var popUp = popUpPool.GetObject();
        popUp.transform.SetParent(popUpPool.transform, true);
        popUp.transform.position = pos;
        string textToSet = string.Format("{0}", text);

        LostPointsPopUp popupScript = popUp.GetComponent<LostPointsPopUp>();
        popupScript.SetText(textToSet);
        popupScript.SetColor(color);
    }

    public void ChangeToJapanese()
    {
        _gameManager.spanish = false;
        _gameManager.japanese = true;
    }

    public void ChangeToSpanish()
    {
        _gameManager.spanish = true;
        _gameManager.japanese = false;
    }

    public void ShowLoseMenu()
    {
        if (_gameManager.spanish)
        {
            loseMenuText.text = _gameManager.textoPerdisteSpanish;
            retryButtonText.text = _gameManager.textoBotonRetrySpanish;
            mainMenuButtonText.text = _gameManager.textoBotonMainMenuSpanish;
        }
        else
        {
            loseMenuText.text = _gameManager.textoPerdisteJap;
            retryButtonText.text = _gameManager.textoBotonRetryJap;
            mainMenuButtonText.text = _gameManager.textoBotonMainMenuJap;
        }

        StartCoroutine(ShowLoseMenuCoroutine());        
    }

    IEnumerator ShowLoseMenuCoroutine()
    {
        DeactivateCanvasGroup(retryButtonCanvas);
        DeactivateCanvasGroup(mainMenuButtonCanvas);
        
        loseMenuObject.SetActive(true);
        loseSparkObject.SetActive(true);
        loseImageAnimator.SetTrigger("DoAnimation");

        yield return new WaitForSeconds(0.6f);

        StartCoroutine(Utils.LerpCanvasAlpha(retryButtonCanvas, 0, 1, 0.6f, true));
        StartCoroutine(Utils.LerpCanvasAlpha(mainMenuButtonCanvas, 0, 1, 0.6f, true));
    }

    public void ShowWinButtons()
    {
        StartCoroutine(Utils.LerpCanvasAlpha(playAgainButtonCanvas, 0, 1, 0.4f, true));
        StartCoroutine(Utils.LerpCanvasAlpha(mainMenuButtonWinCanvas, 0, 1, 0.4f, true));
    }  

    public void HideLoseMenu()
    {
        loseSparkObject.SetActive(false);
        loseMenuObject.SetActive(false);
    }

    public void HideWinMenu()
    {
        winImageAnimator.SetTrigger("Reset");
        winMenuObject.SetActive(false);
    }

    public void HideGameplayUI()
    {
        ChargeBarObject.SetActive(false);
        pointsParent.SetActive(false);
        dificultyTextObject.SetActive(false);
        comboObject.SetActive(false);
        cornerParentAnimator.SetTrigger("Reset");
    }

    public void ShowMainMenu()
    {
        if (_saveManager.GetWonMedal())
        {
            medalButtonObject.SetActive(true);
        }
        
        mainMenuObject.SetActive(true);
    }

    private void DeactivateCanvasGroup(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowWinMenu()
    {
        //escondo cosas
        DeactivateCanvasGroup(playAgainButtonCanvas);
        DeactivateCanvasGroup(mainMenuButtonWinCanvas);
        oldRecordObject.SetActive(false);
        newRecordAnimator.SetTrigger("Reset");

        //seteo puntons
        pointsWinText.text = _gameManager.totalPoints.ToString("00000");

        //apago los puntos de gameplay
        pointsParent.SetActive(false);

        //muestro el ui
        winMenuObject.SetActive(true);

        if (_gameManager.spanish)
        {
            winImageText.text = _gameManager.textoGanasteSpanish;
            playAgainButtonText.text = _gameManager.textoBotonPlayAgainSpanish;
            mainMenuWinButtonText.text = _gameManager.textoBotonMainMenuSpanish;
        }
        else
        {
            winImageText.text = _gameManager.textoGanasteJap;
            playAgainButtonText.text = _gameManager.textoBotonPlayAgainJap;
            mainMenuWinButtonText.text = _gameManager.textoBotonMainMenuJap;
        }
    }

    public void ShowNewRecordNotif()
    {
        newRecordText.text = _gameManager.spanish ? _gameManager.textoNewRecordSpanish : _gameManager.textoNewRecordJap;
        newRecordAnimator.SetTrigger("DoAnimation");
    }

    public void ShowOldRecordText()
    {
        oldRecordObject.SetActive(true);
        string tText = _gameManager.spanish ? _gameManager.textoOldRecordSpanish :
            _gameManager.textoOldRecordJap;

        oldRecordText.text = tText + _saveManager.GetHighScore();
    }


    public void CornerAnimation()
    {
        cornerAnimator.SetTrigger("DoAnimation");
    }

    public void AddWinPoints(string notifText, float pointsToAdd)
    {
        StartCoroutine(AddWinPointsCoroutine(notifText, pointsToAdd));
    }

    IEnumerator AddWinPointsCoroutine(string notifText, float pointsToAdd)
    {
        //mostrar el texto
        winPointsNotif.text = notifText + pointsToAdd;
        winPointsNotifObject.SetActive(true);

        //empezar a anadir puntos 
        winPointsWaiting += pointsToAdd;
        while(winPointsWaiting > 0)
        {
            //restarle a los waiting
            var pointsToSubtract = addPointsWinSpeed * Time.deltaTime;

            if (pointsToSubtract > winPointsWaiting)
            {
                pointsToSubtract = winPointsWaiting;
            }

            float pointsToPrint = _gameManager.totalPoints + pointsToSubtract;
            //armo el texto de puntos
            pointsWinText.text = pointsToPrint.ToString("00000");

            //resto puntos al waiting
            winPointsWaiting -= pointsToSubtract;
            _gameManager.totalPoints += pointsToSubtract;

            yield return null;
        }

        doneAddingPoints = true;
        winPointsNotifObject.SetActive(false);
    }

    public void AddChargeWinPoints(string notifText, float pointsToAdd)
    {
        StartCoroutine(AddChargeWinCoroutine(notifText, pointsToAdd));
    }

    IEnumerator AddChargeWinCoroutine(string notifText, float pointsToAdd)
    {
        //mostrar el texto
        winPointsNotif.text = notifText + pointsToAdd;
        winPointsNotifObject.SetActive(true);

        //empezar a anadir puntos 
        winPointsWaiting += pointsToAdd;
        while (winPointsWaiting > 0)
        {
            //restarle a los waiting
            var pointsToSubtract = addPointsWinSpeed * Time.deltaTime;

            if (pointsToSubtract > winPointsWaiting)
            {
                pointsToSubtract = winPointsWaiting;
            }

            float pointsToPrint = _gameManager.totalPoints + pointsToSubtract;
            //armo el texto de puntos
            pointsWinText.text = pointsToPrint.ToString("00000");

            //resto puntos al waiting
            _gameManager.charge -= pointsToSubtract / 5f;
            winPointsWaiting -= pointsToSubtract;
            _gameManager.totalPoints += pointsToSubtract;

            yield return null;
        }

        doneAddingPoints = true;
        winPointsNotifObject.SetActive(false);
    }

    public void DoChargeBarShakeAnimation()
    {
        chargeBarAnimator.SetTrigger("DoAnimation");
    }


    /// <summary>
    /// sets trigger y despues de un tiempo esconde el texto.
    /// </summary>
    public void StartKeepPlayingAnimation()
    {
        StartCoroutine(StartKeepPlayingAnimationCoroutine());       
    }

    IEnumerator StartKeepPlayingAnimationCoroutine()
    {
        keepClickingAnimator.SetTrigger("DoAnimation");

        yield return new WaitForSeconds(2f);

        StartCoroutine(Utils.LerpCanvasAlpha(keepClickingCanvas, 1, 0, 0.4f, false));
    }

    public void SetLanguage(string leng)
    {
        switch (leng)
        {
            case "Chileno":
                //poner foto color
                banderaChileObject.GetComponent<Image>().sprite = banderaChile;
                //poner foto grey japones
                banderaJaponObject.GetComponent<Image>().sprite = banderaJaponGrey;
                //mostrar ticket bajo chile
                checkChile.SetActive(true);
                //esconder ticket bajo japones
                checkJapon.SetActive(false);
                //cambiar estado bool
                _gameManager.spanish = true;
                _gameManager.japanese = false;
                ChangeUIText(1);
                break;
            case "Japones":
                //poner foto color
                banderaChileObject.GetComponent<Image>().sprite = banderaChileGrey;
                //poner foto grey japones
                banderaJaponObject.GetComponent<Image>().sprite = banderaJapon;
                //mostrar ticket bajo chile
                checkChile.SetActive(false);
                //esconder ticket bajo japones
                checkJapon.SetActive(true);
                //cambiar estado bool
                _gameManager.spanish = false;
                _gameManager.japanese = true;
                ChangeUIText(2);
                break;
        }
    }

    public void ChangeUIText(int leng)
    {
        if(leng == 1)//poner todo en espanol
        {
            //Menu Opciones
            volumenText.text = _gameManager.textoVolumenSpanish;
            idiomaText.text = _gameManager.textoIdiomaSpanish;
            spanishLabelText.text = _gameManager.textoIdiomaEspanolSpanish;
            japaneseLabelText.text = _gameManager.textoIdiomaJaponesSpanish;
            firstCreditText.text = _gameManager.textoPrimerCreditoSpanish;
            secondCreditText.text = _gameManager.textoSegundoCreditoSpanish;
            botonCerrarText.text = _gameManager.textoBotonCerrarSpanish;
            primerNombreText.text = _gameManager.textoPrimerNombreSpanish;

            //Menu Principal
            playText.text = _gameManager.textoJugarSpanish;
            sobreKodomoText.text = _gameManager.textoSobreKodomoSpanish;
            botonKodomoModeObjectSpanish.SetActive(true);
            botonKodomoModeObjectJap.SetActive(false);

            //GamePlay
            clickParaEmpezarText.text = _gameManager.textoClickParaEmpezarSpanish;
            keepClickingText.text = _gameManager.textoKeepClickingSpanish;
            dificultadText.text = _gameManager.textoDificultadSpanish;
        }
        
        if(leng == 2)//poner todo en japones
        {
            //Menu Opciones
            volumenText.text = _gameManager.textoVolumenJap;
            idiomaText.text = _gameManager.textoIdiomaJap;
            spanishLabelText.text = _gameManager.textoIdiomaEspanolJap;
            japaneseLabelText.text = _gameManager.textoIdiomaJaponesJap;
            firstCreditText.text = _gameManager.textoPrimerCreditoJap;
            secondCreditText.text = _gameManager.textoSegundoCreditoJap;
            botonCerrarText.text = _gameManager.textoBotonCerrarJap;
            primerNombreText.text = _gameManager.textoPrimerNombreJap;

            //Menu Principal
            playText.text = _gameManager.textoJugarJap;
            sobreKodomoText.text = _gameManager.textoSobreKodomoJap;
            botonKodomoModeObjectSpanish.SetActive(false);
            botonKodomoModeObjectJap.SetActive(true);

            //GamePlay
            clickParaEmpezarText.text = _gameManager.textoClickParaEmpezarJap;
            keepClickingText.text = _gameManager.textoKeepClickingJap;
            dificultadText.text = _gameManager.textoDificultadJap;
        }
    }

    public void ShowLetter()
    {
        informationMenuObject.SetActive(true);

        if (_gameManager.spanish)
        {
            //Carta Explicacion
            cartaKodomoSpanish.SetActive(true);
            cartaKodomoJap.SetActive(false);
        }
        else
        {
            //Carta Explicacion
            cartaKodomoSpanish.SetActive(false);
            cartaKodomoJap.SetActive(true);
        }
    }

    public void ShowHighScore()
    {
        UpdateHighScoreText();
        highScoreObject.SetActive(true);
    }

    public void UpdateHighScoreText()
    {
        highScoreNumberText.text = _saveManager.GetHighScore().ToString("00000");
    }

    public void HideHighScore()
    {
        highScoreObject.SetActive(false);
    }

    public void ShowMedalMenu()
    {
        if (_gameManager.spanish)
        {
            congratsJapObject.SetActive(false);
            congratsSpanishObject.SetActive(true);

            congratsPointsTextSpanish.text = _saveManager.GetHighScore().ToString() + " Puntos.";
        }
        else
        {
            congratsSpanishObject.SetActive(false);
            congratsJapObject.SetActive(true);

            congratsPointsTextJap.text = _saveManager.GetHighScore().ToString();
        }
      
        MedalMenuObject.SetActive(true);
    }

    public void ShowMedalMenuInMainMenu()
    {
        if (_gameManager.spanish)
        {
            congratsMainMenuJapObject.SetActive(false);
            congratsMainMenuSpanishObject.SetActive(true);

            congratsMainMenuPointsTextSpanish.text = _saveManager.GetHighScore().ToString() + " Puntos.";
        }
        else
        {
            congratsMainMenuSpanishObject.SetActive(false);
            congratsMainMenuJapObject.SetActive(true);

            congratsMainMenuPointsTextJap.text = _saveManager.GetHighScore().ToString();
        }

        MedalMainMenuObject.SetActive(true);
        _gameManager.MedalMainMenuAnimator.SetTrigger("SetIdle");
        medalMainMenuScript.StartShine();

    }

    public void HideMedalMenu(GameObject objecto)
    {
        objecto.SetActive(false);
        _gameManager.GetOutOfMedal();
    }
}
