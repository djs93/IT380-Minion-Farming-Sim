using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player : MonoBehaviour
{
    private Vector3 target;
    private NavMeshAgent agent;
    public float range; //in League units, divide by 70/3 to get Unity units
    private float realRange; //in Unity units, multiply by 70/3 to get League units
    public float damage;
    public float atkSpeed; //attacks per second
    private bool attackMoving; //are we pathing to hit an enemy?
    private GameObject attackTarget; //the object we're attacking
    private float attackCooldown; //how much longer we have until we can attack
    public bool ranged;
    public GameObject projectilePrefab;
    public ParticleSystem meleeParticles;
    public GameObject rangeIndicator;
    public LayerMask rayMask;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        realRange = range * 3/70;
        rangeIndicator.transform.localScale = new Vector3(range * 3 / 70, rangeIndicator.transform.localScale.y, range * 3 / 70);
    }

    // Update is called once per frame
    void Update()
    {
		if (attackCooldown > 0)
		{
            attackCooldown -= Time.deltaTime;
		}
		if (attackCooldown < 0)
		{
            attackCooldown = 0;
		}

        if (attackTarget != null)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(attackTarget.transform.position.x, attackTarget.transform.position.z));

            if (attackMoving)
            {
                if (distance <= realRange)
                {
                    agent.isStopped = true;
                    attackMoving = false;
                    TryAttack(attackTarget);
                }
            }
            else //we have a target and weren't moving last frame
            {
                if (distance > realRange) //we need to see if it's moved out of range first
                {
                    agent.isStopped = false;
                    attackMoving = true;
                    TryAttack(attackTarget);
                }
                else
                {
                    TryAttack(attackTarget);
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100000, rayMask))
            {
                //Debug.Log("Object hit: "+hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag.Equals("Enemy"))//we just shot a minion with our ray
                {
                    if (hit.collider.gameObject != attackTarget)
                    {
                        target = hit.point;
                        float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.z));
                        if (distance > realRange)
                        {
                            //Debug.Log(distance);
                            agent.SetDestination(target);
                            attackTarget = hit.collider.gameObject;
                            attackMoving = true;
                            agent.isStopped = false;
                        }
						else
						{
                            attackTarget = hit.collider.gameObject;
                            agent.isStopped = true;
                        }
                    }
                    //agent.SetDestination(target);
                }
                else //we just shot the ground, an ally minion, or ourself, just move
                {
                    target = hit.point;
                    agent.SetDestination(target);
                    attackMoving = false;
                    agent.isStopped = false;
                    attackTarget = null;
                }
            }
        }
    }

    void TryAttack(GameObject target)
	{
		if (attackCooldown <= 0)
		{
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = rotation;
            if (ranged)
            {
                GameObject proj = Instantiate(projectilePrefab, transform.position, transform.rotation);
                proj.GetComponent<projectile>().target = target;
            }
			else
			{
                meleeParticles.Play();
            }
            attackCooldown = 1 / atkSpeed;
        }
	}
}
