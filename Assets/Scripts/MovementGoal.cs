using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGoal : MonoBehaviour
{
	public TutorialManager tutorialManager;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			tutorialManager.CompleteCurrentGoal();
		}
	}
}
