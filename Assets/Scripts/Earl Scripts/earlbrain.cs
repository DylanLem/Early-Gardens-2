using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class earlbrain : MonoBehaviour
{

    public GameObject eyes, mouth;

    private Vector3 grid_pos;
    private GameObject Gridmanager;
    private GameObject Itemmanager;

    private Moods mood;
    public enum Moods
    {
      Idle,
      Carried,
      Hungry,
      Angry,
      Friendly,
      Eating,
      Sleeping,
      Starving,
      Dying
    }

    private float act_timer;
    private float speed;
    private Color color;
    private float satiety = 10f;
    private float health = 100f;
    private float attitude = 100f;
    private float metabolism = 0.8f;
    private bool is_running = false;
    private Item target;
    /* *** NOTES ***
    earls are smart and stupid. figure it outs
    */

    // Start is called before the first frame update
    void Start()
    {

      Gridmanager = GameObject.FindWithTag("Grid");
      Itemmanager = GameObject.FindWithTag("Item Manager");

      mood = Moods.Idle;
      act_timer = 0.0f;
      //lower speed = faster
      speed = 0.8f;

    }

    // Update is called once per frame
    void Update()
    {
      //EARL's have a fully functional metabolic system.
      //Fun Fact: if EARL's couldn't metabolise food, not only
      //would they starve, but they also wouldn't be able to LEVEL UP
      if(satiety>0) satiety -= metabolism * Time.deltaTime;

      mood = Determine_Mood();
      Determine_Action();
    }



    //What's a get/set property? I clearly have NO idea
    public void Set_Grid_Pos(Vector3 pos)
    {
      grid_pos = pos;
    }

    public Vector3 Get_Grid_Pos()
    {
      return grid_pos;
    }


    //If earl gets scooped by me
    public void Become_Carried()
    {
      mood = Moods.Carried;
      transform.localScale = new Vector3(0.5f,0.5f,0.5f);
      Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"remove");
      gameObject.transform.position = GameObject.FindWithTag("Player").transform.position;
      gameObject.transform.parent = GameObject.FindWithTag("Player").transform;
      Display_Earl_Data();
    }

    public void Become_Placed(Vector3 new_pos)
    {
      act_timer = 0;
      Set_Grid_Pos(new_pos);

      //fullsize this earl, okay?
      transform.localScale = new Vector3(1,1,1);
      //he's gonna be idle for a bit, okay?
      mood = Moods.Idle;

      Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"add",gameObject);
      transform.position = Gridmanager.GetComponent<gridmanager>().Grid_Pos_to_Transform_Pos(grid_pos);

      GameObject.Find("Earl Display").GetComponent<earldisplay>().Clear_Info();
    }

    public void Eat()
    {
      //if earl gets picked up or pushed, he should not be given the pleasure of dinner.
      target = Find_Food();

      if(target == null || Vector3.Distance(target.grid_pos,grid_pos) > 1.44f)
      {
        mood = Moods.Idle;
        return;
      }
      Vector3 tile_pos = target.grid_pos;
      Food food_item = (Food)Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)tile_pos.x,(int)tile_pos.y];

      Debug.Log(food_item);
      Dictionary<string,int> gains = food_item.Become_Eaten();



    }

    private void Display_Earl_Data()
    {
      string attitude_text = "null";

      switch(attitude)
      {
        case 100f:
          attitude_text = "Happy";
          break;

        case float n when n < 100f:
          attitude_text = "pissed";
          break;
      }

      Dictionary<string,dynamic> earl_data = new Dictionary<string,dynamic>()
      {
        {"name" , name},
        {"health" , ((int)health).ToString()},
        {"mood", attitude_text},
        {"satiety", ((int)satiety).ToString()},
        {"sprite", GetComponent<SpriteRenderer>().sprite},
        {"eyes", eyes.GetComponent<SpriteRenderer>().sprite},
        {"mouth", mouth.GetComponent<SpriteRenderer>().sprite},
        {"color", GetComponent<SpriteRenderer>().color}
      };

      GameObject.Find("Earl Display").GetComponent<earldisplay>().Display_Info(earl_data);
    }

    public void Update_Name(string input)
    {
      name = input;
    }

    public void Set_Eyes(string eye_sprite)
    {
      eyes.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Eyes/" + eye_sprite);
    }
}
