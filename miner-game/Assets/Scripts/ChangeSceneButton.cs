using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] int sceneToLoad;

    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }
}