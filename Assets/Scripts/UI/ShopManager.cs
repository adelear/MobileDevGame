using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro; 

[RequireComponent(typeof(CanvasManager))]
public class ShopManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button[] catButtons;
    [SerializeField] Image[] catPortraits;
    [SerializeField] Sprite[] catSpritesPressed;
    [SerializeField] Sprite[] catSpritesRegular; 

    // Cat that is currently selected will make a happy face 
    // Cat that is currently selected, when pressed purchase, check for the current coins players has 
    // Use Scriptable Objects, that extracts the data like the cat's price, and their two portraits. 
    // If player already has cat in their gallery, Cat will have a collar on it, and player's "purchase" button will be owned when the cat is selected
    
    
    
    
}
