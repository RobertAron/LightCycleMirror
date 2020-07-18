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

public class BikeMovementController : NetworkBehaviour, Attachable {
    public MultiDelegate onDestory = default;
    [SerializeField] float speed = default;
    Beam trail = default;
    [SerializeField] GameObject trailPrefab = default;
    [SyncVar] BikeMovementPosition movementPosition;
    [SerializeField] Vector3 spawnOffset = default;
    public Vector3 attachpoint => transform.position + transform.rotation * spawnOffset;

    public override void OnStartServer() {
        movementPosition = new BikeMovementPosition(transform.position,transform.forward);
        StartNewTrail(Vector3.zero);
    }

    [ServerCallback]
    void OnDestroy() {
        if (onDestory != null) onDestory(this.gameObject);
    }

    [Server]
    public void ChangeDirection(Vector3 direction) {
        Vector3 originalAttachmentPosition = attachpoint;
        movementPosition = new BikeMovementPosition(transform.position,direction);
        UpdatePosition();
        StartNewTrail(originalAttachmentPosition-attachpoint);
    }

    void UpdatePosition(){
        float timeSinceLastTime = (float)(NetworkTime.time - movementPosition.time);
        Vector3 offset = movementPosition.direction * timeSinceLastTime * speed;
        transform.position = movementPosition.position + offset;
        transform.rotation = Quaternion.LookRotation(movementPosition.direction);
    }

    void Update() {
        UpdatePosition();
    }

    [Server]
    void StartNewTrail(Vector3 offset){
        var newTrail = Instantiate(trailPrefab,attachpoint,transform.rotation);
        var beam = newTrail.GetComponent<Beam>();
        beam.Init(gameObject,attachpoint,offset);
        NetworkServer.Spawn(newTrail);
        if(trail!=null) trail.attachedTo = newTrail;
        trail = beam;
    }
}