using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_slot : MonoBehaviour
{

    public Item child_item;
    private GameObject border;

    public int inv_x, inv_y;

    void Start()
    {
       border = transform.Find("Slot Border(Clone)").gameObject;
    }

    void OnMouseEnter()
    {

      GameObject.FindWithTag("Inventory Display").GetComponent<inventory_display>().Send_Slot_Pos(gameObject);
      Highlight_Border();
    }

    void OnMouseExit()
    {
      Un_Highlight_Border();
    }

    void OnMouseDown()
    {
      GameObject.FindWithTag("Player").GetComponent<player>().Get_From_Inventory(inv_x,inv_y);

    }

    public void Add_To_Slot(Item item, GameObject phys_rep)
    {
      //used for adding an item to this slot

      phys_rep.transform.localScale = new Vector3(0.75f,0.75f);
      phys_rep.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
      phys_rep.GetComponent<SpriteRenderer>().sortingOrder = 2;

      phys_rep.transform.position = transform.position;

      phys_rep.transform.parent = null;
      phys_rep.transform.parent = transform;


      child_item = item;
    }

    public void Highlight_Border()
    {
      border.GetComponent<border_logic>().Highlight_Border();
    }

    public void Un_Highlight_Border()
    {
      border.GetComponent<border_logic>().Un_Highlight_Border();
    }

}
