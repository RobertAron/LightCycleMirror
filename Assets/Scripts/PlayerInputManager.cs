using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInputManager : NetworkBehaviour {
    BikeMovementController bikeMovementController;
    ColorController colorController {
        set { if (isLocalPlayer) value.color = Color.green; }
    }

    GameObject _playerBike;
    public GameObject playerBike {
        set {
            _playerBike = value;
            bikeMovementController = value.GetComponent<BikeMovementController>();
            colorController = value.GetComponent<ColorController>();
        }
        get { return _playerBike; }
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