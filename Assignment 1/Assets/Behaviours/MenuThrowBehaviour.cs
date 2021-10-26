using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuThrowBehaviour : MonoBehaviour
{
    public GameObject maskPrefab;
    public GameObject spaceKey;
    public GameObject InstructionsPanel;
    GameObject spaceKeyPress;
    Animator animator;
    float timeToThrow = 0.0f;
    float throwDelay = 2.0f;
    float keyPressTime = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spaceKeyPress = spaceKey.transform.Find("SpaceKeyPress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timeToThrow += Time.deltaTime;
        if (timeToThrow >= throwDelay) {
            animator.Play("playerThrow");
            timeToThrow = 0.0f;

            GameObject newMask = Instantiate(maskPrefab, transform.position, Quaternion.identity, InstructionsPanel.transform);
            MaskBehaviour maskBehaviour = newMask.GetComponent<MaskBehaviour>();
            maskBehaviour.direction = Vector2.right;
        }
        if (timeToThrow <= keyPressTime) {
            spaceKeyPress.SetActive(true);
        } else {
            spaceKeyPress.SetActive(false);
        }
    }
}
