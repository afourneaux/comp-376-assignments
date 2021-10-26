using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCleanBehaviour : MonoBehaviour
{
    float timeToReset = 3.0f;
    float timeSinceReset = 0.0f;
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timeSinceReset += Time.deltaTime;
        if (timeSinceReset >= timeToReset) {
            animator.Play("playerThrow");
            timeSinceReset = 0;
        }
    }
}
