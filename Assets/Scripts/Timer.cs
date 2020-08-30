using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour

{    
	private float currentTime;
	private float startTime =0;
	private Text theText;
// Start is called before the first frame update
    void Start()
    {
    	theText = GetComponent<Text>();
    	currentTime = startTime;
        
    }

    // Update is called once per frame
    void Update()
    {
    	currentTime += Time.deltaTime;
        decimal timeDisplay = Decimal.Round(Convert.ToDecimal(currentTime),1);
        theText.text = "Time: " + timeDisplay + " sec";
    }
}
