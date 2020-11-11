using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_z : Effect
{
  //the type of animation the effect will do
  public string type;

  public float time_elapsed;

  public Sprite sprite = Resources.Load<Sprite>("Effects/effect_z");

  public Effect_z(Sprite sprite, GameObject parent = null) : base(parent)
  {

  }

  public override void Update(){
    // time time_elapsed is how many seconds since the effect was created
    time_elapsed += Time.deltaTime;
    // destroy the effect after 2 seconds
    if(time_elapsed >= 2){
      GameObject.Destroy(phys_rep);
    }

    // animate the effect
    Animate();
  }

  public override void Animate(){

    phys_rep.transform.localPosition += new Vector3(0,Time.deltaTime * 1,0);

    //makes a cool little wave
    phys_rep.transform.localPosition += new Vector3(Mathf.Sin(phys_rep.transform.localPosition.y),0,0);
  }



}
