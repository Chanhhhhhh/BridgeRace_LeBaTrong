using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class ColorObject : MonoBehaviour
{
    public ColorType colorType;
    [SerializeField] private Renderer rendere;


    public void changColor(ColorType colorType)
    {
        this.colorType = colorType;
        rendere.material = LevelManager.Instance.colordata.getMaterial(colorType);
    }
}
