using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
	[Header("Title Scene Properties", order = 0)]
	[Space(2f)]
	[Header("Intro Scene Properties", order = 1)]
	public GameObject introImage;
	public GameObject personaImage;
	public GameObject backButton;
	public GameObject nextButton;
	public Color backgroundColor;
	public TMP_Text introText;
	public TMP_Text titleText;
	public int introSeqCount;
	[Header("Tutorial Scene Properties", order = 2)]
	[Space(2f)]
	[Header("Level01 Scene Properties", order = 3)]
	[Space(2f)]


	Scene currentScene;
	public void SetScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void Awake()
	{
		currentScene = SceneManager.GetActiveScene();
		switch (currentScene.name)
		{
			case "00TitleScene":
				break;
			case "01IntroScene":
				introSeqCount--;
				IntroSequenceNext();
				break;
			case "02TutorialScene":
				break;
			case "03Level01Scene":
				break;
		}
	}

	public void IntroSequenceNext()
	{
		introSeqCount++;
		switch (introSeqCount)
		{
			case 0:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Center;
				titleText.text = "Welcome to Minion Farming Simulator 2020!";
				introText.text = "Here, you will learn how to clear minion waves like a pro!";
				introImage.SetActive(true);
				backButton.SetActive(false);
				break;
			case 1:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Overview";
				introText.text = "<b>Title:</b>​ Minion Farming Simulator 2020\n" +
				"<b>Platform:</b>​ WebGL build\n" +
				"<b>Subject:</b>​​ Technology\n" +
				"<b>Sub Subject:</b>​​ Video Games -League of Legends\n" +
				"<b>Main Focus:</b>​​ Minion farming(CSing)\n" +
				"<b>Learning Level:</b>​ Young Adults(Age 16 - 25)";
				introImage.SetActive(false);
				backButton.SetActive(true);
				break;
			case 2:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Proposed EdTech Solution";
				introText.text = "League of Legends is a very complex game with many different systems all working together to form one unique gameplay experience. One of the most important systems in the game is killing “minions” to give you gold to buy items. However, for new players this is difficult and its importance is not reinforced very well by the tutorial. This software aims to solve the CS (creep score) problem by giving players a simulated League of Legends experience focused solely on “farming” minions. There will be optional visual aids not present in League of Legends in order to help newer players get used to “farming” (the killing of minions for gold) faster.";
				break;
			case 3:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Competing Software Review";
				introText.text = "<b>Nasus Q Farming Simulator</b> - This is a very simple game in which the user times their Q ability to hit as soon as a minion is in the correct range. You gain “stacks” off of the Q ability and have to survive 5 minutes to win.\n\n" +
				"<b>CS Practice Tool</b> - This is a slightly more complex but still overall simple simulator for killing minions. You can move, and right click to fire a projectile to kill a minion.There are also options to increase your attack speed and damage as well as allowing minions to damage you or not. It also includes a turret that fires at minions, killing them if you don’t and displaying how much gold you’ve earned and lost.\n\n" +
				"<b>Last Hit -League of Legends</b> - This is an app for Android and Apple devices that uses League of Legends graphics in order to let you practice farming minions on your phone.It features 119 different champions to use as well as an item shop with all the items in the game when this app was created.";
				break;
			case 4:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Competing Software Suggested Improvements";
				introText.text = "<b>Nasus Q Farming Simulator:</b>\n\n" +
					"<b>Accuracy:</b>​ Does not resemble League of Legends gameplay at all. In LoL, you must press Q, then right-click to attack a minion with the empowered attack, not press Q and slam your staff down. The proposed software would make an experience that is much closer to the feel of playing League of Legends by having the user right click to attack, using any empowering abilities beforehand.\n"+
					"<b>​Engagement:</b>​ It’s a bit boring just mashing Q for five minutes, even if it is close to the experience of playing the champion. The proposed software would have various different aspects in order to keep the user engaged, such as buying items and moving around.\n"+
					"<b>​Customization/Growth:​</b> The only stat that goes up during the game is the damage on Nasus’ Q. In League of Legends, you’d get gold from the minions to spend on items, making you even stronger. The proposed software would let you spend the gold you gain from minions on items to make you stronger as well as give you experience that levels you up, making you passively stronger.\n";
				break;
			case 5:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Competing Software Suggested Improvements";
				introText.text = "<b>CS Practice Tool:</b>\n\n" +
					"<b>Accuracy:</b> While much closer to the League of Legends experience than the previous software, in this tool the way you power yourself up is nothing like in League of Legends. Also, the projectiles in this tool move much slower than most attacks in LoL. The proposed software would address both problems, the first being addressed above with items and levelling up. The second problem would be solved by increasing the projectile speed and range based on what champion type you’d like to practice with.\n" +
					"<b>Options:</b> While this tool had options for attack speed, attack damage, and minion aggression, the proposed software would add more options for attack range, projectile speed, per-level stat growth, and execution threshold indicators on minions.\n" +
					"<b>Controls:</b> This tool felt very awkward to use, with movement feeling floaty and no clear indication of what you had selected to attack. The base attack speed is also very low. The proposed software would fix these issues by having more robust movement as well as indicators for attacks.\n";
				break;
			case 6:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Competing Software Suggested Improvements";
				introText.text = "<b>Last Hit - League of Legends:</b>\n\n" +
					"<b>Age:</b> This app has not been updated since 2017, since then, 17 new champions have been released, almost every item being changed or tweaked, and all visuals have been updated. The proposed software would attempt to look like the current version of LoL, or at least have up-to-date information.\n" +
					"<b>Controls:</b> Since this app uses mobile devices, its input method is through tapping a touchscreen. This is not accurate to LoL and would instead be through mouse and keyboard input in the proposed software. This app also does not have the user’s champion (playable character) visible. The proposed software would focus both on movement and minion killing.\n" +
					"<b>Accessibility/Aid:</b> Being a tool to help with killing minions, this app lacks any kind of aid for the user. The proposed software, as stated previously, would have indicators letting the user know when the minion could be killed by them and can be turned off by the user at any time.\n";
				break;
			case 7:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Center;
				personaImage.SetActive(false);
				titleText.text = "Stakeholders and Users";
				introText.text = "League of Legends Players\n" +
					"League of Legends Coaches";
				break;
			case 8:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				personaImage.SetActive(true);
				titleText.text = "Persona";
				introText.text = "<b>Name:</b>​ Christopher\n" +
					"<b>Age:</b>​ 17 years old\n" +
					"<b>Gender:</b>​ Male\n" +
					"<b>Location:</b>​ New York City, NY, USA\n" +
					"<b>Personal Notes:</b>\n" +
					"Competitive Gamer\n" +
					"Plays games with friends\n"+
					"<b>Player Notes:</b>\n" +
					"Wants to learn and master every game he picks up\n"+
					"Favors a support role\n" +
					"Has lower-end computer with a hard drive\n" +
					"Hates load times\n" +
					"Recently picked up League of Legends\n";
				break;
			case 9:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				personaImage.SetActive(false);
				titleText.text = "Persona Justification";
				introText.text = "Christopher came from reflection upon the core demographic of not only this software, but also League of Legends. A young adult, competitive, and eager to learn more about the game, he is the ideal user of this software. I chose this persona because new League players, especially those who like to play support, tend to struggle with farming minions above any other mechanic in the game.";
				break;
			case 10:
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Problem Scenario";
				introText.text = "Recently, Christopher’s friends decided to play League of Legends. They asked Christopher if he wanted to play, and he accepted. He normally plays support roles in the games he plays with friends. Anything from healers in MMOs to medics in First Person Shooters. At first, he had trouble grasping some of the mechanics like moving and aiming abilities. After a few games, he started to get the basics, albeit at a very low level.\n"+
					"By the end of the week, Christopher had fallen in love with League of Legends and wanted to play it all the time. His friends had tighter schedules and would play with Christopher when they could, but he found himself wanting to play even more than that. So, he started playing by himself. He would select Draft Pick and select support as his main role, with Fill as he secondary. He was slowly getting better and better, sharpening his Support skills every match.\n" +
					"All was going smooth, until he got placed in Middle Lane. This was because of his secondary Fill role selection. He was weary, but casted his abilities very well. However, he seemed to be falling behind in gold and couldn’t keep up with his opponent in items, slowly losing every battle against his opponent and eventually the game.\n";
				break;
			case 11:
				nextButton.SetActive(true);
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Activity Scenario";
				introText.text = "After that experience, Christopher looked up why he was falling behind in gold and realized it was because he was supposed to kill the minions in lane for gold. Up until this point, he had been ignoring them or using spells without the intention of landing the final hit, only to lower their health. Christopher then looked up how to better his minion farming abilities, only to be met with many videos telling him to practice in normal games. Christopher, being the eager mechanic learner he is, wants to get better. However, his computer and disdain for slow load times make it difficult for him to simply hop in games with a not-preferred role for the purpose of learning minion farming while also having to deal with opponents.\n" +
					"After more searching, Christopher found this software and began practicing in this lighter-weight and contained environment. He found it handled like the game pretty well and easily was able to cast abilities similarly to the game. Slowly, he started to get better at using abilities to kill enemy minions and was more confident in his farming abilities. He experimented with what abilities to use and when to use them, as well as seeing when was the best time to start firing an auto attack so it would kill when it landed. Going into League of Legends games with his friends now, he is more confident in trying new laning positions and even different support characters that are supposed to kill a few minions periodically. He is now even more invested in becoming better at this new game he loves.";
				break;
			case 12:
				nextButton.SetActive(false);
				introText.horizontalAlignment = HorizontalAlignmentOptions.Left;
				titleText.text = "Problem Statement";
				introText.text = "Christopher is a very competitive person, he wants to get better at every game he plays. He loves support roles, but sometimes League of Legends will force him into roles he’s not as comfortable in. This comes with mostly the same skill sets, except for minion farming, something that is non-existent for most support characters. Due to his slow computer paired with hating long load times, he does not want to possibly lose a game that may have taken a while to load, essentially wasting a huge portion of time in his eyes. Having a training tool separate from the game, its rankings, and its longer load times is a dream for him. He gains a deeper understanding of the game and its mechanics, only driving him to get better at it.";
				break;
			default:
				introSeqCount--;
				break;
		}
	}

	public void IntroSequenceBack()
	{
		introSeqCount-=2;
		IntroSequenceNext();
	}
}
