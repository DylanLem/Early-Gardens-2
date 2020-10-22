using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earlmanager : MonoBehaviour
{
  public GameObject Gridmanager;
  public GameObject Earl;

  public int max_earls, earl_count;
  [SerializeField] private Vector3 grid_pos;

  private List<GameObject> earl_list = new List<GameObject>();
  public List<Earl_Egg> egg_list = new List<Earl_Egg>();
  private List<Vector3> earl_pos_list = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
      Gridmanager = GameObject.FindWithTag("Grid");

      max_earls = 0;

    }


    // Update is called once per frame
    void Update()
    {
      Check_Earl_Eggs();

        Update_Earl_Pos_List();
    }

    private void Check_Earl_Eggs()
    {
      Earl_Egg current_egg = null;

      foreach(Earl_Egg egg in egg_list)
      {
        egg.Update();

        if(egg.birth_ready)
        {
          Birth_Earl(egg, egg.color);
          egg_list.Remove(egg);
          return;
        }

      }

    }

    private void Birth_Earl(Earl_Egg egg, Color color)
    {
      earl_count += 1;

      Destroy(egg.phys_rep);

      GameObject birth_square = Gridmanager.GetComponent<gridmanager>().Get_Tile(egg.grid_pos);



      if (birth_square == null) return;

      var new_earl = Instantiate(Earl, birth_square.transform.position, Quaternion.identity);
      new_earl.GetComponent<SpriteRenderer>().color = color;

      new_earl.GetComponent<earlbrain>().Set_Grid_Pos(birth_square.GetComponent<tilebehavior>().grid_pos);
      Gridmanager.GetComponent<gridmanager>().Update_Square(birth_square.GetComponent<tilebehavior>().Get_Grid_Pos(),"add", new_earl);

      earl_list.Add(new_earl);
    }


    private void Update_Earl_Pos_List()
    {
      earl_pos_list.Clear();
      foreach(GameObject earl in earl_list)
      {
        earl_pos_list.Add(earl.GetComponent<earlbrain>().Get_Grid_Pos());
      }
    }

    public List<Vector3> Get_Earl_Positions()
    {
      return earl_pos_list;
    }

    public List<GameObject> Get_Earl_List()
    {
      return earl_list;
    }
}
