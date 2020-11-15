using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChampionPanel : MonoBehaviour
{
    public player playerPlayer;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;
    // Start is called before the first frame update
    void Start()
    {
        damageText.text = "<b>Damage:</b><br>"+playerPlayer.damage;
        rangeText.text = "<b>Attack Range:</b><br>"+playerPlayer.range;
        attackSpeedText.text = "<b>Attack Speed:</b><br>"+playerPlayer.atkSpeed;
        moveSpeedText.text = "<b>Move Speed:</b><br>"+playerPlayer.movementSpeed;
    }
}
