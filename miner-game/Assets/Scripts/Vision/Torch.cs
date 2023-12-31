using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField]
    private Transform changedTransform;
    public void OnEnable()
    {
        if (changedTransform == null) return;
        if (transform.localScale == Vector3.one) return;

        changedTransform.localScale = transform.localScale;
        transform.localScale = new Vector3(1, 1, 1);

        FieldOfView fov = GetComponentInChildren<FieldOfView>();
    }
}
