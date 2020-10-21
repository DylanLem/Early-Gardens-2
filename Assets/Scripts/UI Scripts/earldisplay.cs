using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class earldisplay : MonoBehaviour
{
    [SerializeField]private Sprite empty_earl;
    [SerializeField]private Image earl_image = null;
    [SerializeField]private Text tEarl_name = null;
    [SerializeField]private Text tEarl_mood = null;
    [SerializeField]private Text tEarl_health = null;
    [SerializeField]private Text tEarl_satiety = null;

    // Start is called before the first frame update
    void Start()
    {
      empty_earl = Resources.Load<Sprite>("UI/hud_inventory_slot");
      Clear_Info();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Display_Info(Dictionary<string,dynamic> earl_data)
    {
      //either enables or disables the earl display
      Parse_Earl_Data(earl_data);

      tEarl_name.enabled = true;
      tEarl_mood.enabled = true;
      tEarl_health.enabled = true;
      tEarl_satiety.enabled = true;
    }

    private void Parse_Earl_Data(Dictionary<string,dynamic> earl_data)
    {
      //earl data is packed and sent from an earl in a specialized dictionary
      tEarl_name.text = earl_data["name"];
      tEarl_mood.text = "Current Mood: " + "\n" + earl_data["mood"];
      tEarl_health.text = "Health: " + earl_data["health"];
      tEarl_satiety.text = "satiety: " + earl_data["satiety"];
      earl_image.sprite = earl_data["sprite"];
      earl_image.color = earl_data["color"];
    }

    public void Clear_Info()
    {
      earl_image.sprite = empty_earl;
      earl_image.color = Color.white;
      tEarl_name.enabled = false;
      tEarl_mood.enabled = false;
      tEarl_health.enabled = false;
      tEarl_satiety.enabled = false;

    }

}
