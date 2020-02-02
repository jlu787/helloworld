using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public bool playingGame = false;
    public GameObject monitor;
    public Animator monitorAnim;
    public int currentScore = 0;
    public int highScore;
    public GameObject monitorLight;
    public GameObject timerObj;
    public GameObject scoreObj;
    public GameObject highscoreObj;
    public AudioSource bgm;
    public TextMesh scoreTM;
    public TextMesh highscoreTM;
    public GameObject playIcon;

    public GameObject[] redLights;
    public GameObject[] greenLights;

    public float maxPitch = 2.0f;
    public float minPitch = 1.0f;
    public float timeToStartIncreasingPitch = 5.0f;
    public float maxPeriod = 150.0f;
    public float minperiod = 10.0f;
    public float periodScale;



    private bool readyToStart = false;
    private string highScoreStr;

    public AudioSource monitorSound;
    public AudioSource monitorReversed;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);
        highScore = PlayerPrefs.GetInt("highscore");
        //highscoreTM.text = highScore.ToString();
        SetHighScore();
        timerObj.SetActive(false);
        scoreObj.SetActive(false);
        StartCoroutine(WaitForMonitorDisplayLight());
        bgm.Play();
        monitorSound.Play();
        periodScale = minperiod;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        if (playingGame == false && Input.GetMouseButtonDown(0) && readyToStart)
        {
            playingGame = true;
            //monitor.SetActive(false);
            monitorAnim.SetBool("Activate", false);
            StartCoroutine(WaitForMonitorToLeave());
            playIcon.SetActive(false);
            highscoreObj.SetActive(false);
            monitorLight.SetActive(false);
            //monitorSound.Play();
            monitorReversed.Play();
        }
        scoreTM.text = currentScore.ToString();
        PitchController();
    }

    IEnumerator WaitForMonitorDisplayLight()
    {
        yield return new WaitForSeconds(1.0f);
        monitorLight.SetActive(true);
        StartCoroutine(WaitForMonitorToStopBouncing());
    }

    IEnumerator WaitForMonitorToLeave()
    {
        yield return new WaitForSeconds(1.75f);
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnCube>().SpawnNewCube();
        //timerObj.SetActive(true);
        //highScoreObj.SetActive(true);
        StartCoroutine(StartTimers());
    }

    IEnumerator WaitForMonitorToStopBouncing()
    {
        yield return new WaitForSeconds(0.5f);
        playIcon.SetActive(true);
        highscoreObj.SetActive(true);
        readyToStart = true;
    }

    IEnumerator StartTimers()
    {
        yield return new WaitForSeconds(0.5f);
        timerObj.SetActive(true);
        timerObj.GetComponent<TimerScript>().Reset();
        scoreObj.SetActive(true);
    }

    public void GameOver()
    {
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<ExplodeScript>().Explode();

        // Destroy all cubes
        GameObject[] cubes;
        cubes = GameObject.FindGameObjectsWithTag("Cube");
        for(int i =0; i < cubes.Length; i++)
        {
            Destroy(cubes[i]);
        }

        timerObj.SetActive(false);
        //GameObject.FindGameObjectWithTag("Timer").SetActive(false);
        playingGame = false;
        readyToStart = false;
        bgm.pitch = 1.0f;
        periodScale = minperiod;

        // get high score
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
        }
        StartCoroutine(WaitForExplosion());
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(4.5f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        // resetting stuff
        monitorAnim.SetBool("Activate", true);
        monitorSound.Play();
        scoreObj.SetActive(false);
        currentScore = 0;
        //playingGame = false;
        //readyToStart = false;
        //bgm.pitch = 1.0f;
        timerObj.GetComponent<TimerScript>().Reset();

        highScore = PlayerPrefs.GetInt("highscore");
        SetHighScore();
        StartCoroutine(WaitForMonitorDisplayLight());
        //bgm.Play();
    }

    void SetHighScore()
    {
        if (highScore.ToString().Length == 2)
        {
            highScoreStr = "0" + highScore.ToString();
        }
        else if (highScore.ToString().Length == 1)
        {
            highScoreStr = "00" + highScore.ToString();
        }
        else
        {
            highScoreStr = highScore.ToString();
        }

        highscoreTM.text = highScoreStr;
    }

    void PitchController()
    {
        if (playingGame && timerObj.GetComponent<TimerScript>().seconds < 5)
        {
            bgm.pitch = (maxPitch - ((maxPitch - minPitch) / timeToStartIncreasingPitch) * (timerObj.GetComponent<TimerScript>().seconds + (timerObj.GetComponent<TimerScript>().deciseconds*0.01f)));

            periodScale = (maxPeriod - ((maxPeriod - minperiod) / timeToStartIncreasingPitch) * (timerObj.GetComponent<TimerScript>().seconds + (timerObj.GetComponent<TimerScript>().deciseconds * 0.01f)));
            foreach (GameObject o in redLights)
            {
                o.GetComponent<Light>().range = Mathf.Sin(/*Time.time **/ periodScale) * 0.25f + 0.1f;
            }
        }
        else
        {
            bgm.pitch = 1.0f;
            foreach (GameObject o in redLights)
            {
                o.GetComponent<Light>().range = 0.1f;
            }
        }
    }

    //void FlashRed()
    //{
    //    if ()
        
    //}

}
