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
    item_database = new Dictionary<int,Type>()
    {
      {0, typeof(CFood)},
      {1, typeof(Apple)},
      {2, typeof(Earl_Egg)},
      {3, typeof(Seeds)},
      {4, typeof(Plant)},
      {5, typeof(Bricks)},
      {6, typeof(Wall)},
      {7, typeof(Stake)},
      {8, typeof(Fence)}
    };
  }

  public Item Get_Item(int _id)
  {
    //Creating a copy of an item in our database. :)
    Item i = (Item)Activator.CreateInstance(item_database[_id]);
    i.Set_Id(_id);
    return i;
  }


  }
