using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Cats")]
public class CatData : ScriptableObject
{
    public string catName;
    public int cost; 
    public string backstory;
    public bool isOwned; 

    public Sprite portraitRegular;
    public Sprite portraitSelected;
    public Sprite portraitOwned;
}
