using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StallBehaviour : MonoBehaviour
{
    float filth = 0.0f;     // Scale from 0.0 to 1.0;
    GameObject filthLayer;

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
        Color colour = filthLayer.GetComponent<Image>().color;
        colour.a = filth;
        filthLayer.GetComponent<Image>().color = colour;
    }

    public bool Wipe() {
        bool wasDirty = filth > 0.0f;
        filth = 0.0f;
        return wasDirty;
    }
}
