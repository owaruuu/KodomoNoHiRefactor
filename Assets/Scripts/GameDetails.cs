using System;

[Serializable]
public class GameDetails 
{
    public int HighScore;
    public bool WonMedal;

    public GameDetails()
    {
        HighScore = 0;
        WonMedal = false;
    }
}
