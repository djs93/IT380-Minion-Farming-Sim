using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialManager : MonoBehaviour
{
    public GameObject goalUI;
	public GameObject goalMasker;
    public GameObject controlsUI;
    public GameObject championPanel;
    public GameObject presetButton1;
    public GameObject presetButton2;
    public GameObject presetButton3;
    public GameObject minionPanel;
    public GameObject collapseButtonPanel;
    public GameObject infoMaskerPanel;
    public GameObject uiMaskerPanel;
    public GameObject champMaskerPanel;
    public GameObject infoPanel;
    public GameObject meleeMinionPicturePanel;
    public GameObject magicMinionPicturePanel;
    public GameObject seigeMinionPicturePanel;
    public GameObject reminderPanel;
	public TextMeshProUGUI infoTitleText;
	public TextMeshProUGUI infoDescText;
	public TextMeshProUGUI reminderDescText;
	public GameObject infoPanelNextButton;
	public GameObject infoPanelHomeButton;
	public GameObject infoPanelLevel01Button;
    public player playerComponent;
    public TutorialState state = TutorialState.TS_0_0_INIT;
	public GoalManager goalManager;
	public WaveManager waveManager;

	public Dictionary<TutorialState, string> stateDescStrings = new Dictionary<TutorialState, string>();
	public Dictionary<TutorialState, string> stateTitleStrings = new Dictionary<TutorialState, string>();
	public Dictionary<TutorialState, string> stateReminderStrings = new Dictionary<TutorialState, string>();

	public GameObject goalCircleGreen;
	public GameObject goalCircleYellow;
	public GameObject tutorialMinion;

	private int currTotalDeadMinions = 0;
    
    public enum TutorialState
	{
		TS_0_0_INIT,
        TS_0_1_GREETING,
        TS_0_END,
        TS_1_1_MOVE_GREEN,
        TS_1_1_MOVE_GREEN_IN_GAME,
        TS_1_2_MOVE_YELLOW,
        TS_1_2_MOVE_YELLOW_IN_GAME,
        TS_1_END,
        TS_2_1_RIGHT_CLICK,
        TS_2_1_RIGHT_CLICK_IN_GAME,
        TS_2_2_MOVE_TO_ATK,
        TS_2_2_MOVE_TO_ATK_IN_GAME,
        TS_2_3_ORB_WALKING,
        TS_2_3_ORB_WALKING_IN_GAME,
        TS_2_END,
        TS_3_1_INTRO,
        TS_3_2_LAST_HIT,
        TS_3_2_LAST_HIT_IN_GAME,
        TS_3_2_1_FAIL,
        TS_3_3_MULTI_LAST_HIT,
        TS_3_3_MULTI_LAST_HIT_IN_GAME,
		TS_3_3_1_FAIL,
		TS_3_END,
        TS_4_1_CHAMP_UI,
        TS_4_2_CHAMP_PRESETS,
        TS_4_3_MINION_UI,
        TS_4_4_COLLAPSE,
        TS_4_END,
        TS_5_1_GOAL_COUNTER,
        TS_5_END,
		TS_6_0_1_TYPES,
		TS_6_0_2_MELEE,
		TS_6_0_3_MAGIC,
		TS_6_0_4_SEIGE,
		TS_6_1_WHY_CS,
        TS_6_END
    }

    // Start is called before the first frame update
    void Start()
    {
        goalUI.SetActive(false);
        controlsUI.SetActive(false);
        championPanel.SetActive(false);
		presetButton1.SetActive(false);
		presetButton2.SetActive(false);
		presetButton3.SetActive(false);
        minionPanel.SetActive(false);
        collapseButtonPanel.SetActive(false);
        infoPanel.SetActive(false);
        tutorialMinion.SetActive(false);
        reminderPanel.SetActive(false);
        goalCircleGreen.SetActive(false);
        goalCircleYellow.SetActive(false);
		infoPanelHomeButton.SetActive(false);
		infoPanelLevel01Button.SetActive(false);
		infoMaskerPanel.SetActive(false);
		uiMaskerPanel.SetActive(false);
		champMaskerPanel.SetActive(false);
		magicMinionPicturePanel.SetActive(false);
		seigeMinionPicturePanel.SetActive(false);
		meleeMinionPicturePanel.SetActive(false);
		SetupStrings();
		AdvanceState();
    }

	private void SetupStrings()
	{
		stateTitleStrings.Add(TutorialState.TS_0_1_GREETING, "Welcome!");
		stateDescStrings.Add(TutorialState.TS_0_1_GREETING, "This tutorial will guide you through how to use this simulator starting at the basics!");
		stateTitleStrings.Add(TutorialState.TS_0_END, "Welcome!");
		stateDescStrings.Add(TutorialState.TS_0_END, "Click Next to learn about movement!");

		stateTitleStrings.Add(TutorialState.TS_1_1_MOVE_GREEN, "Movement");
		stateDescStrings.Add(TutorialState.TS_1_1_MOVE_GREEN, "Use RIGHT CLICK to move your character to the green circle!");
		stateReminderStrings.Add(TutorialState.TS_1_1_MOVE_GREEN_IN_GAME, "Move to the green circle!");

		stateTitleStrings.Add(TutorialState.TS_1_2_MOVE_YELLOW, "Movement");
		stateDescStrings.Add(TutorialState.TS_1_2_MOVE_YELLOW, "Great! Now use RIGHT CLICK to move your character to the yellow circle!");
		stateReminderStrings.Add(TutorialState.TS_1_2_MOVE_YELLOW_IN_GAME, "Move to the yellow circle!");

		stateTitleStrings.Add(TutorialState.TS_1_END, "Movement");
		stateDescStrings.Add(TutorialState.TS_1_END, "Excellent! Click Next to learn about attacking!");

		stateTitleStrings.Add(TutorialState.TS_2_1_RIGHT_CLICK, "Attacking");
		stateDescStrings.Add(TutorialState.TS_2_1_RIGHT_CLICK, "Right click on an enemy to attack them!");
		stateReminderStrings.Add(TutorialState.TS_2_1_RIGHT_CLICK_IN_GAME, "Right click on an enemy to attack them!");

		stateTitleStrings.Add(TutorialState.TS_2_2_MOVE_TO_ATK, "Attacking");
		stateDescStrings.Add(TutorialState.TS_2_2_MOVE_TO_ATK, "Excellent! If an enemy is outside of your range, you can right click them to move into range then attack. Try it now!");
		stateReminderStrings.Add(TutorialState.TS_2_2_MOVE_TO_ATK_IN_GAME, "Right click the enemy to move into range then attack!");

		stateTitleStrings.Add(TutorialState.TS_2_3_ORB_WALKING, "Attacking");
		stateDescStrings.Add(TutorialState.TS_2_3_ORB_WALKING, "Great! Now try weaving your attacks in between your movements. This is called “Orb Walking”");
		stateReminderStrings.Add(TutorialState.TS_2_3_ORB_WALKING_IN_GAME, "Try weaving your attacks in between your movements!");

		stateTitleStrings.Add(TutorialState.TS_2_END, "Attacking");
		stateDescStrings.Add(TutorialState.TS_2_END, "Fantastic! Now click Next to learn about last hitting minions.");

		stateTitleStrings.Add(TutorialState.TS_3_1_INTRO, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_1_INTRO, "Notice the white line on the minion health bars. <br>This white line shows you when you can kill the minion with a single attack. <br>It will turn green once the minion’s current health is at or below this threshold, attack it then!");
		stateReminderStrings.Add(TutorialState.TS_3_2_LAST_HIT_IN_GAME, "Wait until the enemy minion’s health is low enough to kill, then land the final blow!");

		stateTitleStrings.Add(TutorialState.TS_3_2_1_FAIL, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_2_1_FAIL, "Aw, so close! Let’s try that again!");
		stateReminderStrings.Add(TutorialState.TS_3_2_1_FAIL, "Wait until the enemy minion’s health is low enough to kill, then land the final blow!");

		stateTitleStrings.Add(TutorialState.TS_3_2_LAST_HIT, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_2_LAST_HIT, "Wait until the enemy minion’s health is low enough to kill, then land the final blow! <br>You may also damage the minion to speed things up a bit, just make sure you are ready to last hit it!");

		stateTitleStrings.Add(TutorialState.TS_3_3_MULTI_LAST_HIT, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_3_MULTI_LAST_HIT, "Excellent! Now let’s try something more complicated! Try to last hit 2 out of the group of 6! Good luck!");
		stateReminderStrings.Add(TutorialState.TS_3_3_MULTI_LAST_HIT_IN_GAME, "Last hit 2 out of the group of 6!");

		stateTitleStrings.Add(TutorialState.TS_3_3_1_FAIL, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_3_1_FAIL, "Aw, so close! Let’s try that again!");

		stateTitleStrings.Add(TutorialState.TS_3_END, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_END, "Great! Click Next to learn about the simulation UI!");

		stateTitleStrings.Add(TutorialState.TS_4_1_CHAMP_UI, "Controls");
		stateDescStrings.Add(TutorialState.TS_4_1_CHAMP_UI, "These are the champion controls! Here, you can change your damage, range, attack speed, move speed, if you shoot projectiles, and if you can see your current attack range.");

		stateTitleStrings.Add(TutorialState.TS_4_2_CHAMP_PRESETS, "Controls");
		stateDescStrings.Add(TutorialState.TS_4_2_CHAMP_PRESETS, "These buttons are for different champion archetype presets. In order top-to-bottom, they are marksman, mage, and fighter. They change every aspect of your champion.");

		stateTitleStrings.Add(TutorialState.TS_4_3_MINION_UI, "Controls");
		stateDescStrings.Add(TutorialState.TS_4_3_MINION_UI, "These are the minion controls! Here, you can change how much ADDITIONAL damage, armor, attack speed, movement speed, and health a minion has. You can also toggle the execution threshold bar here as well.");

		stateTitleStrings.Add(TutorialState.TS_4_4_COLLAPSE, "Controls");
		stateDescStrings.Add(TutorialState.TS_4_4_COLLAPSE, "Finally, here is the collapse button. This lets you show and hide the controls.");

		stateTitleStrings.Add(TutorialState.TS_4_END, "Controls");
		stateDescStrings.Add(TutorialState.TS_4_END, "Click Next to learn about Goals.");

		stateTitleStrings.Add(TutorialState.TS_5_1_GOAL_COUNTER, "Goals");
		stateDescStrings.Add(TutorialState.TS_5_1_GOAL_COUNTER, "As you’ve already seen, at the top-right there is a goal counter. This counter can track gold earned, minions killed, or any other goal in the simulator.");

		stateTitleStrings.Add(TutorialState.TS_5_END, "Goals");
		stateDescStrings.Add(TutorialState.TS_5_END, "Click Next to learn about minion types in League of Legends.");

		stateTitleStrings.Add(TutorialState.TS_6_0_1_TYPES, "Minion Types");
		stateDescStrings.Add(TutorialState.TS_6_0_1_TYPES, "In League of Legends, there are three types of minions.");

		stateTitleStrings.Add(TutorialState.TS_6_0_2_MELEE, "Minion Types");
		stateDescStrings.Add(TutorialState.TS_6_0_2_MELEE, "Melee minions are close-ranged but have more health.");

		stateTitleStrings.Add(TutorialState.TS_6_0_3_MAGIC, "Minion Types");
		stateDescStrings.Add(TutorialState.TS_6_0_3_MAGIC, "Magic minions are long-ranged and deal more damage, but have less health.");

		stateTitleStrings.Add(TutorialState.TS_6_0_4_SEIGE, "Minion Types");
		stateDescStrings.Add(TutorialState.TS_6_0_4_SEIGE, "Seige minions are super strong ranged minions. <br>They have more health and deal more damage than either other type of minion. <br>There are also super minions, which are even stronger melee minions, however, they are not in this simulator.");

		stateTitleStrings.Add(TutorialState.TS_6_1_WHY_CS, "Why Last Hit?");
		stateDescStrings.Add(TutorialState.TS_6_1_WHY_CS, "In League of Legends, you have items you can buy with gold. As you kill minions, you gain gold. <br>The more accurate you are with last-hitting minions, the more gold you will have. The more gold you have, the more items you can buy, and the more items you can buy, the more powerful you will be against your opponents. <br>This is why last-hitting minions (also called “CS-ing”) is so important, and why this simulator exists.");

		stateTitleStrings.Add(TutorialState.TS_6_END, "Thank You!");
		stateDescStrings.Add(TutorialState.TS_6_END, "Thank you for playing through the tutorial, click Home to go back to the main menu, or Level 1 to go to level 1!");
	}

    public void AdvanceState()
	{
		state += 1;
		Debug.Log("State now" + state.ToString());
		switch (state)
		{
			case TutorialState.TS_0_1_GREETING:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_0_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_1_1_MOVE_GREEN:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_1_1_MOVE_GREEN_IN_GAME:
				infoPanel.SetActive(false);
				uiMaskerPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				goalCircleGreen.SetActive(true);
				playerComponent.SetCanMove(true);
				break;
			case TutorialState.TS_1_2_MOVE_YELLOW:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalCircleGreen.SetActive(false);
				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_1_2_MOVE_YELLOW_IN_GAME:
				infoPanel.SetActive(false);
				uiMaskerPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				goalCircleYellow.SetActive(true);
				playerComponent.SetCanMove(true);
				break;
			case TutorialState.TS_1_END:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalCircleYellow.SetActive(false);
				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_2_1_RIGHT_CLICK:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_2_1_RIGHT_CLICK_IN_GAME:
				infoPanel.SetActive(false);
				uiMaskerPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				tutorialMinion.SetActive(true);
				break;
			case TutorialState.TS_2_2_MOVE_TO_ATK:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				tutorialMinion.SetActive(false);
				break;
			case TutorialState.TS_2_2_MOVE_TO_ATK_IN_GAME:
				tutorialMinion.SetActive(true);
				infoPanel.SetActive(false);
				uiMaskerPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				playerComponent.SetCanMove(true);
				tutorialMinion.SetActive(true);
				tutorialMinion.transform.Translate(new Vector3(5,0,20));
				break;
			case TutorialState.TS_2_3_ORB_WALKING:
				/**
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				**/
				AdvanceState();
				break;
			case TutorialState.TS_2_3_ORB_WALKING_IN_GAME:
				/**
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				**/
				AdvanceState();
				break;
			case TutorialState.TS_2_END:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				tutorialMinion.SetActive(false);
				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_3_1_INTRO:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_2_LAST_HIT:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_2_LAST_HIT_IN_GAME:
				infoPanel.SetActive(false);
				uiMaskerPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				playerComponent.SetCanMove(true);

				goalManager.goalType = GoalManager.GoalType.GT_Kills;
				goalManager.intGoalToggle = true;
				goalManager.intGoal = 1;
				goalManager.intGoalProgress = 0;
				currTotalDeadMinions = 0;
				goalManager.goalSuffix = "minion";
				goalManager.InitGoalWindow();
				goalUI.SetActive(true);

				waveManager.SpawnSingleWave(true);
				break;
			case TutorialState.TS_3_2_1_FAIL:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				state = TutorialState.TS_3_2_LAST_HIT;
				waveManager.DestroyAllMinions();

				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_3_3_MULTI_LAST_HIT:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				waveManager.DestroyAllMinions();
				goalUI.SetActive(false);
				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_3_3_MULTI_LAST_HIT_IN_GAME:
				infoPanel.SetActive(false);
				uiMaskerPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];

				goalManager.intGoal = 2;
				goalManager.intGoalProgress = 0;
				goalManager.goalSuffix = "minions";
				goalManager.InitGoalWindow();
				currTotalDeadMinions = 0;

				waveManager.SpawnSingleWave(false);
				goalUI.SetActive(true);
				playerComponent.SetCanMove(true);
				break;
			case TutorialState.TS_3_3_1_FAIL:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				state = TutorialState.TS_3_3_MULTI_LAST_HIT;
				waveManager.DestroyAllMinions();
				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_3_END:
				infoPanel.SetActive(true);
				uiMaskerPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				waveManager.DestroyAllMinions();
				goalUI.SetActive(false);
				playerComponent.SetCanMove(false);
				break;
			case TutorialState.TS_4_1_CHAMP_UI:
				uiMaskerPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				controlsUI.SetActive(true);
				championPanel.SetActive(true);
				infoMaskerPanel.SetActive(true);
				championPanel.transform.SetAsLastSibling();
				break;
			case TutorialState.TS_4_2_CHAMP_PRESETS:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				presetButton1.SetActive(true);
				presetButton2.SetActive(true);
				presetButton3.SetActive(true);
				champMaskerPanel.SetActive(true);
				break;
			case TutorialState.TS_4_3_MINION_UI:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				minionPanel.SetActive(true);
				champMaskerPanel.SetActive(false);
				championPanel.transform.SetSiblingIndex(championPanel.transform.GetSiblingIndex() - 1);
				minionPanel.transform.SetAsLastSibling();
				break;
			case TutorialState.TS_4_4_COLLAPSE:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				collapseButtonPanel.SetActive(true);
				minionPanel.transform.SetSiblingIndex(minionPanel.transform.GetSiblingIndex() - 1);
				collapseButtonPanel.transform.SetAsLastSibling();
				break;
			case TutorialState.TS_4_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				infoMaskerPanel.transform.SetAsFirstSibling();
				break;
			case TutorialState.TS_5_1_GOAL_COUNTER:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalUI.SetActive(true);
				infoMaskerPanel.transform.SetAsLastSibling();
				break;
			case TutorialState.TS_5_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_6_0_1_TYPES:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalMasker.SetActive(true);
				uiMaskerPanel.SetActive(true);
				infoMaskerPanel.SetActive(false);
				break;
			case TutorialState.TS_6_0_2_MELEE:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				meleeMinionPicturePanel.SetActive(true);
				break;
			case TutorialState.TS_6_0_3_MAGIC:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				meleeMinionPicturePanel.SetActive(false);
				magicMinionPicturePanel.SetActive(true);
				break;
			case TutorialState.TS_6_0_4_SEIGE:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				magicMinionPicturePanel.SetActive(false);
				seigeMinionPicturePanel.SetActive(true);
				break;
			case TutorialState.TS_6_1_WHY_CS:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalUI.SetActive(false);
				controlsUI.SetActive(false);
				seigeMinionPicturePanel.SetActive(false);
				break;
			case TutorialState.TS_6_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				infoPanelNextButton.SetActive(false);
				infoPanelHomeButton.SetActive(true);
				infoPanelLevel01Button.SetActive(true);
				break;
			default:
				Debug.LogWarning("Invalid State!");
				break;
		}
	}

	internal void AddMinionDeath()
	{
		currTotalDeadMinions += 1;
		Debug.Log("Current Dead Minions: " + currTotalDeadMinions.ToString());
		switch (state)
		{
			case TutorialState.TS_3_2_LAST_HIT_IN_GAME:
				if (currTotalDeadMinions == 1 && goalManager.intGoalProgress == 0)
				{
					AdvanceState();
				}					
				break;
			case TutorialState.TS_3_3_MULTI_LAST_HIT_IN_GAME:
				if (currTotalDeadMinions == 6 && goalManager.intGoalProgress <= 1)
				{
					AdvanceState();
				}
				break;
		}
	}

	public void CompleteCurrentGoal()
	{
		switch (state)
		{
			case TutorialState.TS_1_1_MOVE_GREEN_IN_GAME:
				AdvanceState();
				break;
			case TutorialState.TS_1_2_MOVE_YELLOW_IN_GAME:
				AdvanceState();
				break;
			case TutorialState.TS_2_1_RIGHT_CLICK_IN_GAME:
				playerComponent.ClearAttackTarget();
				AdvanceState();
				break;
			case TutorialState.TS_2_2_MOVE_TO_ATK_IN_GAME:
				playerComponent.ClearAttackTarget();
				AdvanceState();
				break;
			case TutorialState.TS_3_2_LAST_HIT_IN_GAME:
				state = TutorialState.TS_3_2_1_FAIL;
				AdvanceState();
				break;
			case TutorialState.TS_3_3_MULTI_LAST_HIT_IN_GAME:
				state = TutorialState.TS_3_3_1_FAIL;
				AdvanceState();
				break;
			default:
				Debug.LogWarning("Tried to complete a goal for a section that has none!");
				break;
		}
	}
}
