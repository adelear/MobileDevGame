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

    public void SetOwnedStatus(bool newStatus)
    {
        isOwned = newStatus;
        SaveOwnedStatus();
    }

    private void SaveOwnedStatus()
    {
        if (isOwned)
        {
            PlayerPrefs.SetInt("CatOwned_" + catName, 1);
        }
        else
        {
            PlayerPrefs.SetInt("CatOwned_" + catName, 0);
        }
        PlayerPrefs.Save();
    }
}
