using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform blueSpawn;
    public Transform redSpawn;
    private float timeUntilNextSpawn;
    public float timeInBetweenWaveMinions;
    public float timeInBetweenWaves;
    public List<minion.MinionTypes> waveComposition;
    private int currentSpawnPos = 0;
    public player userPlayer;
    public GameObject blueTarget;
    public GameObject redTarget;
    public GoalManager goalManager;

    public GameObject meleeMinionPrefab;
    public GameObject magicMinionPrefab;
    public GameObject seigeMinionPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
		if (timeUntilNextSpawn <= 0)
		{
            SpawnMinionMirrored(waveComposition[currentSpawnPos]);
            //spawn next minion in sequence
            currentSpawnPos++;
			if (currentSpawnPos == waveComposition.Count)
			{
                currentSpawnPos = 0;
                timeUntilNextSpawn = timeInBetweenWaves;
			}
			else
			{
                timeUntilNextSpawn = timeInBetweenWaveMinions;
            }
		}
		else
		{
            timeUntilNextSpawn -= Time.deltaTime;
		}
    }

	void SpawnMinionMirrored(minion.MinionTypes minionType)
	{
        GameObject newMinion;
        minion newMinionMinion;

        switch (minionType)
		{
			case minion.MinionTypes.MT_Melee:
                newMinion = Instantiate(meleeMinionPrefab, blueSpawn.position, blueSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = true;
                newMinionMinion.attackPlayer = false;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = blueTarget;
                newMinionMinion.goalManager = goalManager;
                newMinion.tag = "Ally";
                newMinionMinion.gameObject.tag = "Ally";

                newMinion = Instantiate(meleeMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = false;
                newMinionMinion.attackPlayer = true;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = redTarget;
                newMinionMinion.goalManager = goalManager;
                break;
			case minion.MinionTypes.MT_Ranged:
                newMinion = Instantiate(magicMinionPrefab, blueSpawn.position, blueSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = true;
                newMinionMinion.attackPlayer = false;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = blueTarget;
                newMinionMinion.goalManager = goalManager;
                newMinion.tag = "Ally";
                newMinionMinion.gameObject.tag = "Ally";

                newMinion = Instantiate(magicMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = false;
                newMinionMinion.attackPlayer = true;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = redTarget;
                newMinionMinion.goalManager = goalManager;
                break;
            case minion.MinionTypes.MT_Seige:
                newMinion = Instantiate(seigeMinionPrefab, blueSpawn.position, blueSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = true;
                newMinionMinion.attackPlayer = false;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = blueTarget;
                newMinionMinion.goalManager = goalManager;
                newMinion.tag = "Ally";
                newMinionMinion.gameObject.tag = "Ally";

                newMinion = Instantiate(seigeMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = false;
                newMinionMinion.attackPlayer = true;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = redTarget;
                newMinionMinion.goalManager = goalManager;
                break;
            default:
				break;
		}
	}
}
