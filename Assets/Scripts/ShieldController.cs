using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            Vector3 direction = mousePosition - transform.position;

            float zRotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Vector3 rotation = new Vector3(0, 0, zRotation - 90.0f);

            transform.eulerAngles = rotation;
        }
    }
}
