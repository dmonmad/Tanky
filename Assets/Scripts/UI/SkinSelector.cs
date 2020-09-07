using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public List<Button> SkinButtons;
    public List<Sprite> SkinTextures;
    public GameObject ContentParent;
    public GameObject SkinPrefab;
    public Material TankMaterial;
    public ScrollRect SkinScroller; 

    // Start is called before the first frame update
    void Start()
    {
        LoadSkins();
        LoadLocks();
    }

    private void LoadLocks()
    {
        if (SkinButtons.Count > 0)
        {
            for (int i = 0; i < SkinButtons.Count; i++)
            {
                SkinButton sb = SkinButtons[i].GetComponent<SkinButton>();
                if (IsSkinUnlocked(sb.GetTextureName()))
                {
                    SkinButtons[i].interactable = false;
                    sb.Lock();
                }
            }
        }
    }

    private void LoadSkins()
    {
        SkinScroller.enabled = false;

        bool isFirst = false;

        foreach (Sprite skin in SkinTextures)
        {
            GameObject skinObject = Instantiate(SkinPrefab, ContentParent.transform);

            SkinButton sb = skinObject.GetComponent<SkinButton>();
            sb.Initialize(skin, TankMaterial);

            SkinButtons.Add(skinObject.GetComponent<Button>());
        }

        SkinScroller.enabled = true;

        Canvas.ForceUpdateCanvases();

        SkinScroller.horizontalNormalizedPosition = 0;
    }

    private bool IsSkinUnlocked(string _skinName)
    {
        if(PlayerPrefs.GetInt(_skinName, 0) == 1)
        {
            return true;
        }

        return false;
    }
}
