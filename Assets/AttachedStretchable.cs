using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class AttachedStretchable : NetworkBehaviour, Attachable {
    Stretchable stretchable;
    [SyncVar] GameObject _attachedTo;
    public GameObject attachedTo {
        set => _attachedTo = value;
        private get => _attachedTo;
    }

    public Vector3 attachpoint { 
        get {
            if(stretchable) return stretchable.startPosition;
            return transform.position;
        }
    }

    // Start is called before the first frame update
    void Start() {
        Vector3 startPosition = transform.position;
        stretchable = GetComponent<Stretchable>();
        stretchable.startPosition = startPosition;
        stretchable.endPosition = startPosition;
    }

    // Update is called once per frame
    void Update() {
        if (attachedTo == null) return;
        Attachable attachable = attachedTo.GetComponent<Attachable>();
        if(attachable == null) return;
        stretchable.endPosition =  attachable.attachpoint;
    }
}
