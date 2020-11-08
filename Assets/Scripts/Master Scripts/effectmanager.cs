using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectManager
{
  // start the global effect timer at zero
  public float time_elapsed = 0.0;

  // add a database for effects

  public void create_effect(GameObject target){ // gotta finish

  }

  public void Update(){
    // add 1.0 to time_elapsed every second
    time_elapsed += time.deltaTime;

    if(time_elapsed >= 1000){
      time_elapsed = 0;
    }

    if((time_elapsed % 2) == 0){
      // create Zs when the player is sleeping
    }

  }

}
