using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead {
        get { return _isDead; }
        protected set { _isDead = value; }
    }
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SyncVar]
    private int currentHealth;

    public void Setup() {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++) {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    public void SetDefaults(){
        isDead = false;
        for (int i = 0; i < disableOnDeath.Length; i++) {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider col = GetComponent<Collider>();
        if(col != null){
            col.enabled = true;
        }

        currentHealth = maxHealth;
    }

    [ClientRpc]
    public void RpcTakeDamage(int amount){
        if(isDead){
            return;   
        }
        currentHealth -= amount;
        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if(currentHealth <= 0){
            Die();
        }
    }

    private void Die() {
        isDead = true;

        //disable components on player 
        for (int i = 0; i < disableOnDeath.Length; i++) {
            disableOnDeath[i].enabled = false;
        }
        Collider col = GetComponent<Collider>();
        if (col != null) {
            col.enabled = false;
        }

        //respawn
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn() {
        yield return new WaitForSeconds(GameController.instance.matchSettings.RESPAWN_DELAY);

        SetDefaults();
        Transform spawnPoint = NetworkManager.singleton.GetStartPosition();

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }

}
