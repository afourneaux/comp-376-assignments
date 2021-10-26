using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBehaviour : MonoBehaviour
{
    float rotationSpeed = 360.0f;
    public Vector2 direction = new Vector2(0.0f, 0.0f);
    float maxSpeed = 100.0f;   // TODO: Parameters file
    float speed;
    float timeAlive = 0.0f;
    float lifespan = 1.0f;      // TODO: Parameters file
    int maxHits = 2;            // TODO: Parameters file
    public int hits = 0;
    GameObject maskImage;

    void Start() {
        maskImage = transform.Find("MaskImage").gameObject;
        speed = maxSpeed;
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(direction * maxSpeed, ForceMode2D.Impulse);
        rigidBody.AddTorque(rotationSpeed);
    }

    void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifespan) {
            Destroy(gameObject);
        }
        if (hits >= maxHits) {
            Destroy(gameObject);
        }
    }
}
