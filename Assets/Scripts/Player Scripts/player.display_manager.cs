using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class player : MonoBehaviour
{



  public void Send_Inv_Data()
  {
    InventoryDisplay.GetComponent<inventory_display>().Display_Inventory(inventory);
  }

  public void Display_Carried_Item()
  {

    Destroy(carried_physrep);

    if(carried_object == null) return;

    carried_physrep = Instantiate(carried_object.phys_rep);

    carried_physrep.transform.localScale = new Vector3(0.5f,0.5f);
    carried_physrep.transform.parent = transform;
    carried_physrep.transform.position = transform.position;
    carried_physrep.GetComponent<SpriteRenderer>().sortingLayerName = "Grid";
    carried_physrep.GetComponent<SpriteRenderer>().sortingOrder = 2;
  }

  public void Toggle_Title_Display(bool on_off)
  {
    GameObject.FindWithTag("Title Screen").GetComponent<TitleManager>().Toggle_Title_Display(on_off);
  }

  public void Send_Effect(GameObject parent,int effect_id)
  {
    EffectManager.CreateEffect(parent, effect_id);
  }

}
