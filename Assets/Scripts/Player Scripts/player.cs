using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class player : MonoBehaviour
{
    public Item[,] inventory;
    public Item carried_object = null;
    public GameObject carried_earl = null;

    public GameObject Itemmanager;
    public GameObject InventoryDisplay;


    public Vector3 held_index;

    void Awake()
    {
      inventory = new Item[6,4];
    }
    // Start is called before the first frame update
    void Start()
    {


        held_index = Vector3.zero;
        Itemmanager = GameObject.FindWithTag("Item Manager");

        InventoryDisplay = GameObject.FindWithTag("Inventory Display");
        InventoryDisplay.GetComponent<inventory_display>().inventory = inventory;
        Send_Inv_Data();

        SaveSystem.Load_Player(gameObject);
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
    }

    public void Cycle_Item()
    {
      //You can't cycle thru the inventory if you are holding an earl.
      if (carried_earl != null) return;
      Item temp_item = (Item)inventory.GetValue((int)held_index.x,(int)held_index.y);
      inventory[(int)held_index.x,(int)held_index.y] = null;


      if(carried_object != null) Itemmanager.GetComponent<itemmanager>().Add_To_Inventory(carried_object);
      carried_object = temp_item;
      Display_Carried_Item();
      Send_Inv_Data();

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

      InventoryDisplay.GetComponent<inventory_display>().inventory_slots[(int)held_index.x,(int)held_index.y].
      GetComponent<inventory_slot>().Highlight_Border();
    }

    public bool Get_From_Inventory(int x, int y)
    {
      if(carried_earl != null) return false;

      Item temp = carried_object;
      carried_object = inventory[x,y];
      inventory[x,y] = temp;
      Display_Carried_Item();
      Send_Inv_Data();

      return true;
    }


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
