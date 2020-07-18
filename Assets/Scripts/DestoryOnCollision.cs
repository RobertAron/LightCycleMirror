using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class DestoryOnCollision : NetworkBehaviour {
    [SerializeField] bool runOnEnter = true;
    [SerializeField] bool runOnExit = false;
    [SerializeField] List<TextAsset> activationTypes = default;

    private void OnTriggerEnter(Collider other) {
        if (runOnEnter) DestoryOnMatch(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        if (runOnExit) DestoryOnMatch(other.gameObject);
    }

    [ServerCallback]
    public void DestoryOnMatch(GameObject gameObject) {
        var pb = gameObject.GetComponent<BikeMovementController>();
        float minSize = Mathf.Min(
            transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
        bool match = activationTypes
            .ConvertAll(activationType => activationType.name)
            .Any(cName => gameObject.GetComponent(cName) != null);
        if (match && minSize!=0) Destroy(gameObject);
    }
}
