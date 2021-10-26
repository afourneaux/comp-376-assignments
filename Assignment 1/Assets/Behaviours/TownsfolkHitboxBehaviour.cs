using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownsfolkHitboxBehaviour : MonoBehaviour
{
    public GameObject parent;
    TownsfolkBehaviour parentBehaviour;

    void Start() {
        parentBehaviour = parent.GetComponent<TownsfolkBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        parentBehaviour.CheckMaskHit(collider);
    }
}
