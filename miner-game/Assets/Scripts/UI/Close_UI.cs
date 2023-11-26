using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close_UI : MonoBehaviour
{
    [SerializeField] GameObject forgeUI_transform;

    public void Close_GameObject()
    {
        forgeUI_transform.SetActive(false);
    }
}
