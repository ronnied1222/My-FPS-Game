using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour {
    

    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 3f;
    private PlayerMotor motor;
    private ConfigurableJoint joint;

    [SerializeField] private float thrusterForce = 1000f;

    [Header("Joint Options")]
    //[SerializeField] private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField] private float jointSpring = 20f;
    [SerializeField] private float jointMaxForce = 40f;

    private void Start() {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
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

        //apply thruster force
        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump")){
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        } else {
            SetJointSettings(jointSpring);
        }

        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring) {
        joint.yDrive = new JointDrive {positionSpring = _jointSpring, 
            maximumForce = jointMaxForce 
        };


    }

}
