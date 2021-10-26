using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuButtonHandler : MonoBehaviour
{
    bool pauseGame = false;
    bool optionsOpen = false;
    void Update()
    {
        bool input = Input.GetButtonDown("Cancel");
        if (input) {
            AudioController.instance.PlaySFX("clack");
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
        AudioController.instance.PlaySFX("clack");
    }

    public void OnMenuButtonClicked() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        AudioController.instance.PlaySFX("clack");
    }

    public void OnResume() {
        AudioController.instance.PlaySFX("clack");
        pauseGame = false;
        Time.timeScale = 1;
        transform.Find("Panel/OptionsMenu").gameObject.SetActive(false);
        optionsOpen = false;
        transform.Find("Panel").gameObject.SetActive(false);
    }
}
