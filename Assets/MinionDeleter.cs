using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDeleter : MonoBehaviour
{
    public bool deleteBlue;
	// Start is called before the first frame update
	private void OnTriggerEnter(Collider other)
	{
		if (!deleteBlue&&other.gameObject.tag.Equals("Enemy"))
		{
			minion mminion = other.gameObject.GetComponent<minion>();
			if (mminion)
			{
				mminion.Die();
			}
		}
		else if(deleteBlue && other.gameObject.tag.Equals("Ally"))
		{
			minion mminion = other.gameObject.GetComponent<minion>();
			if (mminion)
			{
				mminion.Die();
			}
		}
	}
}
