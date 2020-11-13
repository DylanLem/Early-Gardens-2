using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectManager
{
  public static EffectDatabase ED = new EffectDatabase();

  // start the global effect timer at zero
  public static float time_elapsed = 0.0f;

  // add a database for effects
  public static List<Effect> effects = new List<Effect>();
  public static List<Effect> culled_effects;

  public static void CreateEffect(GameObject target, int effect_id)
  {

    //effect buffer
    if(((int)(time_elapsed * 100 % 100)) == 0)
      effects.Add(ED.Get_Effect(effect_id, target));
  }

  public static void Update(){

    culled_effects = new List<Effect>();

    // add 1.0 to time_elapsed every second
    time_elapsed += Time.deltaTime;

    if(time_elapsed >= 1000){
      time_elapsed = 0;
    }

    foreach(Effect e in effects)
    {
      e.Update();
    }

    foreach(Effect e in culled_effects)
    {
      effects.Remove(e);
    }

  }

}
