using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{

  public GameObject phys_rep;
  public Vector3 local_origin = Vector3.zero;

  public Effect(Sprite sprite,GameObject parent = null)
  {
    phys_rep = new GameObject();
    phys_rep.AddComponent<SpriteRenderer>();

    Set_Sprite(sprite);
    Set_Parent(parent);
    Animate();
  }

  public void Set_Sprite(Sprite sprite)
  {
    phys_rep.GetComponent<SpriteRenderer>().sprite = sprite;
  }

  public void Set_Parent(GameObject gobj)
  {
    if(gobj == null) return;

    phys_rep.transform.parent = gobj.transform;
    phys_rep.transform.position = gobj.transform.position;
    local_origin = phys_rep.transform.position;
  }
  public virtual void Animate()
  {

  }

}
