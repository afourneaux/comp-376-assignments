using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownsfolkBehaviour : MonoBehaviour
{
    float speed;
    Rigidbody2D rigidBody;
    public int direction = 0;
    public float waitTimer;
    float stepTimer = 0.0f;
    float stepTime = 0.3f;
    public float lifetime = 20.0f;
    int npcType;
    bool isMasked;
    bool isSick;
    bool isVaccinated;
    public float timeToNextCough;
    public GameObject coughAlertPrefab;

    const int NPC_TYPE_COUNT = 2;
    public Sprite[] unmaskedSprites = new Sprite[NPC_TYPE_COUNT];
    public Sprite[] maskedSprites = new Sprite[NPC_TYPE_COUNT];


    void Start()
    {
        float r = Random.Range(0.2f, 1.0f);
        float g = Random.Range(0.2f, 1.0f);
        float b = Random.Range(0.2f, 1.0f);
        transform.Find("Shoulders").GetComponent<Image>().color = new Color(r, g, b);

        speed = Random.Range(100.0f, 300.0f);
        rigidBody = GetComponent<Rigidbody2D>();

        npcType = Random.Range(0, NPC_TYPE_COUNT);
        isVaccinated = Random.Range(0.0f, 1.0f) < GameController.instance.parameters.VACCINATED[OptionsController.contagionDifficulty];
        isMasked = Random.Range(0.0f, 1.0f) < GameController.instance.parameters.MASKERS[OptionsController.contagionDifficulty];
        if (isVaccinated) {
            isSick = false;
        } else {
            isSick = Random.Range(0.0f, 1.0f) < GameController.instance.parameters.INITIAL_SPREAD[OptionsController.contagionDifficulty];
        }

        timeToNextCough = Random.Range(GameController.instance.parameters.COUGH_FREQUENCY[OptionsController.contagionDifficulty] / 2.0f, GameController.instance.parameters.COUGH_FREQUENCY[OptionsController.contagionDifficulty]);

        if (isMasked) {
            GetComponent<Image>().sprite = maskedSprites[npcType];
        } else {
            GetComponent<Image>().sprite = unmaskedSprites[npcType];
        }
    }

    void Update() {
        if (GameController.instance.gameOver) {
            Remove();
            return;
        }
        float dt = Time.deltaTime;
        timeToNextCough -= dt;
        if (timeToNextCough <= 0) {
            Cough();
            timeToNextCough = Random.Range(GameController.instance.parameters.COUGH_FREQUENCY[OptionsController.contagionDifficulty] / 2.0f, GameController.instance.parameters.COUGH_FREQUENCY[OptionsController.contagionDifficulty]);
        }
    }

    void FixedUpdate() {
        if (GameController.instance.gameOver) {
            return;
        }
        float dt = Time.fixedDeltaTime;
        lifetime -= dt;
        if (waitTimer > 0) {
            waitTimer -= dt;
        } else {
            stepTimer -= dt;
            rigidBody.MoveRotation(CodeToAngle(direction));
            float speedScalar = 1.0f;
            if (GameController.instance.majorWave) {
                speedScalar = 1.5f;
            }
            rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y) + CodeToVector2(direction) * speed * dt * speedScalar);
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

    public void CheckMaskHit(Collider2D collider) {
        if (isMasked == false) {
            MaskBehaviour mask = collider.GetComponent<MaskBehaviour>();
            if (mask != false) {
                mask.hits++;
                mask.lifespan /= 2.0f;
                isMasked = true;
                GetComponent<Image>().sprite = maskedSprites[npcType];
                GameController.instance.score += 5;
            }
        }
    }

    void Cough() {
        if (isSick && !isMasked) {
            GameObject alert = Instantiate(coughAlertPrefab, transform.position + new Vector3(20.0f, 20.0f, 0.0f), Quaternion.identity, transform.parent);
            AlertBehaviour alertBehaviour = alert.GetComponent<AlertBehaviour>();
            alertBehaviour.SetLifetime(1.0f);

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, GameController.instance.parameters.DISEASE_RANGE[OptionsController.contagionDifficulty]);
            foreach (Collider2D collider in hitColliders) {
                StallBehaviour stall = collider.GetComponent<StallBehaviour>();
                if (stall != null) {
                    stall.AddFilth();
                }

                TownsfolkBehaviour townsfolk = collider.GetComponent<TownsfolkBehaviour>();
                if (townsfolk != null) {
                    townsfolk.Infect();
                }
            }
        }
    }

    public void Infect() {
        if (isMasked || isSick || isVaccinated) {
            return;
        }
        if (Random.Range(0.0f, 1.0f) < GameController.instance.parameters.INFECT_CHANCE[OptionsController.contagionDifficulty]) {
            isSick = true;
        }
    }

    public void Remove() {
        if (isSick) {
            GameController.instance.newCases++;
        }
        if (!isMasked) {
            GameController.instance.newCases += Random.Range(3, 10);
            if (isSick) {
                GameController.instance.newCases += Random.Range(5, 20);
            }
            GameController.instance.score -= 2;
        }
        Destroy(gameObject);
    }
}
