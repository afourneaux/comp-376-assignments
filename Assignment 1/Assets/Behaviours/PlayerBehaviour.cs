using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject maskPrefab;
    float speed = 300.0f;        // TODO: Parameter file
    float angleSpeed = 1000.0f;
    Vector2 aimAt = new Vector2(0.0f, 1.0f);
    GameObject target;
    Rigidbody2D rigidBody;
    Animator animator;

    void Start() {
        target = transform.Find("Target").gameObject;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        float dt = Time.deltaTime;
        UpdateAimAt(dt);
        ThrowMask();
    }

    // Handle movement
    void FixedUpdate()
    {
        float dt = Time.deltaTime;
        UpdatePosition(dt);
        UpdateFacing(dt);
    }

    void UpdateAimAt(float dt) {
        float upDown = Input.GetAxis("AimVertical");
        float leftRight = Input.GetAxis("AimHorizontal");
        Color colour = target.GetComponent<Image>().color;
        float alpha = colour.a;

        if (upDown != 0 || leftRight != 0) {
            alpha = 1.0f;

            aimAt = new Vector2(leftRight, upDown);
            aimAt.Normalize();
        } else {
            // If the aim keys are not pressed, use movement keys instead
            upDown = Input.GetAxis("Vertical");
            leftRight = Input.GetAxis("Horizontal");
            if (upDown != 0 || leftRight != 0) {
                // In this case, do not redisplay the target
                aimAt = new Vector2(leftRight, upDown);
                aimAt.Normalize();
            }
        }
        alpha -= dt;
        alpha = Mathf.Max(0, alpha);
        target.GetComponent<Image>().color = new Color(colour.r, colour.g, colour.b, alpha);
    }

    void UpdatePosition(float dt) {
        float upDown = Input.GetAxis("Vertical");
        float leftRight = Input.GetAxis("Horizontal");

        if (upDown != 0 || leftRight != 0) {
            animator.SetBool("isWalking", true);

            Vector3 direction = new Vector2(leftRight, upDown);

            rigidBody.MovePosition(transform.position + (direction * speed * dt));
        } else {
            animator.SetBool("isWalking", false);
        }
    }

    void UpdateFacing(float dt) {
        float angle = Vector2.SignedAngle(transform.up, aimAt);
        if (angle != 0) {
            float toRotate = Mathf.Sign(angle) * angleSpeed * dt;
            if (Mathf.Abs(toRotate) > Mathf.Abs(angle)) {
                toRotate = angle;
            }
            rigidBody.MoveRotation(rigidBody.rotation + angle);
        }
    }

    void ThrowMask() {
        if (Input.GetButtonDown("Fire1")) {
            GameObject mask = Instantiate(maskPrefab, transform.position, Quaternion.identity, transform.parent);
            MaskBehaviour behaviour = mask.GetComponent<MaskBehaviour>();
            behaviour.direction = aimAt;
            Physics2D.IgnoreCollision(mask.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            animator.Play("Throw");
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        StallBehaviour stall = collision.gameObject.GetComponent<StallBehaviour>();
        if (stall != null) {
            bool wasDirty = stall.Wipe();
            if (wasDirty) {
                animator.Play("Throw");
            }
        }
    }
}
