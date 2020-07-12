using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour {
    [SerializeField] Renderer renderToUpdate = default;
    Material cachedMaterial;
    public Color color {
        set { cachedMaterial.color = value; }
        get { return cachedMaterial.color; }
    }

    void Awake() {
        cachedMaterial = renderToUpdate.material;
    }

    void OnDestroy() {
        Destroy(cachedMaterial);
    }
}
