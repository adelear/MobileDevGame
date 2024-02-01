using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Cats")]
public class CatData : ScriptableObject
{
    public new string name;
    public string backstory;
    public Sprite portrait; 
}
