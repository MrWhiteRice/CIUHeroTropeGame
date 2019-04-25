using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	public LayerMask layer;

	Camera main;
	Camera cam;

    void Start()
    {
		main = Camera.main;

		int layerInt = (int)Mathf.Log(layer.value, 2);

		for(int x = 0; x < transform.childCount; x++)
		{
			transform.GetChild(x).gameObject.layer = layerInt;
		}

		cam = Instantiate(GameObject.FindGameObjectWithTag("CameraSpawn").GetComponent<Camera>(), main.transform);

		print("layer " + layerInt);
		if(layerInt-10 == 3)
		{
			print("asd");
			cam.clearFlags = CameraClearFlags.SolidColor;
		}
		else
		{
			cam.clearFlags = CameraClearFlags.Nothing;
		}

		cam.cullingMask = 0;
		cam.cullingMask |= (1 << layerInt);

		cam.orthographicSize = 6 + layerInt - 10;
		cam.depth = -2 - (layerInt - 10);
    }
}