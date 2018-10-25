using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public GameObject camera;
    public float vel;
    private float screenPadding = 25.0f;
	
	// Update is called once per frame
	private void Update () {
	    if (Input.mousePosition.x >= Screen.width - screenPadding)
        {
            camera.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.World);
        }
        else if (Input.mousePosition.x <= screenPadding)
        {
            camera.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.y >= Screen.height - screenPadding)
        {
            camera.transform.Translate(Vector3.forward * vel * Time.deltaTime, Space.World);
        }
        else if (Input.mousePosition.y <= screenPadding)
        {
            camera.transform.Translate(Vector3.back * vel * Time.deltaTime, Space.World);
        }
	}
}
