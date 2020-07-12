﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInputManager : NetworkBehaviour {
    BikeMovementController bikeMovementController;
    [SyncVar(hook=nameof(PlayerBikeHook))] GameObject playerBike;
    void PlayerBikeHook(GameObject _, GameObject gameObject){
        bikeMovementController = gameObject.GetComponent<BikeMovementController>();
        if(isLocalPlayer) gameObject.GetComponent<ColorController>().color = Color.green;
    }
    public void SetPlayerBike(GameObject gameObject){
        playerBike = gameObject;
        PlayerBikeHook(null,gameObject);
    }

    // Update is called once per frame
    void Update() {
        if (!isLocalPlayer) return;
        if (Input.GetKeyDown("up")) CmdChangeDirection(Vector3.forward);
        if (Input.GetKeyDown("down")) CmdChangeDirection(Vector3.back);
        if (Input.GetKeyDown("left")) CmdChangeDirection(Vector3.left);
        if (Input.GetKeyDown("right")) CmdChangeDirection(Vector3.right);
    }

    [Command]
    void CmdChangeDirection(Vector3 direction) {
        if (playerBike == null) return;
        bikeMovementController.ChangeDirection(direction);
    }
}