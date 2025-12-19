using UnityEngine;

public class PointGiverMissDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PointGiver pg = other.GetComponent<PointGiver>();
        if (pg == null)
            return;

        if (PointGiverProgressManager.instance != null)
        {
            PointGiverProgressManager.instance.RegisterMissed();
        }

        Destroy(other.gameObject);
    }
}
