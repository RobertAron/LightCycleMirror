using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Beam : NetworkBehaviour, Attachable {
    [SerializeField] Stretchable stretchable = default;
    [SyncVar] public Vector3 attachmentOffset = Vector3.zero;
    [SyncVar] public Vector3 spawnPosition = Vector3.zero;
    [SyncVar] GameObject _attachedTo;
    public GameObject attachedTo {
        set => _attachedTo = value;
        private get => _attachedTo;
    }

    public Vector3 attachpoint { 
        get => stretchable.startPosition + attachmentOffset;
    }

    // Update is called once per frame
    void Update() {
        Reposition();
    }

    public void Reposition(){
        stretchable.startPosition = spawnPosition;
        if (attachedTo == null) return;
        Attachable attachable = attachedTo.GetComponent<Attachable>();
        if(attachable == null) return;
        stretchable.endPosition =  attachable.attachpoint;
    }
}
