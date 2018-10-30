using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest : MonoBehaviour {

    private Slider slider;

    private float hp;

	// Use this for initialization
	void Start () {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.maxValue = 10f;
        hp = slider.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
        hp -= .01f;

        if (hp < slider.minValue)
        {
            hp = slider.maxValue;
        }

        slider.value = hp;
	}
}
