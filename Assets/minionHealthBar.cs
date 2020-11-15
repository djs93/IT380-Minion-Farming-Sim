using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionHealthBar : MonoBehaviour
{
    public GameObject attachObject;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(attachObject.transform.position.x, transform.position.y, attachObject.transform.position.z);
    }
}
