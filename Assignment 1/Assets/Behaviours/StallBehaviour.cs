using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StallBehaviour : MonoBehaviour
{
    float filth = 0.0f;     // Scale from 0.0 to 1.0;
    float spreadTimer = 0.0f;
    float spreadTime = 10.0f;
    GameObject filthLayer;
    public GameObject AlertPrefab;

    // Start is called before the first frame update
    void Start()
    {
        float r = Random.Range(0.5f, 1.0f);
        float g = Random.Range(0.5f, 1.0f);
        float b = Random.Range(0.5f, 1.0f);
        GameObject stallColours = transform.Find("MarketColour").gameObject;
        stallColours.GetComponent<Image>().color = new Color(r, g, b);
        filthLayer = transform.Find("MarketFilth").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.gameOver) {
            return;
        }
        float dt = Time.deltaTime;
        spreadTimer -= dt;
        if (spreadTimer <= 0) {
            spreadTimer = spreadTime;
            SpreadFilth();
        }
        Color colour = filthLayer.GetComponent<Image>().color;
        colour.a = filth;
        filthLayer.GetComponent<Image>().color = colour;
    }

    public float Wipe() {
        float oldFilth = filth;
        filth = 0.0f;
        return oldFilth;
    }
    
    public void AddFilth() {
        filth += 0.1f;
        if (filth >= 1.0f) {
            filth = 1.0f;

            Instantiate(AlertPrefab, transform.position, Quaternion.identity, transform.parent.parent).GetComponent<AlertBehaviour>().SetLifetime(5.0f);
        }
    }

    void SpreadFilth() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, GameController.instance.parameters.DISEASE_RANGE[OptionsController.contagionDifficulty] * 4.0f);
        foreach (Collider2D collider in hitColliders) {
            if (Random.Range(0.0f, 1.0f) < filth) {
                StallBehaviour stall = collider.GetComponent<StallBehaviour>();
                if (stall != null) {
                    stall.AddFilth();
                }

                TownsfolkBehaviour townsfolk = collider.GetComponent<TownsfolkBehaviour>();
                if (townsfolk != null) {
                    townsfolk.Infect();
                }
            }
        }
    }
}
