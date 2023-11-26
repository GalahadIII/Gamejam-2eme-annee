using UnityEngine;
public class ExitButton : MonoBehaviour
{
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Temporaire -> quit test editeur
        //Application.Quit();
    }
}