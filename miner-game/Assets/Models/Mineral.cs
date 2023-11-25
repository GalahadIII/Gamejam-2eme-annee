using UnityEngine;

[CreateAssetMenu(menuName = "Models/Mineral")]
public class Mineral : ScriptableObject
{
    [SerializeField] public Sprite[] mineralSprites;
    [SerializeField] public string mineralName;
    [SerializeField] public int mineralHardness;
}