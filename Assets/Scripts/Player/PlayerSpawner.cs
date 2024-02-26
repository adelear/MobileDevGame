using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] catPrefabs;

    private void Start()
    {
        PlayerCharacter.PlayerCharacters selectedCharacter = PlayerCharacter.Instance.Character;
        GameObject prefabToInstantiate = GetPrefabForCharacter(selectedCharacter);

        if (prefabToInstantiate != null)
        {
            Instantiate(prefabToInstantiate, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Prefab not found for the selected character: " + selectedCharacter.ToString());
        }
    }

    private GameObject GetPrefabForCharacter(PlayerCharacter.PlayerCharacters character)
    {
        // Iterate through the catPrefabs array to find the prefab that matches the selected character type 
        // If the character type matches the name of the prefab 
        foreach (GameObject prefab in catPrefabs)
        {
            if (character.ToString() == prefab.name)
            {
                return prefab;
            }
        }

        return null; // Return null if no matching prefab is found
    }
}
