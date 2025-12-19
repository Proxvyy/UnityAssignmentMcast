using UnityEngine;

public class PointGiverMover : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }
}
