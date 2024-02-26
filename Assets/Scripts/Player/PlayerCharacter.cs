using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter
{
    private static PlayerCharacter instance;

    public enum PlayerCharacters
    {
        BlackCat,
        CalicoCat,
        WhiteCat,
        OrangeTabby,
        GreyTabby,
        BritishShorthair
    }

    public PlayerCharacters Character { get; private set; } = PlayerCharacters.BlackCat;


    public static PlayerCharacter Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerCharacter();
            }
            return instance;
        }
    }

    private PlayerCharacter() { } // Private constructor to prevent instantiation outside the class


    public void SetCharacter(PlayerCharacters character)
    {
        Character = character;
    }
}
