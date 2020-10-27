using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Plant : Building
{

  //how many stages of growth does this plant have?
  public int growth_stages;

  //how much fruit it produces
  public float fertility;

  //how long it takes for each stage
  public float grow_time;



  public void Grow()
  {

  }

  public void Bear_Fruit()
  {

  }

  public void Die()
  {

  }


}
