using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earlmanager : MonoBehaviour
{
  public GameObject Gridmanager;
  public GameObject Earl;

  [SerializeField]
  public GameObject Earl_Eyes;
  [SerializeField]
  public GameObject Earl_Mouth;

  public int max_earls, earl_count;
  [SerializeField] private Vector3 grid_pos;

  private List<GameObject> earl_list = new List<GameObject>();
  public List<Earl_Egg> egg_list = new List<Earl_Egg>();
  private List<Vector3> earl_pos_list = new List<Vector3>();

  private List<string> eye_database = new List<string>()
    {
      "earl_eyes_0","earl_eyes_1","earl_eyes_2","earl_eyes_3",
      "earl_eyes_4","earl_eyes_5","earl_eyes_6","earl_eyes_7",
      "earl_eyes_8", "earl_eyes_9", "earl_eyes_10", "earl_eyes_cutesy", "earl_eyes_cutesy_1",
      "earl_eyes_cutesy_2","earl_eyes_cutesy_3","earl_eyes_cutesy_4",
      "earl_eyes_cutesy_5"
    };
  private List<string> mouth_database = new List<string>()
    {
      "earl_mouth_0","earl_mouth_1","earl_mouth_2","earl_mouth_3",
      "earl_mouth_4", /*"earl_mouth_5",*/ "earl_mouth_6", "earl_mouth_7",
      "earl_mouth_8", "Special Mouths/mouth_handlebar", "Special Mouths/mouth_stache"
    };
  private List<string> fur_database = new List<string>()
    {
      "earl_fur_0","earl_fur_1","earl_fur_2","earl_fur_3",
      "earl_fur_4", "earl_fur_5", "earl_fur_6"
    };
    // Start is called before the first frame update
    void Start()
    {
      Gridmanager = GameObject.FindWithTag("Grid");
      GameObject.FindWithTag("Level Controller").GetComponent<LevelController>().Set_Earlmanager(gameObject);

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

      foreach(Earl_Egg egg in egg_list)
      {
        egg.Update();

        if(egg.birth_ready)
        {
          Birth_Earl(egg, egg.color);

          return;
        }

      }

    }

    private void Birth_Earl(Earl_Egg egg, Color color)
    {
      earl_count += 1;

      GameObject birth_square = Gridmanager.GetComponent<gridmanager>().Get_Tile(egg.grid_pos);
      if (birth_square == null) return;

      Destroy(egg.phys_rep);
      egg_list.Remove(egg);
      egg.Delete();

      var new_earl = Instantiate(Earl, birth_square.transform.position, Quaternion.identity);
      new_earl.GetComponent<SpriteRenderer>().color = color;

      //Attach the face
      new_earl.GetComponent<earlbrain>().eyes = Instantiate(Earl_Eyes,new_earl.transform);
      new_earl.GetComponent<earlbrain>().mouth = Instantiate(Earl_Mouth,new_earl.transform);
      new_earl.GetComponent<earlbrain>().eyes.transform.position = new_earl.transform.position;
      new_earl.GetComponent<earlbrain>().mouth.transform.position = new_earl.transform.position;

      new_earl.GetComponent<earlbrain>().Set_Eyes(eye_database[UnityEngine.Random.Range(0,eye_database.Count)]);
      new_earl.GetComponent<earlbrain>().Set_Mouth(mouth_database[UnityEngine.Random.Range(0,mouth_database.Count)]);
      new_earl.GetComponent<earlbrain>().Set_Fur(fur_database[UnityEngine.Random.Range(0,fur_database.Count)]);

      new_earl.GetComponent<earlbrain>().Set_Grid_Pos(birth_square.GetComponent<tilebehavior>().grid_pos);
      Gridmanager.GetComponent<gridmanager>().Update_Square(birth_square.GetComponent<tilebehavior>().Get_Grid_Pos(),"add", new_earl);

      Debug.Log(new_earl.GetComponent<earlbrain>().mouth.GetComponent<SpriteRenderer>().sprite);
      earl_list.Add(new_earl);
    }

    private void Load_Earl(Dictionary<string,dynamic> earl_data)
    {


      GameObject target_square = Gridmanager.GetComponent<gridmanager>().Get_Tile(earl_data["grid_pos"]);
      if (target_square == null || ! target_square.GetComponent<tilebehavior>().Is_Empty()) return;

      earl_count += 1;



      var new_earl = Instantiate(Earl, target_square.transform.position, Quaternion.identity);


      //Attach the face
      new_earl.GetComponent<earlbrain>().eyes = Instantiate(Earl_Eyes,new_earl.transform);
      new_earl.GetComponent<earlbrain>().mouth = Instantiate(Earl_Mouth,new_earl.transform);
      new_earl.GetComponent<earlbrain>().eyes.transform.position = new_earl.transform.position;
      new_earl.GetComponent<earlbrain>().mouth.transform.position = new_earl.transform.position;

      new_earl.GetComponent<earlbrain>().Load_Data(earl_data);


      Gridmanager.GetComponent<gridmanager>().Update_Square(target_square.GetComponent<tilebehavior>().Get_Grid_Pos(),"add", new_earl);

      earl_list.Add(new_earl);
    }

    public void Save_Earls()
    {

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
