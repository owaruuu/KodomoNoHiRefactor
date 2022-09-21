using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    //creo un path random y asi controlo donde se 'graba' my juego dentro de itch.io
    private string savePath = "idbfs/desafio_kodomo_nikkei_2022_609hungb2o93e4uygf39ieowr1";

    private GameDetails gameDetails = new GameDetails();
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        var tempGameDetails = new GameDetails();
        tempGameDetails = DataAccess.Load(savePath);

        if (tempGameDetails != null)
        {
            gameDetails = tempGameDetails;
            gameDetails.WonMedal = true;
            _uiManager.ShowHighScore();
        }
        else
        {
            _uiManager.HideHighScore();
        }
    }

    /// <summary>
    /// returs true if has new high score and saves it
    /// </summary>
    /// <returns></returns>
    public bool CheckNewHighScore()
    {
        if ((int)_gameManager.totalPoints > gameDetails.HighScore)
        {
            gameDetails.HighScore = (int)_gameManager.totalPoints;
            SaveGame();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetHighScore()
    {
        return gameDetails.HighScore;
    }

    /// <summary>
    /// returns Won Medal bool
    /// </summary>
    /// <returns></returns>
    public bool GetWonMedal()
    {
        return gameDetails.WonMedal;
    }

    /// <summary>
    /// returns true if already won Medal, otherwise it changes wonMedal to true and saves the game
    /// </summary>
    /// <returns></returns>
    public bool CheckIfWonMedal()
    {
        if (gameDetails.WonMedal)
            return true;

        gameDetails.WonMedal = true;
        SaveGame();
        return false;
    }

    private void SaveGame()
    {
        DataAccess.Save(gameDetails, savePath);
    }

    [ContextMenu("Reset Medal")]
    public void ResetMedal()
    {
        gameDetails.WonMedal = false;
        SaveGame();
    }
}