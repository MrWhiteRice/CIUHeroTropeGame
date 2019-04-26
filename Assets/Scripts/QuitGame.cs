using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
	float yeet = 0;

	private void Update()
	{
		yeet += Time.deltaTime;

		if(yeet > 1)
		{
			Application.Quit();
		}
	}
}