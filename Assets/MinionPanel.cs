using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinionPanel : MonoBehaviour
{
    public float damageMod;
    public float armorMod;
    public float atkSpeedMod;
    public float movespeedMod;
    public float healthMod;

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI healthText;

    public WaveManager waveManager;
    // Start is called before the first frame update
    public void ToggleExeBars()
    {
        minion.ToggleExecuteBars();
        waveManager.ToggleExecutionBarsOnSpawn();
    }

    void Start()
    {
        UpdateNumbers();
    }

    public void UpdateNumbers()
    {
        damageText.text = "<b>Damage Mod:</b><br>" + damageMod;
        armorText.text = "<b>Armor Mod:</b><br>" + armorMod;
        attackSpeedText.text = "<b>Attack Speed Mod:</b><br>" + atkSpeedMod;
        moveSpeedText.text = "<b>Move Speed Mod:</b><br>" + movespeedMod;
        healthText.text = "<b>Health Mod:</b><br>" + healthMod;
    }

    public void ModDamageMod(float amount)
	{
        damageMod += amount;
        UpdateNumbers();
    }

    public void ModArmorMod(float amount)
    {
        armorMod += amount;
        UpdateNumbers();
        minion.RecalcAllHealthbars();
    }

    public void ModAtkSpeedMod(float amount)
    {
        atkSpeedMod += amount;
        UpdateNumbers();
    }

    public void ModMoveSpeedMod(float amount)
    {
        movespeedMod += amount;
        UpdateNumbers();
    }

    public void ModHealthMod(float amount)
    {
        healthMod += amount;
        UpdateNumbers();
        minion.RecalcAllHealthbars();
    }
}
