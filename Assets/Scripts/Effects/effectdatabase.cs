using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectDatabase
{
  public Dictionary<int,Type> effect_database;

  public EffectDatabase()
  {
    Build_Database();
  }


  void Build_Database()
  {
    effect_database = new Dictionary<int,Type>();

    effect_database.Add(0,typeof(Effect_Z));

  }

  public Effect Get_Effect(int _id, GameObject parent = null)
  {
    //Creating an instance of an effect in our database. :)
    Effect e = (Effect)Activator.CreateInstance(effect_database[_id], new object[] {parent});
    return e;
  }


  }
