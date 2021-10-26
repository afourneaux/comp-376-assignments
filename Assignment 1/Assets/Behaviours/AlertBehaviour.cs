using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehaviour : MonoBehaviour
{
    float elapsedTime = 0.0f;
    float baseY = 0.0f;
    bool hasLifetime = false;
    float lifetime = 0.0f;

    void Start() {
        baseY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if (hasLifetime) {
            lifetime -= dt;
            if (lifetime <= 0) {
                Destroy(gameObject);
            }
        }
        elapsedTime += dt;
        transform.position = new Vector3(transform.position.x, baseY + Mathf.Sin(elapsedTime) * 10.0f, transform.position.z);
    }

    public void SetLifetime(float time) {
        hasLifetime = true;
        lifetime = time;
    }
}
