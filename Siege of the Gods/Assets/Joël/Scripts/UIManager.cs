using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int totalMoney;
    public Text money;
    public int totalTimeLeft;
    public Text timeLeft;
    public int waveCounter;
    public Text waveAmount;
    public Text fastForward;
    static public int gameSpeed = 1;
    int tempSpeed = 1;

    public void FastForward()
    {
        if (gameSpeed > 0 && gameSpeed < 3)
        {
            gameSpeed++;
        }
        else if(gameSpeed != 0)
        {
            gameSpeed = 1;
        }
    }
    public void Pause(Text pauseButtonText)
    {
        if(gameSpeed > 0)
        {
            pauseButtonText.text = "Res";
            tempSpeed = gameSpeed;
            gameSpeed = 0;
        }
        else
        {
            pauseButtonText.text = "Pause";
            gameSpeed = tempSpeed;
        }
    }
    private void Update()
    {
        if(gameSpeed >= 1)
        {
            fastForward.text = "Speed:\n" + gameSpeed.ToString();
        }
        else
        {
            fastForward.text = "Speed:\n" + tempSpeed.ToString();
        }
        money.text = "Drachma: " + totalMoney.ToString();
        timeLeft.text = "Time Until Next Wave: " + totalTimeLeft.ToString();
    }
}
