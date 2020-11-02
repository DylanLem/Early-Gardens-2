using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class earldisplay : MonoBehaviour
{
    private GameObject mouth_rep, eyes_rep,bod_rep;
    [SerializeField]private Sprite empty_earl;
    [SerializeField]private GameObject earl_image = null;
    [SerializeField]private Text tEarl_name = null;
    [SerializeField]private Text tEarl_mood = null;
    [SerializeField]private Text tEarl_health = null;
    [SerializeField]private Text tEarl_satiety = null;

    // Start is called before the first frame update
    void Start()
    {

      bod_rep = new GameObject();
      bod_rep.AddComponent<SpriteRenderer>();
      bod_rep.transform.parent = earl_image.transform;
      bod_rep.transform.position = earl_image.transform.position;
      bod_rep.transform.localScale *= 2;
      bod_rep.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
      bod_rep.GetComponent<SpriteRenderer>().sortingOrder = 1;

      mouth_rep = new GameObject();
      mouth_rep.AddComponent<SpriteRenderer>();
      mouth_rep.transform.parent = earl_image.transform;
      mouth_rep.transform.position = earl_image.transform.position;
      mouth_rep.transform.localScale *= 2;
      mouth_rep.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
      mouth_rep.GetComponent<SpriteRenderer>().sortingOrder = 2;

      eyes_rep = new GameObject();
      eyes_rep.AddComponent<SpriteRenderer>();
      eyes_rep.transform.parent = earl_image.transform;
      eyes_rep.transform.position = earl_image.transform.position;
      eyes_rep.transform.localScale *= 2;
      eyes_rep.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
      eyes_rep.GetComponent<SpriteRenderer>().sortingOrder = 3;



      empty_earl = Resources.Load<Sprite>("UI/hud_inventory_slot");
      Clear_Info();
    }


    public void Display_Info(Dictionary<string,dynamic> earl_data)
    {
      //either enables or disables the earl display
      earl_image.SetActive(true);
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
      tEarl_health.text = "Health: " + (int)earl_data["health"];
      tEarl_satiety.text = "satiety: " + (int)earl_data["satiety"];

      bod_rep.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Fur/" + earl_data["sprite"]);
      eyes_rep.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Eyes/" + earl_data["eyes"]);
      mouth_rep.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Mouths/" + earl_data["mouth"]);

      bod_rep.GetComponent<SpriteRenderer>().color = new Color(earl_data["color"][0],earl_data["color"][1],earl_data["color"][2]);
    }

    public void Clear_Info()
    {
      earl_image.SetActive(false);
      tEarl_name.enabled = false;
      tEarl_mood.enabled = false;
      tEarl_health.enabled = false;
      tEarl_satiety.enabled = false;

    }

}
