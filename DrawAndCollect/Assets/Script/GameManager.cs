using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BallShooter ballShooter;
    [SerializeField] private DrawLine drawLine;

    [Header("UI OBJECTS")] 
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI[] ScoreTexts;

    int scoredBallCount;
    void Start()
    {
        scoredBallCount = 0;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            ScoreTexts[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            ScoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore",0);
            ScoreTexts[0].text = "0";
            ScoreTexts[1].text = "0";
        }
    }
    public void Continue(Vector2 pos)
    {
        //enteredBucket.transform.position = pos;
        //enteredBucket.gameObject.setActive(true);
        //enteredBucket.Play();
        scoredBallCount++;
        ballShooter.Continue();
        drawLine.Continue();
    }

    public void GameOver()
    {
        Debug.Log("kaybettin");
        Panels[1].SetActive(true);
        Panels[2].SetActive(false);
        ScoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        ScoreTexts[2].text = scoredBallCount.ToString();

        if (scoredBallCount > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore",scoredBallCount);
            //BestScoreParticle.gameObject.SetActive(true);
            //BestScoreParticle.Play();
        }
        
        drawLine.StopDraw();
    }

    public void GameStart()
    {
        Panels[0].SetActive(false);
        ballShooter.StartGame();
        drawLine.StartDraw();
        Panels[2].SetActive(true);
    }
    
    public void TryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
