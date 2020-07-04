﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public delegate void MultiDelegate(GameObject gameObject);


public class PlayerBike : NetworkBehaviour
{
    public MultiDelegate onDestory;
    [SerializeField] float speed;
    [SyncVar] Vector3 currentDirection = Vector3.forward;
    [SyncVar] Vector3 lastTurnOrigin = new Vector3(0,0,0);
    [SyncVar] double movementTime = NetworkTime.time;

    void OnDestroy() {
        if(onDestory!=null) onDestory(this.gameObject);
    }

    public void ChangeDirection(Vector3 direction){
        lastTurnOrigin = transform.position;
        movementTime = NetworkTime.time;
        currentDirection = direction;
    }

    void Update(){
        float timeSinceLastTime = (float)(NetworkTime.time - movementTime);
        Vector3 offset = currentDirection * timeSinceLastTime * speed;
        transform.position = lastTurnOrigin + offset;
    }

}
