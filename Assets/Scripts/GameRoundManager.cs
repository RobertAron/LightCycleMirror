using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class GameRoundManager : MonoBehaviour {
    [SerializeField] GameObject playerAvatar = default;
    Dictionary<NetworkConnection, PlayerInputManager> connectedPlayers = new Dictionary<NetworkConnection, PlayerInputManager>();
    List<GameObject> currentPlayers = new List<GameObject>();


    public void AddPlayer(NetworkConnection networkConnection, PlayerInputManager playerInputManager) {
        connectedPlayers.Add(networkConnection, playerInputManager);
        if (currentPlayers.Count == 0) StartGame();
    }

    public void RemovePlayer(NetworkConnection networkConnection) {
        connectedPlayers.Remove(networkConnection);
    }

    public void OnPlayerOut(GameObject gameObject) {
        currentPlayers.Remove(gameObject);
        if (currentPlayers.Count == 0) StartGame();
    }

    bool startGameAvailable = true;
    public void StartGame() {
        if (!startGameAvailable) return;
        foreach(var obj in FindObjectsOfType<Beam>()){
            Destroy(obj.gameObject);
        }
        foreach (var entry in connectedPlayers) {
            var newPlayer = Instantiate(
                playerAvatar,
                new Vector3(0, 1, 0),
                Quaternion.Euler(0, 0, 0)
            );
            var playerBike = newPlayer.GetComponent<BikeMovementController>();
            playerBike.onDestory += OnPlayerOut;
            NetworkServer.Spawn(newPlayer);
            entry.Value.playerBike = newPlayer;
            currentPlayers.Add(newPlayer);
        }
    }

    private void OnEnable() {
        startGameAvailable = true;
    }

    private void OnDisable() {
        startGameAvailable = false;
    }
}
