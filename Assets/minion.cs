using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class minion : MonoBehaviour
{
    private GameObject attackTarget;
    public float health;
    public float armor;
    public float attackSpeed;
    private float attackCooldown;
    public float moveSpeed;
    public bool ranged;
    public GameObject projectilePrefab;
    public float range;
    private float realRange; //in Unity units, multiply by 70/3 to get League units
    private Vector3 target;
    private NavMeshAgent agent;
    public bool attackPlayer;
    public GameObject goalTarget;
    private bool attackMoving; //are we pathing to hit an enemy?
    public ParticleSystem meleeParticles;
    public SphereCollider detectCollider;
    public float detectionRadius; //in league units, multiply by 3/70 to get Unity units
    private List<GameObject> elligibleEnemies;
    public bool blueTeam;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        realRange = range * 3 / 70;
        elligibleEnemies = new List<GameObject>();
        detectCollider.radius = (detectionRadius * 3 / 70)/2;
    }

    public void OnTriggerEnter(Collider col)
    {
		if (blueTeam && (col.gameObject.tag.Equals("Enemy") || (col.gameObject.tag.Equals("Player")&&attackPlayer)))
		{
            elligibleEnemies.Add(col.gameObject);
        }
        else if(!blueTeam && (col.gameObject.tag.Equals("Ally") || (col.gameObject.tag.Equals("Player") && attackPlayer)))
		{
            elligibleEnemies.Add(col.gameObject);
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (blueTeam && (col.gameObject.tag.Equals("Enemy") || (col.gameObject.tag.Equals("Player") && attackPlayer)))
        {
            elligibleEnemies.Remove(col.gameObject);
        }
        else if (!blueTeam && (col.gameObject.tag.Equals("Ally") || (col.gameObject.tag.Equals("Player") && attackPlayer)))
        {
            elligibleEnemies.Remove(col.gameObject);
        }
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

        if (attackTarget == null && elligibleEnemies.Count > 0)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(elligibleEnemies[0].transform.position.x, elligibleEnemies[0].transform.transform.position.z));
            if (distance > realRange)
            {
                //Debug.Log(distance);
                agent.SetDestination(target);
                attackTarget = elligibleEnemies[0];
                attackMoving = true;
                agent.isStopped = false;
            }
            else
            {
                attackTarget = elligibleEnemies[0];
                agent.isStopped = true;
            }
        }

        if (attackMoving && attackTarget != null)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(attackTarget.transform.position.x, attackTarget.transform.position.z));
            if (distance <= realRange)
            {
                agent.isStopped = true;
                attackMoving = false;
                TryAttack(attackTarget);
            }
        }
        else if (attackTarget != null)
        {
            TryAttack(attackTarget);
        }
		else
        {
            attackMoving = false;
            agent.isStopped = false;
            agent.SetDestination(goalTarget.transform.position);
        }

        
        /**
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Object hit: "+hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag.Equals("Enemy"))//we just shot a minion with our ray
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
                else //we just shot the ground or ourself, just move
                {
                    target = hit.point;
                    agent.SetDestination(target);
                    attackMoving = false;
                    agent.isStopped = false;
                    attackTarget = null;
                }
            }
        }
        **/
        //if we have target, see if we can atk
        //if no target, look for new one and move towards goal
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
            attackCooldown = 1 / attackSpeed;
        }
    }
}
