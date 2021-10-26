using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodeBehaviour : MonoBehaviour
{
    public bool N;
    public bool S;
    public bool E;
    public bool W;
    public bool WAIT;
    public int WAITDIR; // 0N 1S 2E 3W
    public List<int> options;

    void Start() {
        if (N) {
            options.Add(0);
        }
        if (S) {
            options.Add(1);
        }
        if (E) {
            options.Add(2);
        }
        if (W) {
            options.Add(3);
        }
        if (options.Count <= 0) {
            Debug.LogError("Node has no exit points!");
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        TownsfolkBehaviour townsfolk = collision.GetComponent<TownsfolkBehaviour>();
        if (townsfolk != null) {
            StartCoroutine(DelayedTownsfolkAction(townsfolk));
            
        }
    }

    IEnumerator DelayedTownsfolkAction(TownsfolkBehaviour townsfolk) {
        yield return new WaitForSeconds(0.5f);
        int selection = Random.Range(0, options.Count);
        townsfolk.direction = options[selection];

        if (WAIT) {
            if (Random.Range(0.0f, 1.0f) > 0.5f) {
                townsfolk.SetFacing(WAITDIR);
                townsfolk.waitTimer = Random.Range(2.0f, 5.0f);
            }
        }
    }
}
