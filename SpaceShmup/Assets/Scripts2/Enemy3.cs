using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 10f;
    public float rollMult = -45f;
    public float pitchMult = 30f;

    private InBoundsCheck boundCheck;

    void Awake()
    {
        boundCheck = GetComponent<InBoundsCheck>();
    }

    public Vector3 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        float yPos = pos.y;

        if (pos.y + boundCheck.radius < -boundCheck.camHeight)
        {
            Vector3 tempPos = pos;
            tempPos.y = boundCheck.camHeight + 2.5f * boundCheck.radius;
            pos = tempPos;
        }
    }

    public virtual void Move()
    {
        Debug.Log("Move Enemy!!!");
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;

    }

    void OnCollisionEnter(Collision other)
    {
        GameObject otherGO = other.gameObject;
        if (otherGO.GetComponent<ProjectileHero>() != null)
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }
    }
}
