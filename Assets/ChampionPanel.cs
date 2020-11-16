using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChampionPanel : MonoBehaviour
{
    public player playerPlayer;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;
    public Toggle rangeToggle;
    // Start is called before the first frame update
    void Start()
    {
        UpdateNumbers();
    }

    public void UpdateNumbers()
	{
        damageText.text = "<b>Damage:</b><br>" + playerPlayer.damage;
        rangeText.text = "<b>Attack Range:</b><br>" + playerPlayer.range;
        attackSpeedText.text = "<b>Attack Speed:</b><br>" + playerPlayer.atkSpeed;
        moveSpeedText.text = "<b>Move Speed:</b><br>" + playerPlayer.movementSpeed;
        rangeToggle.isOn = playerPlayer.ranged;
    }

    public void ModDamage(int amount)
	{
        playerPlayer.damage += amount;
        UpdateNumbers();
        RecalcMinionHealthbars();
    }

    public void ModRange(int amount)
    {
        playerPlayer.range += amount;
        UpdateNumbers();
    }

    public void ModAttackkSpeed(float amount)
    {
        playerPlayer.atkSpeed += amount;
        UpdateNumbers();
    }

    public void ModSpeed(int amount)
    {
        playerPlayer.SetMoveSpeed(playerPlayer.movementSpeed + amount);
        UpdateNumbers();
    }

    public void ToggleRangeDisplay()
	{
        playerPlayer.ToggleRangeDisplay();
	}

    public void SetPresetMarksman()
	{
        playerPlayer.damage = 62;
        playerPlayer.range = 575;
        playerPlayer.atkSpeed = 0.7f;
        playerPlayer.SetMoveSpeed(325);
        playerPlayer.ranged = true;
        UpdateNumbers();
        RecalcMinionHealthbars();
    }

    public void SetPresetMage()
    {
        playerPlayer.damage = 54;
        playerPlayer.range = 550;
        playerPlayer.atkSpeed = 0.6f;
        playerPlayer.SetMoveSpeed(330);
        playerPlayer.ranged = true;
        UpdateNumbers();
        RecalcMinionHealthbars();
    }

    public void SetPresetTank()
    {
        playerPlayer.damage = 68;
        playerPlayer.range = 175;
        playerPlayer.atkSpeed = 0.6f;
        playerPlayer.SetMoveSpeed(345);
        playerPlayer.ranged = false;
        UpdateNumbers();
        RecalcMinionHealthbars();
    }

    public void RecalcMinionHealthbars()
	{
        minion.RecalcAllHealthbars();
	}

    public void ToggleRanged()
    {
        playerPlayer.ranged = !playerPlayer.ranged;
    }
}
