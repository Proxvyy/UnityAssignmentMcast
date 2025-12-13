using UnityEngine;

public class ObstacleMoveDown : MonoBehaviour
{
    public float speed = 4f;
    public float destroyY = -7f;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y <= destroyY)
        {
            Destroy(gameObject);
        }
    }
}
