using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_z : Effect
{
  //the type of animation the effect will do
  public string type;

  public float time_elapsed;

  public Sprite sprite = Resources.Load<Sprite>("Effects/effect_z");

  public effect_z(GameObject parent = null) : base(sprite, parent){

  }

  public void animate(){
    phys_rep.transform.localPosition.y += time.deltaTime * 1;
    phys_rep.transform.localPosition.y = Mathf.Sin(phys_rep.transform.localPosition.y);
  }



  public override void Update(){
    // time time_elapsed is how many seconds since the effect was created
    time_elapsed += time.deltaTime;
    // destroy the effect after 2 seconds
    if(time_elapsed >= 2){
      phys_rep.Destroy();
    }

    // animate the effect
    animate();
  }



}
