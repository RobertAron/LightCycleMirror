using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class GameRoundManager : MonoBehaviour
{
    [SerializeField] GameObject playerAvatar = default;
    Dictionary<NetworkConnection, PlayerInputManager> connectedPlayers = new Dictionary<NetworkConnection, PlayerInputManager>();
    List<GameObject> currentPlayers = new List<GameObject>();

    void Start(){
        Debug.Log("GAME STARTED");
    }

    public void AddPlayer(NetworkConnection networkConnection, PlayerInputManager playerInputManager){
        connectedPlayers.Add(networkConnection,playerInputManager);
        if(currentPlayers.Count==0) StartGame();
    }

    public void RemovePlayer(NetworkConnection networkConnection){
        connectedPlayers.Remove(networkConnection);
    }

    public void OnPlayerOut(GameObject gameObject){
        currentPlayers.Remove(gameObject);
        if(currentPlayers.Count==0) StartGame();
    }

    bool startGameAvailable = true;
    public void StartGame(){
        if(!startGameAvailable) return;
        foreach(var entry in connectedPlayers)
        {
            var newPlayers = Instantiate(playerAvatar);
            var playerBike = newPlayers.GetComponent<PlayerBike>();
            playerBike.onDestory+=OnPlayerOut;
            NetworkServer.Spawn(newPlayers);
            entry.Value.playerBike = playerBike;
            currentPlayers.Add(newPlayers);
        }
    }

    private void OnEnable() {
        startGameAvailable = true;
    }

    private void OnDisable() {
        startGameAvailable = false;
    }
}
