using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class player : MonoBehaviour
{
    public Item[,] inventory;


    public Item carried_object = null;
    public GameObject carried_physrep;

    public GameObject carried_earl = null;

    public GameObject Itemmanager;
    public GameObject InventoryDisplay;

    public Vector3 grid_pos;
    public Vector3 held_index;

    public bool is_sleeping;
    public bool has_action_icon;

    public int time_elapsed;

    void Awake()
    {
      inventory = new Item[6,4];
    }
    // Start is called before the first frame update
    void Start()
    {

        time_elapsed = 0;

        held_index = Vector3.zero;
        Itemmanager = GameObject.FindWithTag("Item Manager");

        InventoryDisplay = GameObject.FindWithTag("Inventory Display");
        InventoryDisplay.GetComponent<inventory_display>().inventory = inventory;
        Send_Inv_Data();

        SaveSystem.Load_Player(gameObject);
        if(GameObject.FindWithTag("Level Controller").GetComponent<LevelController>().on_startup == true)
        {
          Sleep();
          GameObject.FindWithTag("Level Controller").GetComponent<LevelController>().on_startup = false;
        }



        Toggle_Title_Display(is_sleeping);
    }

    // Update is called once per frame
    void Update()
    {
      //The only reason we dont check for this in playermanager
      //is cause i fucked it with the keyboard event system :(
      if(Input.GetKeyDown(KeyCode.Tab))
      {
        Cycle_Item();
      }

      if(is_sleeping){
        Send_Effect(gameObject, 0);
      }
    }

    public void Cycle_Item()
    {
      //You can't cycle thru the inventory if you are holding an earl.
      if (carried_earl != null) return;


      InventoryDisplay.GetComponent<inventory_display>().inventory_slots[(int)held_index.x,(int)held_index.y].
      GetComponent<inventory_slot>().Un_Highlight_Border();

      //Gotta store the held_index ;l
      if(held_index.x >= inventory.GetLength(0) - 1)
      {
        if(held_index.y >= inventory.GetLength(1) - 1)
          held_index.y = 0;
        else
          held_index.y += 1;

        held_index.x = 0;
      }
      else
        held_index.x += 1;

      carried_object = (Item)inventory[(int)held_index.x,(int)held_index.y];
      Display_Carried_Item();
      Send_Inv_Data();

      InventoryDisplay.GetComponent<inventory_display>().inventory_slots[(int)held_index.x,(int)held_index.y].
      GetComponent<inventory_slot>().Highlight_Border();
    }

    public bool Get_From_Inventory(int x, int y)
    {
      if(carried_earl != null) return false;

      carried_object = inventory[x,y];

      Display_Carried_Item();
      Send_Inv_Data();

      return true;
    }

    public void Remove_From_Inventory(int x, int y)
    {
      inventory[x,y] = null;
    }


    public void Sleep()
    {
      GameObject.FindWithTag("Player Manager").GetComponent<playermanager>().move_timer = -2.0f;
      is_sleeping = true;
      Toggle_Title_Display(true);
      Set_Sprite(Resources.Load<Sprite>("Player_sleeping"));
    }

    public void Wake_Up()
    {
      is_sleeping = false;
      Toggle_Title_Display(false);
      Set_Sprite(Resources.Load<Sprite>("Player_new"));
    }


    public void Set_Sprite(Sprite sprite)
    {
      GetComponent<SpriteRenderer>().sprite = sprite;
    }



    //Save System shite
    public Dictionary<string,dynamic> Pack_Data()
    {
      Dictionary<string,dynamic> player_data = new Dictionary<string,dynamic>();

      List<dynamic> inv_data = new List<dynamic>();

      for(int i = 0; i < inventory.GetLength(0); i++)
        for(int j = 0; j < inventory.GetLength(1); j++)
          {
            if(inventory[i,j] != null)
              inv_data.Add(inventory[i,j].Pack_Data());
          }

      player_data["inv_data"] = inv_data;

      if(carried_object != null)
        player_data["carried_object"] = carried_object.Pack_Data();
      else
        player_data["carried_object"] = null;

      return player_data;
    }

    public void Load_Data(Dictionary<string,dynamic> player_data)
    {
      foreach(Dictionary<string,dynamic> item_data in player_data["inv_data"])
      {
        Itemmanager.GetComponent<itemmanager>().Load_To_Inventory(item_data);
      }
      Itemmanager.GetComponent<itemmanager>().Load_To_Inventory(player_data["carried_object"]);
    }


}
