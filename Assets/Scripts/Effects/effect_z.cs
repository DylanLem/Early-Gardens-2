using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Z : Effect
{
  //the type of animation the effect will do
  public string type;

  public float time_elapsed;

 public Sprite sprite = Resources.Load<Sprite>("Effects/effect_z");

  public Effect_Z(GameObject parent = null) : base(parent)
  {

    Set_Sprite(sprite);
    Set_Parent(parent);

    phys_rep.transform.localPosition += new Vector3(0.1f,0.2f,0);
  }

  public override void Update(){
    // time time_elapsed is how many seconds since the effect was created
    time_elapsed += Time.deltaTime;
    // destroy the effect after 2 seconds
    if(time_elapsed >= 3){
      GameObject.Destroy(phys_rep);
      EffectManager.culled_effects.Add(this);
    }

    // animate the effect
    Animate();
  }

  public override void Animate(){

    //move y
    phys_rep.transform.localPosition += new Vector3(0,Time.deltaTime * 0.75f,0);

    //makes a cool little wave
    phys_rep.transform.localPosition += new Vector3((1 * Mathf.Sin(phys_rep.transform.localPosition.y / 0.5f) * 0.01f),0,0);
  }



}
