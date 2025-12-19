using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}
