using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float distance = 0.01f;

    public void MoveUp(float distance)
    {
        MoveTransformation(Vector3.up * distance);
    }

    public void MoveDown(float distance)
    {
        MoveTransformation(Vector3.down * distance);
    }

    public void MoveLeft(float distance)
    {
        MoveTransformation(Vector3.left * distance);
    }

    public void MoveRight(float distance)
    {
        MoveTransformation(Vector3.right * distance);
    }

    void MoveTransformation(Vector3 direction)
    {
        // Get the current position
        Vector3 currentPosition = transform.position;

        // Add the specified direction to the current position
        currentPosition += direction;

        // Set the new position
        transform.position = currentPosition;
    }
}
