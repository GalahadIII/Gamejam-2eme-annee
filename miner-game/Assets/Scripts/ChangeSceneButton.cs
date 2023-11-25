using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    public string sceneToLoad; // Le nom de la scène à charger

    // Fonction appelée lors du clic sur le bouton
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}