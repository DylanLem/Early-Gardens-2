using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Itemdatabase
{
  public Dictionary<int,Type> item_database;
  public int name;

  public Itemdatabase()
  {
    Build_Database();
    name = UnityEngine.Random.Range(0,10);
  }


  void Build_Database()
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

  }

  public Item Get_Item(int _id)
  {
    //Creating a copy of an item in our database. :)
    Item i = (Item)Activator.CreateInstance(item_database[_id]);
    i.Set_Id(_id);
    return i;
  }


  }
