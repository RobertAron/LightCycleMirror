using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Stretchable : MonoBehaviour
{
    [SerializeField] Vector3 _startPosition;
    public Vector3 startPosition{
        get => _startPosition;
        set{
            _startPosition = value;
            UpdateScaleRotation();
        }
    }
    [SerializeField] Vector3 _endPosition;
    public Vector3 endPosition{
        get => _endPosition;
        set{
            _endPosition = value;
            UpdateScaleRotation();
        }
    }
    float length{
        get => (startPosition - endPosition).magnitude;
    }

    private void Update() {
        if(!Application.IsPlaying(gameObject)) UpdateScaleRotation();
    }

    void UpdateScaleRotation(){
        Vector3 vector = endPosition-startPosition;
        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y,
            length
        );
        transform.position = (startPosition+endPosition)/2;
        if(vector!=Vector3.zero) transform.rotation = Quaternion.LookRotation(vector);
    }
}
