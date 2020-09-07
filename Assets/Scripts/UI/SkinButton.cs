using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public Material TankMaterial;
    private Texture texture;
    public GameObject LockObject;
    public GameObject LockBackground;


    public void SetTexture()
    {
        TankMaterial.mainTexture = texture;
    }

    internal void Initialize(Sprite _skin, Material _tankMaterial)
    {
        TankMaterial = _tankMaterial;

        Image skinHolder = GetComponent<Image>();
        skinHolder.sprite = _skin;
        texture = _skin.texture;
    }

    public string GetTextureName()
    {
        return texture.name;
    }

    internal void Lock()
    {
        LockBackground.SetActive(true);
        LockObject.SetActive(true);
    }

    public void ClearSkin()
    {
        TankMaterial.mainTexture = null;
    }
}
