using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public GameObject optionsMenu;

    public void OnPlayButtonClicked() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OnMenuButtonClicked() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnOptionsButtonClicked() {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }
}
