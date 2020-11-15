﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject target;
    public float speed = 20;
    public float damage;
    public bool fromPlayer;
    private bool dying = false; //we have to wait for the trail to die, or else it looks weird
    private float deathCooldown = 0.2f;
    public Vector3 deathTarget;
    // Start is called before the first frame update
    void Start()
    {
        deathTarget = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            if (target)
            {
                transform.LookAt(target.transform);
            }
			else
			{
                transform.LookAt(deathTarget);
            }
            transform.Translate(0, 0, speed * Time.deltaTime);
			if (!target)
			{
                if(Mathf.Abs(Vector3.Distance(transform.position, deathTarget)) <= 1.1f)
				{
                    dying = true;
				}
			}
        }
		else
		{
            if (deathCooldown <= 0)
            {
                Destroy(this);
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
		}
    }

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == target)
		{
            //deal damage!
            minion targetMinion = target.GetComponent<minion>();
			if (targetMinion)
			{
                targetMinion.TakeDamage(damage, fromPlayer);
			}
            dying = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
