using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballEnemy : Enemy
{
    private bool launched = false;

    private float currentRotation;
    private float rotationDirection;
    
    private Rigidbody2D rigidBody;

    private Vector2 moveDirection;

    private Quaternion rotation;

    public float rotationIncrement;
    public float maximumRotation;

    private void Start()
    {
        currentRotation = 0.0f;
        rotationDirection = 1.0f;
    }

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

    private void FixedUpdate()
    {
        if (launched)
        {
            float frameRotation = rotationIncrement * rotationDirection;

            currentRotation += frameRotation;

            if (currentRotation >= maximumRotation || currentRotation <= -maximumRotation)
            {
                rotationDirection = -rotationDirection;
            }

            rotation = Quaternion.AngleAxis(frameRotation, Vector3.forward);

            moveDirection = rotation * moveDirection;

            rigidBody.velocity = moveDirection;
        }
    }

    public override void Launch()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        moveDirection = target.transform.position - transform.position;

        moveDirection.Normalize();
        moveDirection *= maxSpeed;

        rigidBody.velocity = moveDirection;

        launched = true;
    }
}
