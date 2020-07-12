using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public delegate void MultiDelegate(GameObject gameObject);


public class BikeMovementController : NetworkBehaviour {
    public MultiDelegate onDestory = default;
    [SerializeField] float speed = default;
    [SyncVar] Vector3 currentDirection = Vector3.forward;
    [SyncVar] Vector3 lastTurnOrigin;
    [SyncVar] double movementTime = NetworkTime.time;

    void Awake() {
        lastTurnOrigin = transform.position;
    }

    void OnDestroy() {
        if (onDestory != null) onDestory(this.gameObject);
    }

    public void ChangeDirection(Vector3 direction) {
        lastTurnOrigin = transform.position;
        movementTime = NetworkTime.time;
        currentDirection = direction;
    }

    void Update() {
        float timeSinceLastTime = (float)(NetworkTime.time - movementTime);
        Vector3 offset = currentDirection * timeSinceLastTime * speed;
        transform.position = lastTurnOrigin + offset;
    }
}