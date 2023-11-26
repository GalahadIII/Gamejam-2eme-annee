using UnityEngine;

public class Close_UI : MonoBehaviour
{
    [SerializeField] private new GameObject gameObject;

    public void Close_GameObject()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
