using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBehaviour : MonoBehaviour
{
    float rotationSpeed = 360.0f;
    public Vector2 direction = new Vector2(0.0f, 0.0f);
    float maxSpeed = 1000.0f;   // TODO: Parameters file
    float speed;
    float timeAlive = 0.0f;
    float lifespan = 1.0f;      // TODO: Parameters file
    int maxHits = 2;            // TODO: Parameters file
    public int hits = 0;
    GameObject maskImage;

    void Start() {
        maskImage = transform.Find("MaskImage").gameObject;
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        timeAlive += dt;
        transform.Translate(direction.x * speed * dt, direction.y * speed * dt, 0.0f);
        speed -= dt * (maxSpeed / lifespan);
        speed = Mathf.Max(0, speed);
        maskImage.transform.Rotate(new Vector3(0.0f, 0.0f, rotationSpeed * dt));
        if (timeAlive >= lifespan) {
            Destroy(gameObject);
        }
        if (hits >= maxHits) {
            Destroy(gameObject);
        }
    }
}
