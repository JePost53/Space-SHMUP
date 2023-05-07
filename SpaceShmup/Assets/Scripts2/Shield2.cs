using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield2 : MonoBehaviour
{

    [Header("Inscribed")]
    public float rotationsPerSecond = 0.1f;

    [Header("Dynamic")]
    public int levelShown = 0;

    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        int shieldLevel = Mathf.FloorToInt(Hero2.S.shieldLevel);
        if (levelShown != shieldLevel)
        {
            levelShown = shieldLevel;

        }

        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
