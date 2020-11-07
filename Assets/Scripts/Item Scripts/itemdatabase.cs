using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Itemdatabase
{
  public List<Type> item_database;
  public int name;

  public Itemdatabase()
  {
    Build_Database();
    name = UnityEngine.Random.Range(0,10);
  }


  void Build_Database()
  {
    item_database = new List<Type>()
    {
      typeof(CFood),
      typeof(Apple),
      typeof(Earl_Egg),
      typeof(Seeds),
      typeof(Plant),
      typeof(Bricks),
      typeof(Wall),
      typeof(Stake),
      typeof(Fence)
    };
  }

  public Item Get_Item(int _id)
  {
    //Creating a copy of an item in our database. :)
    Item i = (Item)Activator.CreateInstance(item_database[_id]);
    return i;
  }


  }
