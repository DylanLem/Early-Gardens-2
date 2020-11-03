using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_display : MonoBehaviour
{

    public GameObject InventorySlot;
    public GameObject borderhighlight;
    public GameObject[,] inventory_slots;
    public Item[,] inventory;

    void Awake()
    {

      Create_Inventory_Slots();
    }

    void Update()
    {

    }

    public void Create_Inventory_Slots()
    {
      inventory_slots = new GameObject[6,4];

      //our array of inventory slot game objects (slot gobs)
      GameObject[] slot_Gobs = GameObject.FindGameObjectsWithTag("Inventory Slot");


      foreach(GameObject slot in slot_Gobs)
      {
        int x = (int)Char.GetNumericValue(slot.name[slot.name.Length -1]);
        int y = (int)Char.GetNumericValue(slot.name[slot.name.Length -2]);


        inventory_slots[x,y] = slot;

        slot.GetComponent<inventory_slot>().inv_x = x;
        slot.GetComponent<inventory_slot>().inv_y = y;



        GameObject b_highlight = Instantiate(borderhighlight,inventory_slots[x,y].transform.parent);
        b_highlight.transform.position = inventory_slots[x,y].transform.position;
        b_highlight.transform.parent = inventory_slots[x,y].transform;
      }
    }

    public void Display_Inventory(Item[,] inventory)
    {

      for(int i = 0; i < inventory.GetLength(0); i++)
        for(int j = 0; j < inventory.GetLength(1); j++)
        {

          Item item = inventory[i,j];

          if(inventory_slots[i,j].transform.Find("Slot Border(Clone)").gameObject.GetComponent<SpriteRenderer>().sprite == Resources.Load<Sprite>("UI/borderhighlight"))
          {
            inventory_slots[i,j].transform.Find("Slot Border(Clone)").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("UI/bordernorm");
          }

          //break out of this loop if the item is null
          if(item == null) continue;


          inventory_slots[i,j].GetComponent<inventory_slot>().Add_To_Slot(item,item.phys_rep);

          inventory[i,j].phys_rep.transform.parent = inventory_slots[i,j].transform;
        }
    }

    public void Send_Slot_Pos(GameObject inv_slot)
    {
      for(int j = 0; j < 4; j++)
        for(int i = 0; i < 6; i++)
        {
          if(inventory_slots[i,j] == inv_slot)
          {
            GameObject.FindWithTag("Player").GetComponent<player>().held_index = new Vector3(i,j);
          }

        }
        Display_Inventory(inventory);
    }
}
