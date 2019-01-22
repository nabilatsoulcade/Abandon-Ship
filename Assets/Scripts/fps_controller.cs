using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps_controller : MonoBehaviour {

    //Initialize Variables
    public float playerSpeed = 2f; //Player Speed
    public float cameraSensitivity = 2f; //Camera Sensitivity
    public bool invertedCameraHorizontal = false;
    public bool invertedCameraVertical = false;
    public enum inputDevice {autoDetect, KeyboardAndMouse, Gamepad };
    public inputDevice device_input;

	float hor_movement_input; //Horizontal Movement Input
	float ver_movement_input; //Vertical Movement Input

    float rotation_x_input; //Camera X Rotation Input
    float rotation_y_input; //Camera Y Rotation Input

    float rotation_x; //Camera X Rotation
    float rotation_y; //Camera Y Rotation

	float hor_movement; //Horizontal Movement
	float ver_movement; //Vertical Movement

    //Intialize Objects
    CharacterController player;
    public GameObject player_camera;

    // Use this for initialization
    void Start () {
        //Get Required Components
        player = GetComponent<CharacterController>();
        device_input = inputDevice.autoDetect;
	}

	// Update is called once per frame
	void Update () {

        //Get Input
        get_input();

        //Calculate Camera Direction
        switch(invertedCameraHorizontal)
            {
            case true:
                rotation_x = -rotation_x_input * cameraSensitivity;
                break;

            case false:
                rotation_x = rotation_x_input * cameraSensitivity;
                break;
            }

        switch (invertedCameraVertical)
        {
            case true:
                rotation_y = -rotation_y_input * cameraSensitivity;
                break;

            case false:
                rotation_y = rotation_y_input * cameraSensitivity;
                break;
        }

        //Calculate Movement In-Game Movement Speed
        hor_movement = hor_movement_input * playerSpeed;
	    ver_movement = ver_movement_input * playerSpeed;
        Vector3 movement = new Vector3(hor_movement, 0, ver_movement);
        transform.Rotate(0, rotation_x, 0);

        //Apply Movement & Camera Calculations
        player_camera.transform.Rotate(-rotation_y, 0, 0);
        movement = transform.rotation * movement;
        player.Move(movement * Time.deltaTime);

        
    }

    void get_input()
    {
        
        //Get Input

        //If you Override the Input Device with the Inspector, this switch statement will pull the correct input
        if (device_input != inputDevice.autoDetect)
        {
        switch (device_input)
            {
                //Override to use Keyboard and Mouse
                case inputDevice.KeyboardAndMouse:
                    rotation_x_input = Input.GetAxis("Mouse X"); //Mouse Horizontal Axis
                    rotation_y_input = Input.GetAxis("Mouse Y"); //Mouse Vertical Axis

                    hor_movement_input = Input.GetAxis("Horizontal");//Horizontal Movement Input
                    ver_movement_input = Input.GetAxis("Vertical"); //Vertical Input
                    break;

                //Override to use Gamepad
                case inputDevice.Gamepad:
                    rotation_x_input = Input.GetAxis("HorizontalAlt"); //Mouse Horizontal Axis
                    rotation_y_input = -Input.GetAxis("VerticalAlt"); //Mouse Vertical Axis

                    hor_movement_input = Input.GetAxis("Horizontal");//Horizontal Movement Input
                    ver_movement_input = -Input.GetAxis("Vertical"); //Vertical Input
                    break;
            }
            
        }
        //If the Device type isn't overridden with inspector, auto assign the input device based off of gamepad avalibility
        else
        {
            rotation_x_input = Input.GetAxis("Mouse X"); //Mouse Horizontal Axis
            rotation_y_input = Input.GetAxis("Mouse Y"); //Mouse Vertical Axis

            hor_movement_input = Input.GetAxis("Horizontal");//Horizontal Movement Input
            ver_movement_input = Input.GetAxis("Vertical"); //Vertical Input
        }
        
    }
}
