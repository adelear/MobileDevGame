using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button[] catButtons;
    [SerializeField] Button nextButton;
    [SerializeField] Button prevButton;
    [SerializeField] Button purchaseButton;
    [SerializeField] Button ownedButton; 

    [SerializeField] CatData[] catData;
    [SerializeField] Image[] catPortraits; 


    private int currentPage = 0;
    private int catsPerPage = 6;
    private int currentlySelectedIndex = -1; 

    // Cat that is currently selected will make a happy face 
    // Cat that is currently selected, when pressed purchase, check for the current coins players has 
    // Use Scriptable Objects, that extracts the data like the cat's price, and their two portraits. 
    // If player already has cat in their gallery, Cat will have a collar on it, and player's "purchase" button will be owned when the cat is selected



    // I want each button to take data from scriptableobject cat data 
    // There's only 6 catbuttons that show up each page
    // So when player pressent next button, the 6 buttons are now replaced with the next 6 catData stuff 
    private void Start()
    {
        ShowCurrentPage();
        ownedButton.interactable = false; 
    }

    private void ShowCurrentPage()
    {
        int startIndex = currentPage * catsPerPage;

        for (int i = 0; i < catsPerPage; i++)
        {
            int dataIndex = startIndex + i;

            if (dataIndex < catData.Length)
            {
                catButtons[i].gameObject.SetActive(true);

                // Setting button images based on the current page and ownership status
                CatData currentCat = catData[dataIndex];

                if (currentCat.isOwned)
                {
                    // Change the sprite of the corresponding cat portrait image
                    catPortraits[i].sprite = currentCat.portraitOwned;
                }
                else
                {
                    catPortraits[i].sprite = currentCat.portraitSelected;
                    purchaseButton.gameObject.SetActive(true);
                    ownedButton.gameObject.SetActive(false);
                }

                catButtons[i].onClick.RemoveAllListeners();
                catButtons[i].onClick.AddListener(() => OnCatButtonPressed(currentCat));
            }
            else
            {
                catButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnNextButtonPressed()
    {
        currentPage++;
        if (currentPage >= Mathf.CeilToInt((float)catData.Length / catsPerPage))
        {
            currentPage = 0;
        }
        ShowCurrentPage();
    }

    public void OnPrevButtonPressed()
    {
        currentPage--;
        if (currentPage < 0)
        {
            currentPage = Mathf.CeilToInt((float)catData.Length / catsPerPage) - 1;
        }
        ShowCurrentPage();
    }

    private void OnCatButtonPressed(CatData cat)
    {
        Debug.Log("Selected cat: " + cat.name);
        DeselectCat(); // Deselect the previously selected cat

        int catIndex = System.Array.IndexOf(catData, cat);
        currentlySelectedIndex = catIndex; 
        if (catIndex >= 0 && catIndex < catPortraits.Length)
        {
            catPortraits[catIndex].sprite = cat.portraitRegular;
        }

        if (cat.isOwned)
        {
            purchaseButton.gameObject.SetActive(false);
            ownedButton.gameObject.SetActive(true); 
        }
        else
        {
            purchaseButton.gameObject.SetActive(true);
            ownedButton.gameObject.SetActive(false);
        }
    }


    private void DeselectCat()
    {
        // Deselect the previously selected cat (if any)
        if (currentlySelectedIndex >= 0 && currentlySelectedIndex < catPortraits.Length)
        {
            CatData deselectedCat = catData[currentlySelectedIndex];

            if (deselectedCat.isOwned)
            {
                catPortraits[currentlySelectedIndex].sprite = deselectedCat.portraitOwned;
            }
            else
            {
                catPortraits[currentlySelectedIndex].sprite = deselectedCat.portraitSelected;
            }
        }
    }

}
