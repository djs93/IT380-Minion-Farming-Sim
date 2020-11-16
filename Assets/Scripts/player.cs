using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player : MonoBehaviour
{
    private Vector3 target;
    private NavMeshAgent agent;
    public float range; //in League units, divide by 70/3 to get Unity units
    private float oldRange; //in League units, divide by 70/3 to get Unity units
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
    public float movementSpeed; //conversion factor is 9.5 Unity units to 325 league units for speed

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed * 9.5f / 325;
        rangeIndicator.transform.localScale = new Vector3(range * 3 / 70, rangeIndicator.transform.localScale.y, range * 3 / 70);
        oldRange = range;
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

        if(range != oldRange)
		{
            oldRange = range;
            rangeIndicator.transform.localScale = new Vector3(range * 3 / 70, rangeIndicator.transform.localScale.y, range * 3 / 70);
        }

        if (attackTarget != null)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(attackTarget.transform.position.x, attackTarget.transform.position.z));

            if (attackMoving)
            {
                if (distance <= range * 3 / 70)
                {
                    agent.isStopped = true;
                    attackMoving = false;
                    TryAttack(attackTarget);
                }
            }
            else //we have a target and weren't moving last frame
            {
                if (distance > range * 3 / 70) //we need to see if it's moved out of range first
                {
                    agent.isStopped = false;
                    attackMoving = true;
                    agent.SetDestination(attackTarget.transform.position);
                }
                else
                {
                    TryAttack(attackTarget);
                }
            }
        }

        if (Input.GetButtonDown("Fire2") || Input.GetButton("Fire2"))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100000, rayMask))
            {
                //Debug.Log("Object hit: "+hit.collider.gameObject.tag);
                if(hit.collider.gameObject.tag.Equals("Enemy"))//we just shot a minion with our ray
                {
                    minion hitMinion = hit.collider.gameObject.GetComponent<minion>();
                    if (hitMinion && hitMinion.currentHealth > 0)
                    {
                        if (hit.collider.gameObject != attackTarget)
                        {
                            target = hit.point;
                            float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.z));
                            if (distance > range * 3 / 70)
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
                    }
					else //if we clicked on a dead minion, don't attack, just move to that position
					{
                        target = hit.collider.gameObject.transform.position;
                        agent.SetDestination(target);
                        attackMoving = false;
                        agent.isStopped = false;
                        attackTarget = null;
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
                projectile projProjectile = proj.GetComponent<projectile>();
                projProjectile.target = target;
                projProjectile.fromPlayer = true;
                projProjectile.damage = damage;
            }
			else
			{
                meleeParticles.Play();
                //deal damage!
                minion targetMinion = target.GetComponent<minion>();
                if (targetMinion)
                {
                    targetMinion.TakeDamage(damage, true);
                }
            }
            attackCooldown = 1 / atkSpeed;
        }
	}

    public void ToggleRangeDisplay()
	{
        rangeIndicator.SetActive(!rangeIndicator.activeSelf);
	}

    public void SetMoveSpeed(float speed)
	{
        movementSpeed = speed;
        agent.speed = movementSpeed * 9.5f / 325;
    }
}
