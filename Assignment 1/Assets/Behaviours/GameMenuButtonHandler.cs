using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtonHandler : MonoBehaviour
{
    bool pauseGame = false;
    bool optionsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool input = Input.GetButtonDown("Cancel");
        if (input) {
            pauseGame = !pauseGame;
            Time.timeScale = pauseGame ? 0 : 1;
            transform.Find("Panel").gameObject.SetActive(pauseGame);

            if (pauseGame == false) {
                transform.Find("Panel/OptionsMenu").gameObject.SetActive(false);
                optionsOpen = false;
            }
        }
    }

    public void OnOptionsButtonClicked() {
        optionsOpen = !optionsOpen;
        transform.Find("Panel/OptionsMenu").gameObject.SetActive(optionsOpen);
    }

    public void OnMenuButtonClicked() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnResume() {
        pauseGame = false;
        Time.timeScale = 1;
        transform.Find("Panel/OptionsMenu").gameObject.SetActive(false);
        optionsOpen = false;
        transform.Find("Panel").gameObject.SetActive(false);
    }
}
