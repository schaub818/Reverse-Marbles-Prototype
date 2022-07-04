using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBall : MonoBehaviour
{
    [HideInInspector]
    public bool isInGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GoalArea")
        {
            isInGoal = true;
        }

        if (collision.gameObject.tag == "Shield" || collision.gameObject.tag == "TargetBall")
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

            rigidbody.drag = 1.0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GoalArea")
        {
            isInGoal = false;
        }
    }
}
