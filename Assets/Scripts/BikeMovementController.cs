using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public delegate void MultiDelegate(GameObject gameObject);
struct BikeMovementPosition{
    public BikeMovementPosition(Vector3 position,Vector3 direction){
        this.position = position;
        this.direction = direction;
        this.time = NetworkTime.time;
    }
    public Vector3 position;
    public Vector3 direction;
    public double time;
}

public class BikeMovementController : NetworkBehaviour {
    public MultiDelegate onDestory = default;
    [SerializeField] float speed = default;
    [SyncVar] BikeMovementPosition movementPosition;

    public override void OnStartServer() {
        movementPosition = new BikeMovementPosition(transform.position,transform.forward);
    }

    void OnDestroy() {
        if (onDestory != null) onDestory(this.gameObject);
    }

    public void ChangeDirection(Vector3 direction) {
        movementPosition = new BikeMovementPosition(transform.position,direction);
    }

    void Update() {
        float timeSinceLastTime = (float)(NetworkTime.time - movementPosition.time);
        Vector3 offset = movementPosition.direction * timeSinceLastTime * speed;
        transform.position = movementPosition.position + offset;
    }
}