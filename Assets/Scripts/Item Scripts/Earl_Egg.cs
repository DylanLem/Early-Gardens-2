using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Earl_Egg : Item
{
  List<Earl_Egg> Eggmanager;
  public Color color;



  public float egg_timer;
  public float incubation;

  public bool birth_ready;

  public Earl_Egg()
  {
    name = "Egg?";
    description = "What the heck even is that?";
    tag = "Egg";
    sprites = new Sprite[]
    {Resources.Load<Sprite>("items/egg_0")};
    initial_sprite = sprites[0];

    //All egg behaviour will be managed here :)
    Eggmanager = GameObject.Find("Earl Manager").GetComponent<earlmanager>().egg_list;

    color = new Color(UnityEngine.Random.Range(0,1.0f),UnityEngine.Random.Range(0,1.0f),UnityEngine.Random.Range(0,1.0f));

    egg_timer = 0f;
    incubation = 2f;

    birth_ready = false;

    Set_Phys_Rep(color);
    Eggmanager.Add(this);
  }

  public void Update()
  {
    if(is_placed)
    {
      egg_timer += Time.deltaTime;
    }

    if(egg_timer >= incubation)
    {
      birth_ready = true;
    }

  }

}
