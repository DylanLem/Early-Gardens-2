using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{

  public GameObject phys_rep;
  public Vector3 local_origin = Vector3.zero;
  public Sprite sprite;

  public Effect(GameObject parent = null)
  {
    phys_rep = new GameObject();
    phys_rep.AddComponent<SpriteRenderer>();

    phys_rep.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
    phys_rep.GetComponent<SpriteRenderer>().sortingOrder = -1;

  }

  public void Set_Sprite(Sprite sprite)
  {

    phys_rep.GetComponent<SpriteRenderer>().sprite = sprite;
  }

  public void Set_Parent(GameObject gobj)
  {
    if(gobj == null) return;

    Debug.Log("HEY");

    phys_rep.transform.position = gobj.transform.position;
    local_origin = phys_rep.transform.position;
  }


  public virtual void Update()
  {}

  public virtual void Animate()
  {}

}
