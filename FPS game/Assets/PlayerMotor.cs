using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class PlayerMotor : MonoBehaviour {

    [SerializeField] Camera cam;
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 thrusterForce = Vector3.zero;


    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    //gets movement vector
    public void Move(Vector3 _velocity){
        velocity = _velocity;

    }

    public void ApplyThruster(Vector3 _thruster){
        thrusterForce = _thruster;
    }

    //gets rotation vector
    public void Rotate(Vector3 _rotate){
        rotation = _rotate;
    }

    //gets camera vector
    public void RotateCamera(Vector3 _cameraRotation){
        cameraRotation = _cameraRotation;
    }

    //run every physics iteration
    private void FixedUpdate() {
        PerfromMovement();
        PerformRotation();
    }

    //perform movement based on velocity varaiable
    private void PerfromMovement() {
        if (velocity != Vector3.zero){
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }

        if (thrusterForce != Vector3.zero){
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    public void PerformRotation() {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null){
            cam.transform.Rotate(-cameraRotation);
        }

    }


}
