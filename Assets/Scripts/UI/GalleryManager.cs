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
        ownedCats = new CatData[catData.Length];

        for (int i = 0; i < catData.Length; i++)
        {
            if (catData[i].isOwned)
            {
                ownedCats[ownedCatNum] = catData[i];
                ownedCatNum++;
                Debug.Log($"Owned Cat: {catData[i].name}");
            }
        }

        Debug.Log($"Total Owned Cats: {ownedCatNum}");

        ShowCurrentCat();

        // Set up button click events
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(OnPrevButtonPressed);
        }

        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonPressed);
        }
    }



    private void ShowCurrentCat()
    {
        if (currentCat>=0 && currentCat < ownedCatNum)
        {
            CatData currentCatData = ownedCats[currentCat];
            catPortrait.sprite = currentCatData.portraitRegular;
            catName.text = currentCatData.catName;
        }
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
