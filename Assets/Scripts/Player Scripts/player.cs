using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class player : MonoBehaviour
{
    public Item[,] inventory;
    public Item carried_object = null;
    public GameObject carried_earl = null;

    public GameObject Itemmanager;
    public GameObject InventoryDisplay;


    public Vector3 held_index;

    void Awake()
    {
      inventory = new Item[6,4];
    }
    // Start is called before the first frame update
    void Start()
    {
        held_index = Vector3.zero;
        Itemmanager = Instantiate(Itemmanager);
        InventoryDisplay = GameObject.FindWithTag("Inventory Display");
        Send_Inv_Data();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Tab))
      {
        Cycle_Item();
      }
    }

    public void Cycle_Item()
    {
      //You can't cycle thru the inventory if you are holding an earl.
      if (carried_earl != null) return;

      //Gotta store the held_index ;l
      if(held_index.x >= inventory.GetLength(0) - 1)
      {
        if(held_index.y >= inventory.GetLength(1) - 1)
          held_index.y = 0;
        else
          held_index.y += 1;

        held_index.x = 0;
      }
      else
        held_index.x += 1;

      Debug.Log(held_index);

      if(carried_object != null) Itemmanager.GetComponent<itemmanager>().Add_To_Inventory(carried_object);
      carried_object = inventory[(int)held_index.x,(int)held_index.y];

    }


}
