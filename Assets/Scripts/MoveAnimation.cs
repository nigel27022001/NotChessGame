using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveAnimation : MonoBehaviour
{
    public float duration;
    public float elapsedTime;
    public Vector3 destination;

    public bool isMoving = false;

    public void SetVector(float x, float y)
    {
        destination = new Vector3(x, y, -1.0f);
        isMoving = true;
    }
    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float durationCompleted = elapsedTime / duration;
            transform.position = Vector3.Lerp(transform.position, destination, durationCompleted);
            if (Vector3.Distance(transform.position, destination) < 0.001f)
            {
                isMoving = false;
                elapsedTime = 0;
            }
        }
    }
}
