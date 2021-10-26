using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHorseBehaviour : MonoBehaviour
{
    float screenWidth = 800.0f;
    float speed = 100.0f;
    float timePassed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        timePassed += dt;
        transform.Translate(new Vector3(-speed * dt, 0.0f, 0.0f));
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Pow(Mathf.Sin(timePassed * 5), 55) * 5.0f);
        if (transform.localPosition.x <= -screenWidth) {
            transform.localPosition = new Vector3(screenWidth, transform.localPosition.y, 0.0f);
        }
    }
}
