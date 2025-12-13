using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float minX = -2.3f;
    public float maxX = 2.3f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x + h * speed * Time.deltaTime, minX, maxX);
        transform.position = pos;
    }
}
