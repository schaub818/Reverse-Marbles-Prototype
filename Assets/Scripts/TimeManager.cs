using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float startingTime;

    public bool Paused;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Paused)
        {
            startingTime -= Time.deltaTime;

            text.text = "Time Left: " + Mathf.Round(startingTime);
        }
    }
}