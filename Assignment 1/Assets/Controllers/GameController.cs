using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    float timeToMajorWave;
    const int WAVES = 3;
    int wave = 0;
    float timeToEnd = 300.0f;
    float warningFlashTimer = 0.0f;
    bool warningFlashOn = false;
    public bool majorWave = false;
    public bool gameOver = false;
    public float score = 0;
    public float newCases = 0;
    public GameObject maskCount;
    public GameObject scoreBox;
    public GameObject cleaningBarrel;
    public GameObject player;
    public GameObject clock;
    public GameObject warning;
    public GameObject endscreen;
    TMPro.TMP_Text maskTextBox;
    TMPro.TMP_Text scoreTextBox;
    TMPro.TMP_Text casesTextBox;
    TMPro.TMP_Text clockTextBox;
    PlayerBehaviour playerBehaviour;
    GameObject maskAlert;
    public Parameters parameters;
    

    void OnEnable() {
        instance = this;
    }

    void Start()
    {
        maskTextBox = maskCount.transform.Find("MaskText").GetComponent<TMPro.TMP_Text>();
        casesTextBox = scoreBox.transform.Find("CasesText").GetComponent<TMPro.TMP_Text>();
        scoreTextBox = scoreBox.transform.Find("ScoreText").GetComponent<TMPro.TMP_Text>();
        clockTextBox = clock.GetComponent<TMPro.TMP_Text>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        maskAlert = cleaningBarrel.transform.Find("MaskAlert").gameObject;
        parameters = new Parameters();

        timeToMajorWave = Random.Range(timeToEnd / 6.0f, timeToEnd / 3.0f);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        timeToEnd -= dt;
        timeToMajorWave -= dt;

        if (gameOver) {
            clockTextBox.text = "0 : 00";
        } else {
            int seconds = Mathf.RoundToInt(timeToEnd % 60);
            string clockString = Mathf.FloorToInt(timeToEnd / 60) + " : ";
            if (seconds < 10) {
                clockString += "0";
            }
            clockString += seconds;
            clockTextBox.text = clockString;
        }

        if (timeToEnd <= 0) {
            gameOver = true;
            endscreen.SetActive(true);
            endscreen.transform.Find("Backdrop/ScoreText").GetComponent<TMPro.TMP_Text>().text = score.ToString();
            endscreen.transform.Find("Backdrop/CasesText").GetComponent<TMPro.TMP_Text>().text = newCases.ToString();
        }

        if (wave < WAVES || majorWave) {
            if (timeToMajorWave <= 0) {
                if (majorWave == false) {
                    wave++;
                    majorWave = true;
                    timeToMajorWave = 10.0f;
                } else {
                    majorWave = false;
                    timeToMajorWave = Random.Range(timeToEnd / 4.0f, timeToEnd / 2.0f);
                }
            }
        }
        
        if (majorWave) {
            warningFlashTimer -= dt;
            if (warningFlashTimer <= 0) {
                warningFlashTimer = 1.0f;
                warningFlashOn = !warningFlashOn;
            }
        } else {
            warningFlashOn = false;
        }
        warning.SetActive(warningFlashOn);

        maskTextBox.text = "x " + playerBehaviour.masksToThrow;
        casesTextBox.text = "New cases: " + newCases;
        scoreTextBox.text = "Score: " + score;
        maskAlert.SetActive(playerBehaviour.masksToThrow <= 0);
    }
}
