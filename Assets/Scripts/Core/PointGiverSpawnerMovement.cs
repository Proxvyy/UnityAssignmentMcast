using UnityEngine;

public class PointGiverSpawnerMovement : MonoBehaviour
{
    [SerializeField] float leftLimitX = -2.0f;
    [SerializeField] float rightLimitX = 2.0f;
    [SerializeField] float moveSpeed = 2.0f;

    bool movingRight = true;

    void Update()
    {
        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += moveSpeed * Time.deltaTime;
            if (pos.x >= rightLimitX)
            {
                pos.x = rightLimitX;
                movingRight = false;
            }
        }
        else
        {
            pos.x -= moveSpeed * Time.deltaTime;
            if (pos.x <= leftLimitX)
            {
                pos.x = leftLimitX;
                movingRight = true;
            }
        }

        transform.position = pos;
    }
}
