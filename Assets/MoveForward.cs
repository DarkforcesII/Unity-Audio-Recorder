using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // finding hmd
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }

        // finding controller
        var gameControllers = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.GameController, gameControllers);

        foreach (var device in gameControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has role '{1}'", device.name, device.role.ToString()));
        }
    }



    void moveForward()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveForward();
    }
}
