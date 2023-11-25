using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool HasVisual = true;
    public float CamHeight;
    public Animator animator;
    public AudioClip _ac;

    private AudioSource _as;

    private void Start()
    {
        _as = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        Debug.Log(gameObject.name);

        if (!HasVisual) return;
        animator!.SetTrigger("isTriggered");
        _as!.PlayOneShot(_ac);


    }

}
