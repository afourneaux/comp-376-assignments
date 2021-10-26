using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownsfolkBehaviour : MonoBehaviour
{
    float speed;
    float angleSpeed = 1000.0f;
    bool isMove = false;
    Rigidbody2D rigidBody;
    public int direction;
    public float waitTimer;
    float stepTimer;
    float stepTime = 0.3f;
    public float lifetime = 20.0f;
    int npcType;
    bool masked;

    const int NPC_TYPE_COUNT = 2;
    public Sprite[] unmaskedSprites = new Sprite[NPC_TYPE_COUNT];
    public Sprite[] maskedSprites = new Sprite[NPC_TYPE_COUNT];

    // Start is called before the first frame update
    void Start()
    {
        float r = Random.Range(0.2f, 1.0f);
        float g = Random.Range(0.2f, 1.0f);
        float b = Random.Range(0.2f, 1.0f);
        transform.Find("Shoulders").GetComponent<Image>().color = new Color(r, g, b);

        speed = Random.Range(100.0f, 300.0f);
        rigidBody = GetComponent<Rigidbody2D>();

        npcType = Random.Range(0, NPC_TYPE_COUNT);
        masked = Random.Range(0.0f, 1.0f) > 0.4f;

        if (masked) {
            GetComponent<Image>().sprite = maskedSprites[npcType];
        } else {
            GetComponent<Image>().sprite = unmaskedSprites[npcType];
        }
    }

    void FixedUpdate() {
        float dt = Time.fixedDeltaTime;
        lifetime -= dt;
        if (waitTimer > 0) {
            waitTimer -= dt;
        } else {
            stepTimer -= dt;
            rigidBody.MoveRotation(CodeToAngle(direction));
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + CodeToVector2(direction) * speed * dt);
        }
        if (stepTimer < 0) {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            stepTimer = stepTime;
        }
    }

    public void SetFacing(int direction) {
        rigidBody.SetRotation(CodeToAngle(direction));
    }

    float CodeToAngle(int direction) {
        float angle = 0.0f;
        switch(direction) {
            case 0:
                angle = 0.0f;
                break;
            case 1:
                angle = 180.0f;
                break;
            case 2:
                angle = 270.0f;
                break;
            case 3:
                angle = 90.0f;
                break;
            default:
                Debug.LogError("TownsfolkBehaviour.CodeToAngle: Invalid direction code " + direction);
                break;
        }
        return angle;
    }

    Vector2 CodeToVector2(int direction) {
        Vector2 vector = Vector2.up;
        switch(direction) {
            case 0:
                vector = Vector2.up;
                break;
            case 1:
                vector = Vector2.down;
                break;
            case 2:
                vector = Vector2.right;
                break;
            case 3:
                vector = Vector2.left;
                break;
            default:
                Debug.LogError("TownsfolkBehaviour.CodeToVector2: Invalid direction code " + direction);
                break;
        }
        return vector;
    }

    public void Remove() {
        Destroy(gameObject);
    }
}
