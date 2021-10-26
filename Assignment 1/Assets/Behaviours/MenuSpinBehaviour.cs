using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpinBehaviour : MonoBehaviour
{
    float angleSpeed = 2.0f;
    float timeToSwitch = 0.0f;
    float nextSwitch = 0.0f;
    
    public GameObject ArrowKeys;
    GameObject UpKeyPress;
    GameObject DownKeyPress;
    GameObject LeftKeyPress;
    GameObject RightKeyPress;
    Vector2 lookAtPoint;

    int upDown = 0;
    int leftRight = 0;

    float destinationAngle;
    float remainingAngle;
    

    void Start() {
        UpKeyPress = ArrowKeys.transform.Find("UpKeyPress").gameObject;
        DownKeyPress = ArrowKeys.transform.Find("DownKeyPress").gameObject;
        LeftKeyPress = ArrowKeys.transform.Find("LeftKeyPress").gameObject;
        RightKeyPress = ArrowKeys.transform.Find("RightKeyPress").gameObject;
        lookAtPoint = new Vector2(0.0f, 1.0f);
        remainingAngle = destinationAngle = 0.0f;

        UpdateHighlight();
    }


    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        timeToSwitch += dt;
        if (timeToSwitch >= nextSwitch) {
            nextSwitch = Random.Range(1f, 2.0f);
            timeToSwitch = 0;

            upDown = Random.Range(-1, 2);
            leftRight = Random.Range(-1, 2);

            if (upDown != 0 || leftRight != 0) {
                lookAtPoint = new Vector2(leftRight, upDown);
                lookAtPoint.Normalize();
                destinationAngle = remainingAngle = Vector2.SignedAngle(transform.up, lookAtPoint);
            }
            UpdateHighlight();
        }

        float toRotate = destinationAngle * dt * angleSpeed;
        if (Mathf.Abs(remainingAngle) < Mathf.Abs(toRotate)) {
            toRotate = remainingAngle;
        }
        remainingAngle -= toRotate;

        transform.Rotate(new Vector3(0.0f, 0.0f, toRotate));
    }

    void UpdateHighlight() {
        // TODO: Optimise by tracking a bool and SetActive at the end
        UpKeyPress.SetActive(false);
        DownKeyPress.SetActive(false);
        LeftKeyPress.SetActive(false);
        RightKeyPress.SetActive(false);

        if (upDown > 0) {
            UpKeyPress.SetActive(true);
        }
        if (upDown < 0) {
            DownKeyPress.SetActive(true);
        }
        if (leftRight > 0) {
            RightKeyPress.SetActive(true);
        }
        if (leftRight < 0) {
            LeftKeyPress.SetActive(true);
        }
    }
}
