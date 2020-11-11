using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectManager
{
  // start the global effect timer at zero
  public static float time_elapsed = 0.0f;

  // add a database for effects
  public static List<Effect> effects = new List<Effect>();

  public static void create_effect(GameObject target, int effect_id)
  { // gotta finish

  }

  public static void Update(){
    // add 1.0 to time_elapsed every second
    time_elapsed += Time.deltaTime;

    if(time_elapsed >= 1000){
      time_elapsed = 0;
    }

    if((time_elapsed % 2) == 0){
      // create Zs when the player is sleeping
    }

  }

}
