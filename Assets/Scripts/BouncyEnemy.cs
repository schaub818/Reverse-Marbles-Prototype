using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyEnemy : Enemy
{
    private Rigidbody2D rigidBody;

    private Vector2 moveDirection;

    private bool bouncingBack = false;

    private float bounceTimer = 0.0f;

    public int hp;

    public float bounceBackTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TargetBall hitBall;

        bool hitTargetBall = collision.TryGetComponent<TargetBall>(out hitBall);

        if (hitTargetBall)
        {
            collision.attachedRigidbody.velocity = rigidBody.velocity;
        }

        if (collision.gameObject.tag != "Field" && collision.gameObject.tag != "GoalArea")
        {
            if (hp > 0)
            {
                hp--;

                int randomDirection = Random.Range(0, 2);

                if (randomDirection < 1)
                {
                    randomDirection = -1;
                }

                Quaternion rotation = Quaternion.AngleAxis(135.0f * randomDirection, Vector3.forward);

                moveDirection = rotation * moveDirection;

                rigidBody.velocity = moveDirection;

                bouncingBack = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (bouncingBack)
        {
            bounceTimer += Time.deltaTime;

            if (bounceTimer >= bounceBackTime)
            {
                bouncingBack = false;
                bounceTimer = 0.0f;

                Launch();
            }
        }
    }

    public override void Launch()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        moveDirection = target.transform.position - transform.position;

        moveDirection.Normalize();
        moveDirection *= maxSpeed;

        rigidBody.velocity = moveDirection;
    }
}
