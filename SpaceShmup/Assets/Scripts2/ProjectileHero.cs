using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHero : MonoBehaviour
{

    private InBoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<InBoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        float posY = transform.position.y;
        if (posY - bndCheck.radius > bndCheck.camHeight)
        {
            Destroy(gameObject);
        }
    }
}
