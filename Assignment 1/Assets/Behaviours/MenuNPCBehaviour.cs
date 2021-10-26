using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNPCBehaviour : MonoBehaviour
{
    bool masked = false;
    float resetTimer = 0.0f;
    float timeToReset = 1.0f;
    public Sprite unmaskedSprite;
    public Sprite maskedSprite;

    // Update is called once per frame
    void Update()
    {
        if (masked) {
            resetTimer += Time.deltaTime;
            if (resetTimer >= timeToReset) {
                resetTimer = 0.0f;
                masked = false;
                transform.GetComponent<Image>().sprite = unmaskedSprite;
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        MaskBehaviour mask = collider.GetComponent<MaskBehaviour>();
        if (mask != null) {
            mask.hits++;
            masked = true;
            transform.GetComponent<Image>().sprite = maskedSprite;
        }
    }
}
