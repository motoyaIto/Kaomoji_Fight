using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Graphic))]
public class CursorHue : MonoBehaviour {

    Graphic img;
    Graphic Img
    {
        get
        {
            if (img == null)
                img = this.GetComponent<Graphic>();
            return img;
        }
    }

    Material Mat
    {
        get
        {
            if (Img.material == null || !Img.material.HasProperty("_Hue"))
            {
                Img.material = new Material(Shader.Find("Custom/Select/Cursor"));
            }
            return Img.material;
        }
    }

    [Range(0f, 360f)]
    public float hue = 0;

    void Update()
    {
        UpdateHue();
    }

    void UpdateHue()
    {
        ChangeHue();
        Mat.SetFloat("_Hue", hue);
    }

    // 色を変える
    public void ChangeHue(float val = 5)
    {
        hue += val;
        hue = hue % 360f;
    }
}
