using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_UI : MonoBehaviour
{
    [SerializeField] GameObject gameObject;

    public void Close_GameObject()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
