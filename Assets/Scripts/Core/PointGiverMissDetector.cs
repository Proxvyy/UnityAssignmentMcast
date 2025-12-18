using UnityEngine;

public class PointGiverMissDetector : MonoBehaviour
{
    [SerializeField] float destroyBelowY = -6.0f;

    bool resolved = false;

    void Update()
    {
        if (resolved)
        {
            return;
        }

        if (transform.position.y < destroyBelowY)
        {
            resolved = true;

            if (PointGiverProgressManager.Instance != null)
            {
                PointGiverProgressManager.Instance.RegisterResolved();
            }

            Destroy(gameObject);
        }
    }
}
