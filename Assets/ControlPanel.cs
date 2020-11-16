using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
	public RectTransform panel;
	bool collapsed = false;
	public void Collapse()
	{
		if (!collapsed)
		{
			panel.rect.Set(panel.rect.x, -90, panel.rect.width, panel.rect.height);
			collapsed = true;
		}
		else
		{
			Expand();
			collapsed = false;
		}
	}

	public void Expand()
	{ 
		panel.rect.Set(panel.rect.x, 80, panel.rect.width, panel.rect.height);
	}
}
