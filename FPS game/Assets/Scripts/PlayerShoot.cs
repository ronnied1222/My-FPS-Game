using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";
    [SerializeField] private Camera cam;
    public PlayerWeapon weapon;

    [SerializeField] LayerMask mask;

    private void Start() {
        if (cam == null){
            Debug.Log("No camera referecned (Player Shoot)");
            this.enabled = false;
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }

    }

    [Client]
    private void Shoot() {
        RaycastHit _hit;

        //start of ray, direction of ray, output parm, how far, layer mask 
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask) ){
            //hit something
            if (_hit.collider.tag == PLAYER_TAG){
                CmdPlayerWasShot(_hit.transform.name, weapon.damage);
            }

        }
    }

    [Command]
    void CmdPlayerWasShot(string _playerID, int damage) {
        Debug.Log(_playerID);
        Player p = GameController.GetPlayer(_playerID);

        p.RpcTakeDamage(damage);
    }
}
