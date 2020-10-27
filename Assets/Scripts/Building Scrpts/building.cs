using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Building : Item
{

  public Building()
  {
    is_building = true;
    is_pushable = false;
  }
  // If your building has some sort of autonomy, give it an Update() function
  public virtual void Update()
  {

  }



}
