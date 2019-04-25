using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour
{
	public static Dead instance;

	void Start()
	{
		CheckSingleton();
		Invoke("Reload", 3);
	}

	void CheckSingleton()
	{
		if(instance != null)
		{
			if(instance != this)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			instance = this;
		}
	}

	void Reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}