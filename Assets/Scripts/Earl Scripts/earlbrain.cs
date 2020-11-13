using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class earlbrain : MonoBehaviour
{



    // Due to lack of foresight, the fur sprite is just the sprite for the game object itself
    public GameObject eyes, mouth, ears;

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

    private string home_grid;
    private float act_timer;
    private float speed;
    private Color color;
    private float satiety = 10f;
    private float health = 100f;
    private float attitude = 100f;
    private float metabolism = 0.8f;
    private Item target;
    /* *** NOTES ***
    earls are smart and stupid. figure it outs

    If you ever need to SPAWN an EARL in any way, please please DON'T
    Instantiate(Earl), cause he won't have eyes, mouth, or ears right off
    the bat. and earls are quite self conscious. USE earlmanager's Birth_Earl().
    */

    // Start is called before the first frame update
    void Start()
    {

      Gridmanager = GameObject.FindWithTag("Grid");
      Itemmanager = GameObject.FindWithTag("Item Manager");

      mood = Moods.Idle;
      act_timer = 0.0f;
      speed = UnityEngine.Random.Range(0.7f,1.2f);

      home_grid = Gridmanager.name;

    }

    // Update is called once per frame
    void Update()
    {
      //EARL's have a fully functional metabolic system.
      //Fun Fact: if EARL's couldn't metabolise food, not only
      //would they starve, but they also wouldn't be able to LEVEL UP
       satiety -= metabolism * Time.deltaTime;

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

    //when i set him down gently....
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

    //lawd he's hungry watch out
    public void Eat()
    {
      //if earl gets picked up or pushed, he should not be given the pleasure of dinner.
      //we make him confirm his target before munchtime.
      target = Find_Food();

      if(target == null || Vector3.Distance(target.grid_pos,grid_pos) > 1.44f)
      {
        mood = Moods.Idle;
        return;
      }


      Vector3 tile_pos = target.grid_pos;
      Food food_item = (Food)Itemmanager.GetComponent<itemmanager>().items_on_grid[(int)tile_pos.x,(int)tile_pos.y];

      Dictionary<string,int> gains = food_item.Become_Eaten();

      Digest_Gains(gains);
    }

    public void Digest_Gains(Dictionary<string,int> gains)
    {
      satiety += gains.ContainsKey("satiety") == true ? gains["satiety"] : 0;
    }



    public Dictionary<string,dynamic> Pack_Earl_Data()
    {
      Dictionary<string,dynamic> earl_data = new Dictionary<string,dynamic>()
      {
        // We're just making a big dictionary of all earl's properties in case we need them.
        // if any EARLs ask tell them its for a family tree.

        {"name" , name},
        {"health" , health},
        {"mood", "Pissed"},
        {"satiety", satiety},
        {"grid_pos", new int[3]{(int)grid_pos.x,(int)grid_pos.y,(int)grid_pos.z}},
        {"speed", speed.ToString()},

        {"sprite", GetComponent<SpriteRenderer>().sprite.name},
        {"eyes", eyes.GetComponent<SpriteRenderer>().sprite.name},
        {"mouth", mouth.GetComponent<SpriteRenderer>().sprite.name},
        {"color", new float[3]{(float)GetComponent<SpriteRenderer>().color.r,(float)GetComponent<SpriteRenderer>().color.g,(float)GetComponent<SpriteRenderer>().color.b}},
        {"home_grid", home_grid}
      };

      return earl_data;
    }

    public void Load_Data(Dictionary<string,dynamic> earl_data)
    {

      Update_Name(earl_data["name"]);
      Set_Eyes(earl_data["eyes"]);
      Set_Mouth(earl_data["mouth"]);
      Set_Fur(earl_data["sprite"]);
      GetComponent<SpriteRenderer>().color = new Color(earl_data["color"][0],earl_data["color"][1],earl_data["color"][2]);

      satiety = earl_data["satiety"];
      health = earl_data["health"];
      home_grid = earl_data["home_grid"];
    }

    public void Display_Earl_Data()
    {
      GameObject.Find("Earl Display").GetComponent<earldisplay>().Display_Info(Pack_Earl_Data());
    }

    public void Update_Name(string input)
    {
      name = input;
    }



    public void Set_Eyes(string eye_sprite)
    {
      eyes.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Eyes/" + eye_sprite);
    }

    public void Set_Mouth(string mouth_sprite)
    {
      mouth.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Mouths/" + mouth_sprite);
    }

    public void Set_Fur(string fur_sprite)
    {
      GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Fur/" + fur_sprite);
    }

    public void Set_Ears(string ear_sprite)
    {
      GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Earls/Ears/" + ear_sprite);
    }
}
