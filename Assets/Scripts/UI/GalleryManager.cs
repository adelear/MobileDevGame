using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button previousButton;
    [SerializeField] Button nextButton;
    [SerializeField] Button selectCatButton;

    [Header("Images")]
    [SerializeField] Image catPortrait;

    [Header("Cat Information")]
    [SerializeField] CatData[] catData;
    [SerializeField] CatData[] ownedCats;

    [Header("Text")]
    [SerializeField] TMP_Text catName;

    private int currentCat = 0;
    private int ownedCatNum = 0; 

    private void Start()
    {
        CatEvents.OnOwnedCatNumValueChanged += UpdateGallery;
        
        ownedCatNum = GetOwnedCatCount();
        ownedCats = new CatData[catData.Length];
        UpdateGallery(); // Update the gallery initially

        if (previousButton != null)
        {
            previousButton.onClick.AddListener(OnPrevButtonPressed);
        }

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonPressed);
        }
    }

    private void OnDestroy()
    {
        CatEvents.OnOwnedCatNumValueChanged -= UpdateGallery;
    }

    private void UpdateGallery()
    {
        ownedCatNum = GetOwnedCatCount(); 
        ownedCats = new CatData[catData.Length];

        // Populate the array with owned cats
        int count = 0;
        for (int i = 0; i < catData.Length; i++)
        {
            if (catData[i].isOwned)
            {
                ownedCats[count] = catData[i];
                count++;
            }
        }

        ShowCurrentCat();
    }

    private void ShowCurrentCat()
    {
        if (currentCat >= 0 && currentCat < ownedCats.Length && ownedCats[currentCat] != null)
        {
            CatData currentCatData = ownedCats[currentCat];
            catPortrait.sprite = currentCatData.portraitRegular;
            catName.text = currentCatData.catName;
        }
    }

    private int GetOwnedCatCount()
    {
        int count = 0;
        foreach (var cat in catData)
        {
            if (cat.isOwned)
            {
                count++;
            }
        }
        return count;
    }

    public void OnNextButtonPressed()
    {
        currentCat++;
        if (currentCat >= ownedCatNum)
        {
            currentCat = 0;
        }
        ShowCurrentCat();
    }

    public void OnPrevButtonPressed()
    {
        currentCat--;
        if (currentCat < 0)
        {
            currentCat = ownedCatNum - 1;
        }
        ShowCurrentCat();
    }
}
