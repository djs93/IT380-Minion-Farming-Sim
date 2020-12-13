using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject goalUI;
    public GameObject controlsUI;
    public GameObject infoPanel;
    public GameObject reminderPanel;
	public TextMeshProUGUI infoTitleText;
	public TextMeshProUGUI infoDescText;
	public TextMeshProUGUI reminderDescText;
    public player playerComponent;
    public TutorialState state = TutorialState.TS_0_0_INIT;

	public Dictionary<TutorialState, string> stateDescStrings = new Dictionary<TutorialState, string>();
	public Dictionary<TutorialState, string> stateTitleStrings = new Dictionary<TutorialState, string>();
	public Dictionary<TutorialState, string> stateReminderStrings = new Dictionary<TutorialState, string>();

	public GameObject goalCircleGreen;
	public GameObject goalCircleYellow;
    
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
        TS_3_END,
        TS_4_1_CHAMP_UI,
        TS_4_2_CHAMP_PRESETS,
        TS_4_3_MINION_UI,
        TS_4_4_COLLAPSE,
        TS_4_END,
        TS_5_1_GOAL_COUNTER,
        TS_5_END,
        TS_6_1_WHY_CS,
        TS_6_END
    }

    // Start is called before the first frame update
    void Start()
    {
        goalUI.SetActive(false);
        controlsUI.SetActive(false);
        infoPanel.SetActive(false);
        reminderPanel.SetActive(false);
        goalCircleGreen.SetActive(false);
        goalCircleYellow.SetActive(false);
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
		stateReminderStrings.Add(TutorialState.TS_1_2_MOVE_YELLOW_IN_GAME, "Move to the green circle!");

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
		stateDescStrings.Add(TutorialState.TS_3_1_INTRO, "On the minion health bars you will now see a white line. This white line shows you when you can kill the minion with a single attack. It will turn green once the minion’s current health is at or below this threshold, attack it then!");
		stateReminderStrings.Add(TutorialState.TS_3_2_LAST_HIT_IN_GAME, "Wait until the enemy minion’s health is low enough to kill, then land the final blow!");

		stateTitleStrings.Add(TutorialState.TS_3_2_1_FAIL, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_2_1_FAIL, "Aw, so close! Let’s try that again!");
		stateReminderStrings.Add(TutorialState.TS_3_2_1_FAIL, "Wait until the enemy minion’s health is low enough to kill, then land the final blow!");

		stateTitleStrings.Add(TutorialState.TS_3_3_MULTI_LAST_HIT, "Last Hitting");
		stateDescStrings.Add(TutorialState.TS_3_3_MULTI_LAST_HIT, "Excellent! Now let’s try something more complicated! Try to last hit 2 out of the group of 5! Good luck!");
		stateReminderStrings.Add(TutorialState.TS_3_3_MULTI_LAST_HIT_IN_GAME, "Hit 2 out of the group of 5!");

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
		stateDescStrings.Add(TutorialState.TS_5_END, "Click Next to learn why killing minions is so important in League of Legends.");

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
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				goalCircleGreen.SetActive(true);
				break;
			case TutorialState.TS_1_2_MOVE_YELLOW:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalCircleGreen.SetActive(false);
				break;
			case TutorialState.TS_1_2_MOVE_YELLOW_IN_GAME:
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				goalCircleYellow.SetActive(true);
				break;
			case TutorialState.TS_1_END:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				goalCircleYellow.SetActive(false);
				break;
			case TutorialState.TS_2_1_RIGHT_CLICK:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_2_1_RIGHT_CLICK_IN_GAME:
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				break;
			case TutorialState.TS_2_2_MOVE_TO_ATK:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_2_2_MOVE_TO_ATK_IN_GAME:
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				break;
			case TutorialState.TS_2_3_ORB_WALKING:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_2_3_ORB_WALKING_IN_GAME:
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				break;
			case TutorialState.TS_2_END:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_1_INTRO:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_2_LAST_HIT:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_2_LAST_HIT_IN_GAME:
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				break;
			case TutorialState.TS_3_2_1_FAIL:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_3_MULTI_LAST_HIT:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_3_3_MULTI_LAST_HIT_IN_GAME:
				infoPanel.SetActive(false);
				reminderPanel.SetActive(true);
				reminderDescText.text = stateReminderStrings[state];
				break;
			case TutorialState.TS_3_END:
				infoPanel.SetActive(true);
				reminderPanel.SetActive(false);
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_4_1_CHAMP_UI:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_4_2_CHAMP_PRESETS:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_4_3_MINION_UI:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_4_4_COLLAPSE:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_4_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_5_1_GOAL_COUNTER:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_5_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_6_1_WHY_CS:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			case TutorialState.TS_6_END:
				infoTitleText.text = stateTitleStrings[state];
				infoDescText.text = stateDescStrings[state];
				break;
			default:
				Debug.LogWarning("Invalid State!");
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
			default:
				Debug.LogWarning("Tried to complete a goal for a section that has none!");
				break;
		}
	}
}
