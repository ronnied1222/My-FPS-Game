using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField] Behaviour[] compentsToDisable;
    [SerializeField] string remoteLayerName = "RemotePlayer";
    Camera sceneCamera;

    void Start() {
        DisableComponents();
        AssignRemoteLayer();
    }

    void DisableComponents () {
        if (!isLocalPlayer) {
            for (int i = 0; i < compentsToDisable.Length; i++) {
                compentsToDisable[i].enabled = false;
            }
        } else {
            sceneCamera = Camera.main;
            if (sceneCamera != null) {
                sceneCamera.gameObject.SetActive(false);
            }
        }

        GetComponent<Player>().Setup();
    }

    public override void OnStartClient() {
        base.OnStartClient();

        string _id = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameController.RegisterPlayer(_id, _player);
    }
    void AssignRemoteLayer () {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void OnDisable() {
        if (sceneCamera != null){
            sceneCamera.gameObject.SetActive(true);
        }

        GameController.UnRegisterPlayer(transform.name);
    }

}
