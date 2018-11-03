using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    #region Player Registering


    private static Dictionary<string, Player> players = new Dictionary<string, Player>();


    public static void RegisterPlayer(string _netID, Player _player) {
        string _id = "Player " + _netID;
        players.Add(_id, _player);

        _player.transform.name = _id;
    }

    public static Player GetPlayer(string playerID) {
        return players[playerID];
    }

    public static void UnRegisterPlayer(string _id) {
        players.Remove(_id);
    }
    #endregion


    public MatchSettings matchSettings;

    public static GameController instance;

    private void Awake() {
        if(instance != null){
            Debug.LogError("More than 1 Game Controller in Scene");
        }else{
            instance = this;
        }


    }

    //private void OnGUI() {
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string s in players.Keys){
    //        GUILayout.Label(s + " - " + players[s].transform.name);
    //    }

    //    GUILayout.EndVertical();


    //    GUILayout.EndArea();
    //}

}
