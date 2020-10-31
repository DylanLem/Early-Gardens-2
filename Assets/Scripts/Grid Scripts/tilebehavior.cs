using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilebehavior : MonoBehaviour
{

    public bool is_empty;
    public GameObject contains {get;set;}
    public Vector3 grid_pos;
    // Start is called before the first frame update


    void Awake()
    {
      is_empty = true;
    }

    public void Set_Sprite(Sprite s)
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = s;
    }
    //could've used a get/set property but u live and learn baby
    public bool Is_Empty()
    {
      return is_empty;
    }

    public void Set_Empty(bool state)
    {
      is_empty = state;
    }

    public Vector3 Get_Grid_Pos()
    {
      return grid_pos;
    }

    public bool Add_To_Tile(GameObject obj)
    {

      if(! is_empty) return false;

      contains = obj;

      obj.transform.parent = transform;
      obj.transform.localScale = Vector3.one;
      obj.transform.position = transform.position;


      is_empty = false;
      GameObject.FindWithTag("Grid").GetComponent<gridmanager>().Set_Empty_Squares();

      return true;
    }

    public GameObject Remove_From_Tile()
    {

      if(is_empty) return null;

      var obj = contains;


      contains = null;
      is_empty = true;

      GameObject.FindWithTag("Grid").GetComponent<gridmanager>().Set_Empty_Squares();

      return obj;
    }
    public GameObject Get_Contents()
    {
      return contains;
    }
}
