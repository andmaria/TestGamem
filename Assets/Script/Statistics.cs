using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    public static Text scoreText, bestText, recordText;
    public static int score, bestScore, recordScore;
    
    static int saveLive;
    void Start()
    {

    }
    public static void scoreTabl()
    {
        
        if (GameManager.firstCard.ToString() == GameManager.secondCard.ToString()&& GameManager.thirdCard.ToString()!= GameManager.secondCard.ToString() && GameManager.thirdCard.ToString() != GameManager.firstCard.ToString())
        {
            saveLive = PlayerPrefs.GetInt("live");
            score = (1 * saveLive)+PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("score", score);
        }
      else  if (GameManager.firstCard.ToString() == GameManager.secondCard.ToString() && GameManager.thirdCard.ToString() == GameManager.secondCard.ToString() && GameManager.thirdCard.ToString() == GameManager.firstCard.ToString())
        {
            saveLive = PlayerPrefs.GetInt("live");
            score =( 3 * saveLive) + PlayerPrefs.GetInt("score"); 
            PlayerPrefs.SetInt("score", score);
        }
    }
    public static void bestScoreTabl()
    {
        PlayerPrefs.GetInt("bestScore");

        if (PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("bestScore"))
        {
            bestScore = PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
        
    }
    public static void recordScoreTabl()
    {
        recordText = GameObject.Find("/Canvas/Panel/RecordText").GetComponent<Text>();
        PlayerPrefs.GetInt("recordScore");
        if (PlayerPrefs.GetInt("score") >= PlayerPrefs.GetInt("recordScore"))
        {
            recordScore = PlayerPrefs.GetInt("bestScore");
            PlayerPrefs.SetInt("recordScore", recordScore);

            recordText.text = "RECORD:" + PlayerPrefs.GetInt("recordScore").ToString();

        }
        else
        {
            recordText.text = " ";
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
        PlayerPrefs.SetInt("recordScore", recordScore);
    }

}
