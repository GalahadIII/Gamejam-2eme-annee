using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject pauseUI;
    public void Setting()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }
}
