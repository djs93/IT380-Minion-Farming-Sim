using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMinion : MonoBehaviour
{
    public TutorialManager tutorialManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	internal void TakeDamage(float damage, bool fromPlayer)
	{
        tutorialManager.CompleteCurrentGoal();
	}
}
