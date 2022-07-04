using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float maxSpeed;

    public TargetBall target;

    public abstract void Launch();
}
