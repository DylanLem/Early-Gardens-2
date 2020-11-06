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
    if(carried_object == null) return;

    carried_object.phys_rep.transform.localScale = new Vector3(0.5f,0.5f);
    carried_object.phys_rep.transform.parent = transform;
    carried_object.phys_rep.transform.position = transform.position;
    carried_object.phys_rep.GetComponent<SpriteRenderer>().sortingLayerName = "Grid";
    carried_object.phys_rep.GetComponent<SpriteRenderer>().sortingOrder = 2;
  }

  public void Toggle_Title_Display(bool on_off)
  {
    GameObject.FindWithTag("Title Screen").SetActive(on_off);
  }

}
