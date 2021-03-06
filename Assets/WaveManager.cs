﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Transform blueSpawn;
    public Transform redSpawn;
    private float timeUntilNextSpawn;
    private float timeUntilNextSpawnSingle;
    public float timeInBetweenWaveMinions;
    public float timeInBetweenWaves;
    public List<minion.MinionTypes> waveComposition;
    public List<minion.MinionTypes> altWaveComposition;
    private List<GameObject> spawnedMinions;
    private int currentSpawnPos = 0;
    private int currentSpawnPosSingle = 0;
    private int waveCounter = 1;
    private bool spawningSingleWave;
    private bool isSingleAltWave;
    public player userPlayer;
    public GameObject blueTarget;
    public GameObject redTarget;
    public GoalManager goalManager;

    public GameObject meleeMinionPrefab;
    public GameObject magicMinionPrefab;
    public GameObject seigeMinionPrefab;

    public MinionPanel minionPanel;
    public bool executeBarsEnabledOnSpawn = true;

    public bool spawningEnabled = true;

    public TutorialManager tutorialManager;

	void Start()
	{
        spawnedMinions = new List<GameObject>();
    }
	public void ToggleExecutionBarsOnSpawn()
	{
        executeBarsEnabledOnSpawn = !executeBarsEnabledOnSpawn;
	}

    // Update is called once per frame
    void Update()
    {
		if (spawningSingleWave)
		{
            if (timeUntilNextSpawnSingle <= 0)
            {
                if (isSingleAltWave)
                {
                    SpawnMinionMirrored(altWaveComposition[currentSpawnPosSingle]);
                }
                else
                {
                    SpawnMinionMirrored(waveComposition[currentSpawnPosSingle]);
                }
                //spawn next minion in sequence
                currentSpawnPosSingle++;
                if (currentSpawnPosSingle == (isSingleAltWave ? altWaveComposition.Count : waveComposition.Count))
                {
                    currentSpawnPosSingle = 0;
                    timeUntilNextSpawnSingle = 0;
                    spawningSingleWave = false;
                }
                else
                {
                    timeUntilNextSpawnSingle = timeInBetweenWaveMinions;
                }
            }
            else
            {
                timeUntilNextSpawnSingle -= Time.deltaTime;
            }
        }
		if (!spawningEnabled)
		{
            return;
		}
		if (timeUntilNextSpawn <= 0)
		{
            if (waveCounter % 3 == 0)
            {
                SpawnMinionMirrored(altWaveComposition[currentSpawnPos]);
            }
            else
            {
                SpawnMinionMirrored(waveComposition[currentSpawnPos]);
            }
            //spawn next minion in sequence
            currentSpawnPos++;
			if (currentSpawnPos == (waveCounter % 3 == 0 ? altWaveComposition.Count : waveComposition.Count))
			{
                currentSpawnPos = 0;
                timeUntilNextSpawn = timeInBetweenWaves;
                waveCounter++;
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

	public void SpawnMinionMirrored(minion.MinionTypes minionType)
	{
        SpawnMinion(true, false, blueTarget, true, blueSpawn, minionType);
        SpawnMinion(false, true, redTarget, false, redSpawn, minionType);
        /**
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
                newMinionMinion.damage += minionPanel.damageMod;
                newMinionMinion.armor += minionPanel.armorMod;
                newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
                newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
                newMinionMinion.health += minionPanel.healthMod;

                newMinion = Instantiate(meleeMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = false;
                newMinionMinion.attackPlayer = true;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = redTarget;
                newMinionMinion.goalManager = goalManager;
                newMinionMinion.damage += minionPanel.damageMod;
                newMinionMinion.armor += minionPanel.armorMod;
                newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
                newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
                newMinionMinion.health += minionPanel.healthMod;
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
                newMinionMinion.damage += minionPanel.damageMod;
                newMinionMinion.armor += minionPanel.armorMod;
                newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
                newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
                newMinionMinion.health += minionPanel.healthMod;

                newMinion = Instantiate(magicMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = false;
                newMinionMinion.attackPlayer = true;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = redTarget;
                newMinionMinion.goalManager = goalManager;
                newMinionMinion.damage += minionPanel.damageMod;
                newMinionMinion.armor += minionPanel.armorMod;
                newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
                newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
                newMinionMinion.health += minionPanel.healthMod;
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
                newMinionMinion.damage += minionPanel.damageMod;
                newMinionMinion.armor += minionPanel.armorMod;
                newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
                newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
                newMinionMinion.health += minionPanel.healthMod;

                newMinion = Instantiate(seigeMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinionMinion = newMinion.GetComponentInChildren<minion>();
                newMinionMinion.blueTeam = false;
                newMinionMinion.attackPlayer = true;
                newMinionMinion.userPlayer = userPlayer;
                newMinionMinion.goalTarget = redTarget;
                newMinionMinion.goalManager = goalManager;
                newMinionMinion.damage += minionPanel.damageMod;
                newMinionMinion.armor += minionPanel.armorMod;
                newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
                newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
                newMinionMinion.health += minionPanel.healthMod;
                break;
            default:
				break;
		}
        **/
	}

    public void SpawnMinion(bool isBlueTeam, bool attackPlayer, GameObject goalTarget, bool isAlly, Transform spawnTransform, minion.MinionTypes minionType)
	{
        GameObject newMinion;
        minion newMinionMinion;
        GameObject spawnPrefab;

		switch (minionType)
		{
			case minion.MinionTypes.MT_Melee:
                spawnPrefab = meleeMinionPrefab;
                break;
			case minion.MinionTypes.MT_Ranged:
                spawnPrefab = magicMinionPrefab;
                break;
			case minion.MinionTypes.MT_Seige:
                spawnPrefab = seigeMinionPrefab;
                break;
			default:
                Debug.LogWarning("Tried to spawn an invalid minion type!");
                return;
		}

		newMinion = Instantiate(spawnPrefab, spawnTransform.position, spawnTransform.rotation, null);
        newMinionMinion = newMinion.GetComponentInChildren<minion>();
        newMinionMinion.blueTeam = isBlueTeam;
        newMinionMinion.attackPlayer = attackPlayer;
        newMinionMinion.userPlayer = userPlayer;
        newMinionMinion.goalTarget = goalTarget;
        newMinionMinion.goalManager = goalManager;
		newMinion.tag = isAlly ? "Ally" : "Enemy";
		newMinionMinion.gameObject.tag = isAlly ? "Ally" : "Enemy";
		newMinionMinion.damage += minionPanel.damageMod;
        newMinionMinion.armor += minionPanel.armorMod;
        newMinionMinion.attackSpeed += minionPanel.atkSpeedMod;
        newMinionMinion.SetMoveSpeed(newMinionMinion.moveSpeed + minionPanel.movespeedMod);
        newMinionMinion.health += minionPanel.healthMod;

        if (isBlueTeam)
		{
            newMinionMinion.executeThreshold.gameObject.SetActive(false);
        }
        else
        {
            if (newMinionMinion.executeThreshold)
            {
                newMinionMinion.executeThreshold.gameObject.SetActive(executeBarsEnabledOnSpawn);
            }

			if (tutorialManager)
			{
                newMinionMinion.tutorialManager = tutorialManager;
			}
        }

        spawnedMinions.Add(newMinion);
    }

    public void SpawnSingleWave(bool isAltWave)
	{
        spawningSingleWave = true;
        isSingleAltWave = isAltWave;
    }

    public void DestroyAllMinions()
	{
		foreach (GameObject gameObject in spawnedMinions)
		{
            Destroy(gameObject);
		}
	}
}
