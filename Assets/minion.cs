using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class minion : MonoBehaviour
{
    public enum MinionTypes
	{
        MT_Melee,
        MT_Ranged,
        MT_Seige
	};

    public GameObject attackTarget;
    public float damage;
    public float health;
    public float currentHealth;
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
    public player userPlayer;
    public Slider slider;
    public RectTransform executeThreshold;
    public ParticleSystem deathParticles;
    public ParticleSystem goldParticles;
    private bool deathAnimationPlayed;
    public GameObject destructionObject;
    public GoalManager goalManager;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = health;
        target = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed * 9.5f / 325;
        elligibleEnemies = new List<GameObject>();
        detectCollider.radius = (detectionRadius * 3 / 70)/2;
        gameObject.GetComponent<MeshRenderer>().material.color = blueTeam ? Color.blue : Color.red;
        var main = meleeParticles.main;
        main.startColor = blueTeam ? Color.blue : Color.red;
        executeThreshold.gameObject.SetActive(!blueTeam);
        currentHealth = health;
        RecalculateHealhbar();
    }

    public void TryAddElligibleEnemy(GameObject enemyObj)
	{
        if (blueTeam && (enemyObj.tag.Equals("Enemy")))
        {
            elligibleEnemies.Add(enemyObj);
            //Debug.Log("Added object: " + enemyObj.name);
        }
        else if (!blueTeam && (enemyObj.tag.Equals("Ally") || (enemyObj.tag.Equals("Player") && attackPlayer)))
        {
            elligibleEnemies.Add(enemyObj);
            //Debug.Log("Added object: " + enemyObj.name);
        }
    }

    public void TryRemoveElligibleEnemy(GameObject enemyObj)
    {
        if (blueTeam && (enemyObj.tag.Equals("Enemy")))
        {
            elligibleEnemies.Remove(enemyObj);
			if (elligibleEnemies.Count == 0)
			{
                attackTarget = null;
                attackMoving = false;
			}
        }
        else if (!blueTeam && (enemyObj.tag.Equals("Ally") || (enemyObj.tag.Equals("Player") && attackPlayer)))
        {
            elligibleEnemies.Remove(enemyObj);
            if (elligibleEnemies.Count == 0)
            {
                attackTarget = null;
                attackMoving = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }
            if (attackCooldown < 0)
            {
                attackCooldown = 0;
            }

			if (attackTarget && !attackTarget.activeSelf)
			{
                attackTarget = null;
			}

            if (attackTarget == null && elligibleEnemies.Count > 0) //if no target, look for one
            {
				for (int i = 0; i < elligibleEnemies.Count; i++)
                {
					if (elligibleEnemies[i] && elligibleEnemies[i].activeSelf)
                    {
                        attackTarget = elligibleEnemies[i];
                        break;
                    }
                }
                //Debug.Log("Set attack target to "+attackTarget.name);
            }

            if (attackTarget != null) //if we have target, see if we can atk
            {
                float distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.z), new Vector3(attackTarget.transform.position.x, attackTarget.transform.position.z));
                //Debug.Log("Distance: " + distance);
                if (attackMoving)
                {
                    if (distance <= range * 3 / 70)
                    {
                        //Debug.Log("Trying attack");
                        agent.isStopped = true;
                        attackMoving = false;
                        TryAttack(attackTarget);
                    }
                    else
                    {
                        agent.SetDestination(attackTarget.transform.position);
                    }
                }
                else //we have a target and weren't moving last frame
                {
                    if (distance > range * 3 / 70) //we need to see if it's moved out of range first
                    {
                        //Debug.Log("Moving closer to attack");
                        agent.isStopped = false;
                        attackMoving = true;
                        agent.SetDestination(attackTarget.transform.position);
                    }
                    else
                    {
                        //Debug.Log("Attacking");
                        TryAttack(attackTarget);
                    }
                }
            }
            else //if no target, move towards goal
            {
                agent.isStopped = false;
                attackMoving = false;
                agent.SetDestination(goalTarget.transform.position);
            }
        }
		else
		{
			if (!deathParticles.IsAlive(true)&& !goldParticles.IsAlive(true))
            {
                Die();
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
                projProjectile.fromPlayer = false;
                projProjectile.damage = damage;
            }
            else
            {
                meleeParticles.Play();
                //deal damage!
                minion targetMinion = target.GetComponent<minion>();
                if (targetMinion)
                {
                    targetMinion.TakeDamage(damage, false);
                }
            }
            attackCooldown = 1 / attackSpeed;
        }
    }

    public void RecalculateHealhbar()
	{
        slider.value = currentHealth;

        if (executeThreshold.gameObject.activeSelf)
        {
            float thresholdPosition = ((userPlayer.damage * (100 / (100 + armor))) / health) * 960;
            if (thresholdPosition > 960.0f)
            {
                thresholdPosition = 960.0f;
            }

            executeThreshold.anchoredPosition = new Vector2(thresholdPosition, executeThreshold.anchoredPosition.y);
            if (userPlayer.damage * (100 / (100 + armor)) >= currentHealth)
            {
                executeThreshold.transform.GetComponentInParent<Image>().color = Color.green;
            }
            else
            {
                executeThreshold.transform.GetComponentInParent<Image>().color = Color.white;
            }
        }
	}

    public void TakeDamage(float rawDamage, bool playerDamage)
	{
        float realDamage = rawDamage * (100 / (100 + armor));
        Debug.Log("Taking " + realDamage + "damage");
        currentHealth -= realDamage;
        RecalculateHealhbar();
		if (currentHealth <= 0 && !deathAnimationPlayed)
		{
			if (playerDamage)
			{
                goldParticles.Play();
                goalManager.AddKill();
			}
			else
            {
                deathParticles.Play();
            }
            deathAnimationPlayed = true;
		}
        if(!playerDamage && attackTarget && attackTarget.tag.Equals("Player")) //got damaged by a minion but we're targeting the player, retarget to a minion!
		{
			//pick non-character target
			for (int i = 0; i < elligibleEnemies.Count; i++)
			{
				if (elligibleEnemies[i] && !elligibleEnemies[i].tag.Equals("Player"))
				{
                    attackTarget = elligibleEnemies[i];
                }
			}
		}
	}

    public void Die()
	{
        Destroy(destructionObject);
        //ToDo: add other death things here
    }

    public static void RecalcAllHealthbars()
	{
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < minions.Length; i++)
		{
            minion minionComp = minions[i].GetComponent<minion>();
            if (minionComp && minionComp.slider && minionComp.slider.gameObject.activeSelf)
			{
                minionComp.RecalculateHealhbar();
			}
		}
    }

    public static void ToggleExecuteBars()
    {
        GameObject[] minions = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < minions.Length; i++)
        {
            minion minionComp = minions[i].GetComponent<minion>();
            if (minionComp && minionComp.executeThreshold)
            {
                minionComp.executeThreshold.gameObject.SetActive(!minionComp.executeThreshold.gameObject.activeSelf);
            }
        }
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
