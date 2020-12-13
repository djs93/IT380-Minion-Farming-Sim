using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
	public enum GoalType
	{
		GT_Gold,
		GT_Kills,
		GT_Percent_Rounds,
		GT_Percent_Gold,
		GT_Rounds
	};
	public GoalType goalType;
	public bool intGoalToggle; //false for float goal
	public int intGoal = 0;
	public int intGoalProgress = 0;
	public float floatGoal = 0.0f;
	public float floatGoalProgress = 0.0f;
	public string goalSuffix;
	public TextMeshProUGUI goalWindowText;

	public GameObject congratsCanvas;
	public TextMeshProUGUI congratsWindowText;

	public TutorialManager tutorialManager;

	public void Start()
	{
		InitGoalWindow();
	}
	// Start is called before the first frame update
	public void AddKill()
	{
		if (goalType == GoalType.GT_Kills)
		{
			intGoalProgress++;
			goalWindowText.text = "<b>Goal:</b>" + intGoalProgress +"/"+ intGoal + " " + goalSuffix;
		}
		if ((intGoalToggle && intGoalProgress >= intGoal) || (!intGoalToggle &&floatGoalProgress >= floatGoal))
		{
			popCongrats();
		}
	}

	public void InitGoalWindow()
	{
		if (goalType == GoalType.GT_Kills)
		{
			goalWindowText.text = "<b>Goal:</b>" + intGoalProgress + "/" + intGoal + " " + goalSuffix;
		}
	}

	public void AddGold()
	{
		Debug.LogWarning("AddGold goal not yet implemented");
	}
	
	public void popCongrats()
	{
		if (tutorialManager)
		{
			tutorialManager.CompleteCurrentGoal();
		}
		else
		{
			congratsCanvas.SetActive(true);
			congratsWindowText.text = congratsWindowText.text.Replace("{goalAmount}", intGoal.ToString());
			congratsWindowText.text = congratsWindowText.text.Replace("{goalSuffix}", goalSuffix);
		}
	}
}
