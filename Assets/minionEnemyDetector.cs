using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minionEnemyDetector : MonoBehaviour
{
    public minion parent;
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("HI");
		parent.TryAddElligibleEnemy(other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
		parent.TryRemoveElligibleEnemy(other.gameObject);
	}
}
