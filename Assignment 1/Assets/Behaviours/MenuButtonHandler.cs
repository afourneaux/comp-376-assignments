using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public void OnPlayButtonClicked() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OnOptionsButtonClicked() {

    }
}
