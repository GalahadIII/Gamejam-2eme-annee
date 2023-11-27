using UnityEngine;
public class ExitButton : MonoBehaviour
{
    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Temporaire -> quit test editeur
        #endif

        #if !UNITY_EDITOR
        Application.Quit();
        #endif
    }
}