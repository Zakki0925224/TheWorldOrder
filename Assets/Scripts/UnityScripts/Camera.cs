using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Camera MainCamera;
    private GameObject ClickedGameObject;

    void Update()
    {
        var cameraPosition = this.MainCamera.transform.position;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cameraPosition.x += 1 * Time.deltaTime * Constants.MoveCameraSpeed;
            this.MainCamera.transform.position = cameraPosition;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cameraPosition.x -= 1 * Time.deltaTime * Constants.MoveCameraSpeed;
            this.MainCamera.transform.position = cameraPosition;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cameraPosition.z += 1 * Time.deltaTime * Constants.MoveCameraSpeed;
            this.MainCamera.transform.position = cameraPosition;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cameraPosition.z -= 1 * Time.deltaTime * Constants.MoveCameraSpeed;
            this.MainCamera.transform.position = cameraPosition;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && cameraPosition.y > Constants.MinCameraPositionY)
        {
            cameraPosition.y -= 1 * Time.deltaTime * Constants.MoveCameraSpeed;
            this.MainCamera.transform.position = cameraPosition;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && cameraPosition.y < Constants.MaxCameraPositionY)
        {
            cameraPosition.y += 1 * Time.deltaTime * Constants.MoveCameraSpeed;
            this.MainCamera.transform.position = cameraPosition;
        }
    }
}
