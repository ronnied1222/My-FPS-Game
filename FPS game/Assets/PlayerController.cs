using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {


    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    private PlayerMotor motor;

    private void Start() {
        motor = GetComponent<PlayerMotor>();    
    }

    private void Update() {
        //calculate movement 
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        //final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        //apply movement
        motor.Move(velocity);

        //Calculate rotation; only applies for going left and right, you dont want the player to move up and down only the camera
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0, _yRot, 0) * lookSensitivity;

        //apply rotation
        motor.Rotate(rotation);

        //Calculate camera rotation
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 cameraRotation = new Vector3(_xRot, 0, 0) * lookSensitivity;

        //apply rotation
        motor.RotateCamera(cameraRotation);
    }


}
