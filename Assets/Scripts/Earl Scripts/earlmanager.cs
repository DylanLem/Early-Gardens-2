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

        Birth_Earl();
        Update_Earl_Pos_List();
    }

    private void Birth_Earl()
    {
      if(earl_count >= max_earls) return;

      earl_count += 1;

      GameObject birth_square = Gridmanager.GetComponent<gridmanager>().Find_Empty_Square();

      if (birth_square == null) return;

      var new_earl = Instantiate(Earl, birth_square.transform.position, Quaternion.identity);
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
