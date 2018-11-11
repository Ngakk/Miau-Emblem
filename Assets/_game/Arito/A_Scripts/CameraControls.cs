using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {

    public GameObject camera;
    public float vel;
    public float screenPadding;
    private Vector2 mousePos;

    private void Update()
    {
        mousePos = Input.mousePosition;
        if (mousePos.x >= Screen.width - screenPadding)
            CamMovement("left");
        else if (mousePos.x <= screenPadding)
            CamMovement("right");
        if (mousePos.y >= Screen.height - screenPadding)
            CamMovement("up");
        else if (mousePos.y <= screenPadding)
            CamMovement("down");
    }

    // Direction of Movement
    public void CamMovement(string _dir)
    {
        switch (_dir)
        {
            default:
                break;

            case "up":
                camera.transform.Translate(Vector3.forward * vel * Time.deltaTime, Space.World);
                break;

            case "down":
                camera.transform.Translate(Vector3.back * vel * Time.deltaTime, Space.World);
                break;

            case "left":
                camera.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.World);
                break;

            case "right":
                camera.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.World);
                break;

            case "topRight":
                camera.transform.Translate(Vector3.forward * vel * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.World);
                break;

            case "topLeft":
                camera.transform.Translate(Vector3.forward * vel * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.World);
                break;

            case "bottomRight":
                camera.transform.Translate(Vector3.back * vel * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.World);
                break;

            case "bottomLeft":
                camera.transform.Translate(Vector3.back * vel * Time.deltaTime, Space.World);
                camera.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.World);
                break;
        }
        
    }
}
