﻿using System;
using RDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
// ReSharper disable InconsistentNaming

public class GameManager : MonoBehaviour
{
    public Text MainCountText;
    public Slider progressBar;
    
    private int targetCount = 33;
    private int tempCount;

    public GameObject resetButton;
    public Text buttonText;
    public Button mainButton;
    public float SecondsUntukVibarate;

    [HideInInspector]
    public int MainCount = 0;

    public bool isVibrate;
    public bool isSound;

    private AudioSource beepAudio;

    private AboveButtonsScript buttonSettingScript;

    public void updateCount()
    {
        buttonText.text = "+1";
        MainCount++;
        MainCountText.text = MainCount.ToString();
        
        tempCount++;
        
        progressBar.value++;
        if (tempCount == targetCount)
        {
            progressBar.value = 0;
            tempCount = 0;
        }


            PlayerPrefs.SetInt("mainCount", MainCount);

        if (isVibrate)
            hapticButton();
        
        if (isSound)
            beepAudio.Play();
    }

    private void hapticButton()
    {
        Vibration.Vibrate(45); //45ms
    }

    // Start is called before the first frame update
    void Start()
    {
        buttonSettingScript = GameObject.Find("NavPanelAbove").GetComponent<AboveButtonsScript>();
        beepAudio = GetComponent<AudioSource>();
        MainCount = PlayerPrefs.GetInt("mainCount");

        tempCount = MainCount;
        
        MainCountText.text = MainCount.ToString();

        if (MainCount > targetCount)
        {
            //TODO: tolak je target count tu nanti
        }
        else
        {
            progressBar.value = MainCount;
        }

            //setting
        isVibrate = buttonSettingScript.onIsVibrate;
        isSound = buttonSettingScript.onIsSound;
        
        // Debug.Log("From GameManager, isSound is " + isSound + ", isVibrate is " + isVibrate);

        if (MainCount == 0)
            buttonText.text = "MULA";
        else
            buttonText.text = "SAMBUNG";
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCount > 0)
            resetButton.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainButton.onClick.Invoke(); //simulate 'space' button seolah2 tekan button
        }
    }

    public void ResetAllPrefs()
    {
        throw new NotImplementedException("Future update");
    }
}
