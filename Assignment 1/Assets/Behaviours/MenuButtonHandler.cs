using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public GameObject optionsMenu;
    public Slider musicSlider;
    public Slider SFXSlider;

    void Start() {
        OnContagionDifficultySelect(OptionsController.contagionDifficulty);
        OnMaskCapacitySelect(OptionsController.maskCapacity);
        musicSlider.value = OptionsController.musicVolume;
        SFXSlider.value = OptionsController.sfxVolume;
    }

    public void OnPlayButtonClicked() {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OnMenuButtonClicked() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OnOptionsButtonClicked() {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void OnContagionDifficultySelect(int index) {
        bool active1 = false;
        bool active2 = false;
        bool active3 = false;
        switch(index) {
            case 0:
                active1 = true;
            break;
                case 1:
                active2 = true;
            break;
                case 2:
                active3 = true;
                break;
            default:
                Debug.LogError("OnContagionDifficultySelect called with invalid value: " + index);
                break;
        }

        optionsMenu.transform.Find("Image/SkullImage1").gameObject.SetActive(active1);
        optionsMenu.transform.Find("Image/SkullImage2").gameObject.SetActive(active2);
        optionsMenu.transform.Find("Image/SkullImage3").gameObject.SetActive(active3);

        OptionsController.contagionDifficulty = index;
    }

    public void OnMaskCapacitySelect(int index) {
        bool active1 = false;
        bool active2 = false;
        bool active3 = false;
        switch(index) {
            case 0:
                active1 = true;
            break;
                case 1:
                active2 = true;
            break;
                case 2:
                active3 = true;
                break;
            default:
                Debug.LogError("OnMaskCapacitySelect called with invalid value: " + index);
                break;
        }

        optionsMenu.transform.Find("Image/MaskImage1").gameObject.SetActive(active1);
        optionsMenu.transform.Find("Image/MaskImage2").gameObject.SetActive(active2);
        optionsMenu.transform.Find("Image/MaskImage3").gameObject.SetActive(active3);

        OptionsController.maskCapacity = index;
    }

    public void OnMusicVolumeChanged() {
        OptionsController.musicVolume = musicSlider.value;
        AudioController.instance.UpdateVolumeMusic(OptionsController.musicVolume);
    }

    public void OnSFXVolumeChanged() {
        OptionsController.sfxVolume = SFXSlider.value;
        AudioController.instance.UpdateVolumeSFX(OptionsController.sfxVolume);
    }
}
