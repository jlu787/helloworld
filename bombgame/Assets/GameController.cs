using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool playingGame = false;
    public GameObject monitor;
    public Animator monitorAnim;
    public int currentScore = 0;
    public int highScore;
    public GameObject monitorLight;
    public GameObject timerObj;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highscore");
        timerObj.SetActive(false);
        StartCoroutine(WaitForMonitorDisplayLight());
    }

    // Update is called once per frame
    void Update()
    {
        if (playingGame == false && Input.GetMouseButtonDown(0))
        {
            playingGame = true;
            //monitor.SetActive(false);
            monitorAnim.SetBool("Activate", false);
            StartCoroutine(WaitForMonitor());
            monitorLight.SetActive(false);

        }
    }

    IEnumerator WaitForMonitorDisplayLight()
    {
        yield return new WaitForSeconds(1.0f);
        monitorLight.SetActive(true);
    }

    IEnumerator WaitForMonitor()
    {
        yield return new WaitForSeconds(1.75f);
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnCube>().SpawnNewCube();
        timerObj.SetActive(true);

    }

    public void GameOver()
    {
        GameObject.FindGameObjectWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
        GameObject.FindGameObjectWithTag("Cube").SetActive(false);
        GameObject.FindGameObjectWithTag("Timer").SetActive(false);

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


}
