using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBoundsCheck : MonoBehaviour
{
    [Header("Inscribed")]
    public float radius = 1f;

    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;

    public bool keepOnScreen;

    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -camWidth + radius, camWidth - radius);
        if (keepOnScreen == true)
            pos.y = Mathf.Clamp(pos.y, -camHeight + radius, camHeight - radius);

        transform.position = pos;
    }
}
