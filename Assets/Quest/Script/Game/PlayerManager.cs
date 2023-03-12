using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed = 10.0f;

    float translationX, translationZ;

    void Update()
    {
        translationZ = Input.GetAxis("Vertical") * speed;
        translationX = Input.GetAxis("Horizontal") * speed;

        translationZ *= Time.deltaTime;
        translationX *= Time.deltaTime;

        transform.Translate(translationX, 0, translationZ);
    }
}
