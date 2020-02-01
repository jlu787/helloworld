using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public bool paused = false;
    public float timePerRound = 10;
    public float seconds;
    public float deciseconds = 0;
    public TextMesh text;
    private string secondsStr;
    private string decisecondsStr;
    private bool explodedYet = false;

    // Start is called before the first frame update
    void Start()
    {
        seconds = timePerRound;
        text.text = ((int)timePerRound).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (seconds <= 0 && deciseconds <= 0)
            {
                if (!explodedYet)
                {
                    text.text = "00:00";
                    // DIE
                    //GameObject.FindGameObjectWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                    GameObject.FindGameObjectWithTag("Controller").GetComponent<GameController>().GameOver();
                    explodedYet = true;
                }

            }
            else
            {
                if (deciseconds <= 0.0f)
                {
                    seconds--;
                    deciseconds = 100;
                }

                deciseconds -= Time.deltaTime * 100f;
                if (seconds.ToString().Length == 1)
                {
                    secondsStr = "0" + seconds.ToString();
                }
                else secondsStr = seconds.ToString();

                if (((int)deciseconds).ToString().Length == 1)
                {
                    decisecondsStr = "0" + ((int)deciseconds).ToString();
                }
                else
                {
                    decisecondsStr = ((int)deciseconds).ToString();
                }
                text.text = secondsStr + ":" + decisecondsStr;

            }
        }
      
    }

    public void Reset()
    {
        explodedYet = false;
        seconds = timePerRound;
        deciseconds = 0.0f;
        text.text = ((int)timePerRound).ToString();
        paused = false;
    }

    // increase seconds by this much when a bomb is set
    public void BombSet()
    {
        seconds+=2;
    }
}
