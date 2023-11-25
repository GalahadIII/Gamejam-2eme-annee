using UnityEngine;

public class PassScaleVisual : MonoBehaviour
{
    [SerializeField]
    private Transform changedTransform;
    public void OnEnable()
    {
        if (changedTransform == null) return;
        changedTransform.localScale = transform.localScale;
        transform.localScale = new Vector3(1, 1, 1);
    }
}
