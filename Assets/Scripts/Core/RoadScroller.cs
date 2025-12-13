using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2f;

    Renderer rend;
    Material mat;
    Vector2 offset;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material; // instance at runtime
        offset = Vector2.zero;
    }

    void Update()
    {
        offset.y -= scrollSpeed * Time.deltaTime;

        // URP Unlit/Lit uses _BaseMap (not always _MainTex)
        mat.SetTextureOffset("_BaseMap", offset);
    }
}
