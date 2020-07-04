using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInputManager : NetworkBehaviour
{
    public PlayerBike playerBike;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up")) CmdChangeDirection(Vector3.forward);
        if(Input.GetKeyDown("down")) CmdChangeDirection(Vector3.back);
        if(Input.GetKeyDown("left")) CmdChangeDirection(Vector3.left);
        if(Input.GetKeyDown("right")) CmdChangeDirection(Vector3.right);
    }

    [Command]
    void CmdChangeDirection(Vector3 direction){
        if(playerBike==null) return;
        playerBike.ChangeDirection(direction);
    }
}