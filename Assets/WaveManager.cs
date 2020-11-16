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

        switch (minionType)
		{
			case minion.MinionTypes.MT_Melee:
                newMinion = Instantiate(meleeMinionPrefab, blueSpawn.position, blueSpawn.rotation, null);
                newMinion.GetComponentInChildren<minion>().blueTeam = true;
                newMinion.GetComponentInChildren<minion>().attackPlayer = false;
                newMinion = Instantiate(meleeMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinion.GetComponentInChildren<minion>().blueTeam = false;
                newMinion.GetComponentInChildren<minion>().attackPlayer = true;
                break;
			case minion.MinionTypes.MT_Ranged:
                newMinion = Instantiate(magicMinionPrefab, blueSpawn.position, blueSpawn.rotation, null);
                newMinion.GetComponentInChildren<minion>().blueTeam = true;
                newMinion.GetComponentInChildren<minion>().attackPlayer = false;
                newMinion = Instantiate(magicMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinion.GetComponentInChildren<minion>().blueTeam = false;
                newMinion.GetComponentInChildren<minion>().attackPlayer = true;
                break;
            case minion.MinionTypes.MT_Seige:
                newMinion = Instantiate(seigeMinionPrefab, blueSpawn.position, blueSpawn.rotation, null);
                newMinion.GetComponentInChildren<minion>().blueTeam = true;
                newMinion.GetComponentInChildren<minion>().attackPlayer = false;
                newMinion = Instantiate(seigeMinionPrefab, redSpawn.position, redSpawn.rotation, null);
                newMinion.GetComponentInChildren<minion>().blueTeam = false;
                newMinion.GetComponentInChildren<minion>().attackPlayer = true;
                break;
            default:
				break;
		}
	}
}
