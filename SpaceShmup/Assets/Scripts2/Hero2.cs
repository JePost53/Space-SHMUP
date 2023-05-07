using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero2 : MonoBehaviour
{

    // Singleton
    static public Hero2 S { get; private set; }

    [Header("Inscribed")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    public float gameRestartDelay = 2f;

    [Header("Dynamic")]
    [Range(0,4)]
    public float shieldLevel = 1;

    public GameObject projectilePrefab;
    public float projectileSpeed = 50f;

    public float reloadTime = 0.25f;
    private float reloadLeft = 0;

    void Awake()
    {
        if (S == null)
            S = this;
        else
            UnityEngine.Debug.LogError("Hero.Awake()");
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(vAxis * pitchMult, hAxis * rollMult, 0);

        if (Input.GetKey(KeyCode.Space) && reloadLeft <= 0)
        {
            Fire();
            reloadLeft = reloadTime;
        }
        else if (reloadLeft > 0)
            reloadLeft -= Time.deltaTime;
    }

    void Fire()
    {
        GameObject projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = transform.position;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * projectileSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        UnityEngine.Debug.Log(go.gameObject.name);

        if (go.tag == "Enemy")
        {
            Destroy(go);

            shieldLevel--;
            if (shieldLevel <= 0)
            {
                Main.HERO_DIED();
                Destroy(this.gameObject);
            }
        }
    }
}
