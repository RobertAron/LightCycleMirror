using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Mirror;

public class DestoryOnCollision : NetworkBehaviour
{
    [SerializeField] bool runOnEnter = true;
    [SerializeField] bool runOnExit = false;
    [SerializeField] List<string> activationTypes = default;

    private void OnTriggerEnter(Collider other) {
        if(runOnEnter) DestoryOnMatch(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if(runOnExit) DestoryOnMatch(other.gameObject);
    }


    public void DestoryOnMatch(GameObject gameObject){
        var pb = gameObject.GetComponent<PlayerBike>();
        bool match = activationTypes.Any(c => gameObject.GetComponent(c)!=null);
        if(match) Destroy(gameObject);
    }
}
