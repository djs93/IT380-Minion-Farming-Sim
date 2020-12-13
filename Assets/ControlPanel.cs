using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPanel : MonoBehaviour
{
	public RectTransform panel;
	public TextMeshProUGUI buttonText;
	public Animator animator;

	public void Collapse()
	{
		bool collapsed = animator.GetBool("open");

		if (!collapsed)
		{
			buttonText.text = "Expand";
		}
		else
		{
			buttonText.text = "Collapse";
		}

		animator.SetBool("open", !collapsed);
	}
}
