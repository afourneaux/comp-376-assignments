using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStallBehaviour : MonoBehaviour
{
    float timeToReset = 3.0f;
    float timeSinceReset = 0.0f;
    GameObject marketFilth;

    void Start() {
        marketFilth = transform.Find("MarketFilth").gameObject;
    }

    void Update() {
        timeSinceReset += Time.deltaTime;
        Color colour = marketFilth.GetComponent<Image>().color;
        colour.a = ((timeSinceReset / timeToReset) * 100.0f) / 255.0f;
        marketFilth.GetComponent<Image>().color = colour;

        if (timeSinceReset >= timeToReset) {
            timeSinceReset = 0;
        }
    }
}
