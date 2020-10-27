using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Itemdatabase
{
  List<Type> item_database;

  public Itemdatabase()
  {
    Build_Database();
  }


  void Build_Database()
  {
    item_database = new List<Type>()
    {
      typeof(CFood),
      typeof(Apple),
      typeof(Earl_Egg),
      typeof(Seeds)
    };
  }

  public Item Get_Item(int _id)
  {
    //Creating a copy of an item in our database. :)
    Item i = (Item)Activator.CreateInstance(item_database[_id]);
    return i;
  }


  }
