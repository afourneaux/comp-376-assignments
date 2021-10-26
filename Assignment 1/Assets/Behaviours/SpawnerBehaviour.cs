using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public GameObject NPCPrefab;
    public int direction;
    float timeToNextSpawn;

    void Start() {
        timeToNextSpawn = Random.Range(10.0f, 15.0f);
    }

    void Update() {
        if (GameController.instance.gameOver) {
            return;
        }
        float dt = Time.deltaTime;
        if (GameController.instance.majorWave) {
            dt *= 10.0f;
        }
        timeToNextSpawn -= dt;
        if (timeToNextSpawn < 0) {
            GameObject townsfolk = Instantiate(NPCPrefab, transform.position, Quaternion.identity, transform.parent.parent);
            townsfolk.GetComponent<TownsfolkBehaviour>().direction = direction;

            timeToNextSpawn = Random.Range(20.0f, 30.0f);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider) {
        TownsfolkBehaviour townsfolk = collider.GetComponent<TownsfolkBehaviour>();
        if (townsfolk != null) {
            if (townsfolk.lifetime > 0) {
                townsfolk.direction = direction;
            } else {
                townsfolk.Remove();
            }
        }
    }
}
