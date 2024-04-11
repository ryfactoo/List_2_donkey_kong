using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed = 3f; // Prędkość ruchu
    public float moveDistance = 3f; // Zakres ruchu
    private bool movingRight = true; // Kierunek ruchu (w prawo)

    void Update()
    {
        // Jeśli platforma jest na skraju swojego zakresu, zmień kierunek
        if (transform.position.x >= moveDistance && movingRight)
        {
            movingRight = false;
        }
        else if (transform.position.x <= 0 && !movingRight)
        {
            movingRight = true;
        }

        // Ustaw wektor prędkości na podstawie aktualnego kierunku ruchu
        float moveDirection = movingRight ? 1 : -1;
        Vector3 movement = new Vector3(moveDirection, 0f, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
