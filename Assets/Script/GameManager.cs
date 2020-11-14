using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool faceCard, backCard;
    public static GameManager firstCard, secondCard, thirdCard;
    private string firstCardName, secondCardName,thirdCardName;
    public static Queue<GameManager> series;
    public static int result,live;
    public static bool coroutine;
    public Text liveText,resultH;
    public GameObject Panel;
    private int i=0;

    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
        Invoke("StartRotateBack", 1.0f);
        PlayerPrefs.SetInt("live",5);
        PlayerPrefs.SetInt("i", 0);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        liveText.text = PlayerPrefs.GetInt("live").ToString();
        faceCard = false;
        coroutine = true;
        backCard = false;
        Panel.SetActive(false);
        series = new  Queue<GameManager>();
        result = 0;
      
    }
    public void StartRotateBack()
    {
        for (float i = 0f; i <= 180f; i += 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            Invoke("StartRotateFace", 3.0f);
          
        }
    }
    public void StartRotateFace()
    {
        for (float i = 180f; i >= 0f; i -= 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    private void OnMouseDown()
    {
        if (!backCard && coroutine)
        {
            StartCoroutine(RotateCard());
        }
    }
    public IEnumerator RotateCard()
    {
        coroutine = false;
        if (!faceCard)
        {
            series.Enqueue(this);
            for (float i = 0f; i <= 180f; i += 10)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                yield return new WaitForSeconds(0f);
            }
        }
        else if (faceCard)
        {
            for (float i = 180f; i >= 0f; i -= 10)
            {
                transform.rotation= Quaternion.Euler(0f, i, 0f);
                yield return new WaitForSeconds(0f);
                series.Clear();
            }

        }
        coroutine = true;
        faceCard = !faceCard;
        if (series.Count == 3)
        {
            checkResult();
            Statistics.scoreTabl();
        }
    }
    private void checkResult()
    {
        firstCard = series.Dequeue();
        firstCardName = firstCard.name.Substring(0, firstCard.name.Length - 1);
        secondCard = series.Dequeue();
        secondCardName = secondCard.name.Substring(0, secondCard.name.Length - 1);
        thirdCard = series.Dequeue();
        thirdCardName = secondCard.name.Substring(0, secondCard.name.Length - 1);
       
        if (firstCard.ToString() == secondCard.ToString() && thirdCard.ToString() == secondCard.ToString() && thirdCard.ToString() == firstCard.ToString())
        {
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
            Destroy(thirdCard.gameObject);

            
            i = PlayerPrefs.GetInt("i")+1;
            PlayerPrefs.SetInt("i", i);
            series.Clear();
            firstCard.backCard = true;
            secondCard.backCard = true;
            thirdCard.backCard = true;
            result += 1;
        }
        else
        {
            live= PlayerPrefs.GetInt("live")-1;
            PlayerPrefs.SetInt("live", live);
            liveText.text = PlayerPrefs.GetInt("live").ToString();
            firstCard.StartCoroutine("RotateBack");
            secondCard.StartCoroutine("RotateBack");
            thirdCard.StartCoroutine("RotateBack");
        }
    }
    private void Update()
    {
       
        if (PlayerPrefs.GetInt("live") == 0)
        {
            Panel.SetActive(true);
            resultH.text = "Game Over";
            Statistics.bestScoreTabl();
            Statistics.recordScoreTabl();
            Statistics.scoreText = GameObject.Find("/Canvas/Panel/Text").GetComponent<Text>();
            Statistics. bestText = GameObject.Find("/Canvas/Panel/Best").GetComponent<Text>();
            Statistics.scoreText.text = "Your result:" + PlayerPrefs.GetInt("score").ToString();
            Statistics.bestText.text = "Best result:" + PlayerPrefs.GetInt("bestScore").ToString();
        }
        else if(PlayerPrefs.GetInt("i") == 5)
        {
            Panel.SetActive(true);
            resultH.text = "Your Win";
            Statistics.bestScoreTabl();
            Statistics.recordScoreTabl();

            Statistics.scoreText = GameObject.Find("/Canvas/Panel/Text").GetComponent<Text>();
            Statistics.bestText = GameObject.Find("/Canvas/Panel/Best").GetComponent<Text>();
            Statistics.scoreText.text = "Your result:" + PlayerPrefs.GetInt("score").ToString();
            Statistics. bestText.text = "Best result:" + PlayerPrefs.GetInt("bestScore").ToString();
        }
    }
    public IEnumerator RotateBack()
    {
        coroutine = false;
        yield return new WaitForSeconds(0.2f);

        for (float i = 180f; i >= 0f; i -= 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            yield return new WaitForSeconds(0f);
            series.Clear();

        }
        backCard = false;
        faceCard = false;
        coroutine = true;
        
    }
   
}
