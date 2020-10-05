using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
	public void SetScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
