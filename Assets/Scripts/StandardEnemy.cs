using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : Enemy
{
    private Rigidbody2D rigidBody;

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
            Destroy(gameObject);
        }
    }

    public override void Launch()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        Vector2 moveDirection = target.transform.position - transform.position;

        moveDirection.Normalize();
        moveDirection *= maxSpeed;

        rigidBody.velocity = moveDirection;
    }
}
