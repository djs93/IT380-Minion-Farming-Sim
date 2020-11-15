using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 20;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Translate(0, 0, speed*Time.deltaTime);
    }
}
