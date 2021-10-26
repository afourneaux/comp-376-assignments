using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWalkBehaviour : MonoBehaviour
{
    float timeToSpin = 0.0f;
    float spinSpeed = 1.0f;
    int step = 0;

    public GameObject WSADButtons;
    GameObject WKeyPress;
    GameObject SKeyPress;
    GameObject AKeyPress;
    GameObject DKeyPress;

    void Start() {
        WKeyPress = WSADButtons.transform.Find("WKeyPress").gameObject;
        SKeyPress = WSADButtons.transform.Find("SKeyPress").gameObject;
        AKeyPress = WSADButtons.transform.Find("AKeyPress").gameObject;
        DKeyPress = WSADButtons.transform.Find("DKeyPress").gameObject;

        UpdateStep();
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpin += Time.deltaTime;
        if (timeToSpin >= spinSpeed) {
            timeToSpin = 0;
            transform.Rotate(new Vector3(0.0f, 0.0f, 45.0f));

            step = (step + 1) % 8;

            UpdateStep();
        }
    }

    void UpdateStep() {
        switch (step) {
            case 0:
                WKeyPress.SetActive(true);
                SKeyPress.SetActive(false);
                AKeyPress.SetActive(false);
                DKeyPress.SetActive(false);
                break;
            case 1:
                WKeyPress.SetActive(true);
                SKeyPress.SetActive(false);
                AKeyPress.SetActive(true);
                DKeyPress.SetActive(false);
                break;
            case 2:
                WKeyPress.SetActive(false);
                SKeyPress.SetActive(false);
                AKeyPress.SetActive(true);
                DKeyPress.SetActive(false);
                break;
            case 3:
                WKeyPress.SetActive(false);
                SKeyPress.SetActive(true);
                AKeyPress.SetActive(true);
                DKeyPress.SetActive(false);
                break;
            case 4:
                WKeyPress.SetActive(false);
                SKeyPress.SetActive(true);
                AKeyPress.SetActive(false);
                DKeyPress.SetActive(false);
                break;
            case 5:
                WKeyPress.SetActive(false);
                SKeyPress.SetActive(true);
                AKeyPress.SetActive(false);
                DKeyPress.SetActive(true);
                break;
            case 6:
                WKeyPress.SetActive(false);
                SKeyPress.SetActive(false);
                AKeyPress.SetActive(false);
                DKeyPress.SetActive(true);
                break;
            case 7:
                WKeyPress.SetActive(true);
                SKeyPress.SetActive(false);
                AKeyPress.SetActive(false);
                DKeyPress.SetActive(true);
                break;
            default:
                Debug.LogError("Incorrect rotation angle: " + transform.rotation.z);
                break;
        }
    }
}
