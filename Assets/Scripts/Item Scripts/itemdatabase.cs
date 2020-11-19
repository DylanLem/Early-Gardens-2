using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Itemdatabase
{
  public static Dictionary<int,Type> item_database;



  static void Build_Database()
  {

    item_database = new Dictionary<int,Type>();

    item_database.Add(0, typeof(CFood));
    item_database.Add(1, typeof(Apple));
    item_database.Add(2, typeof(Earl_Egg));
    item_database.Add(3, typeof(Seeds));
    item_database.Add(4, typeof(Plant));
    item_database.Add(5, typeof(Bricks));
    item_database.Add(6, typeof(Wall));
    item_database.Add(7, typeof(Stake));
    item_database.Add(8, typeof(Fence));
    item_database.Add(9, typeof(Box));
    item_database.Add(10, typeof(CardboardBox));
    item_database.Add(11, typeof(Bed));
    item_database.Add(12, typeof(Player_Bed));

  }

  public static Item Get_Item(int _id)
  {
    if(item_database == null) Build_Database();
    //Creating an instance of an item in our database. :)
    Item i = (Item)Activator.CreateInstance(item_database[_id]);
    i.Set_Id(_id);
    return i;
  }


  }
