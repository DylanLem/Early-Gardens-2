﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public partial class playermanager : MonoBehaviour
{

    public GameObject Player;
    public GameObject Itemmanager;
    public GameObject Gridmanager;
    public GameObject Earlmanager;

    private Item[,] items_on_grid;

    private Vector3 camera_offset;
    public Vector3 grid_pos;

    public float move_timer;

    private KeyCode current_key;

    Dictionary<string,List<KeyCode>> directions = new Dictionary<string,List<KeyCode>>()
    {
      {"Up_Left", new List<KeyCode>(){KeyCode.Keypad7}},
      {"Up", new List<KeyCode>(){KeyCode.Keypad8, KeyCode.UpArrow}},
      {"Up_Right", new List<KeyCode>(){KeyCode.Keypad9}},
      {"Right",new List<KeyCode>(){KeyCode.Keypad6,KeyCode.RightArrow}},
      {"Down_Right",new List<KeyCode>(){KeyCode.Keypad3}},
      {"Down",new List<KeyCode>(){KeyCode.Keypad2,KeyCode.DownArrow}},
      {"Down_Left",new List<KeyCode>(){KeyCode.Keypad1}},
      {"Left",new List<KeyCode>(){KeyCode.Keypad4,KeyCode.LeftArrow}},

    };

    public Button grab, build, destroy;



    void Start()
    {
      Itemmanager =GameObject.FindWithTag("Item Manager");
      Gridmanager = GameObject.FindWithTag("Grid");
      Earlmanager = GameObject.Find("Earl Manager");
      camera_offset = new Vector3(0,0);

      Spawn_Player();
      Snap_Camera();
      move_timer = 0;
      current_ContextAction = ContextActions.Grab;


      grab = GameObject.Find("Grab").GetComponent<Button>();
        grab.onClick.AddListener(delegate{
          Set_Context_Action(ContextActions.Grab);
          });

      build = GameObject.Find("Build").GetComponent<Button>();
        build.onClick.AddListener(delegate{
           Set_Context_Action(ContextActions.Build);
        });

      destroy = GameObject.Find("Destroy").GetComponent<Button>();
        destroy.onClick.AddListener(delegate{
           Set_Context_Action(ContextActions.Destroy);
         });
    }

    // Update is called once per frame
    void Update()
    {
      move_timer += Time.deltaTime;
      Check_Action(Get_Keys_Pressed());

    }

    public void Spawn_Player()
    {

      Player = Instantiate(Player);
      Player.GetComponent<player>().playermanager = gameObject;


      GameObject.FindWithTag("Level Controller").GetComponent<LevelController>().Set_Player(Player);



        GameObject spawn_square = GameObject.FindWithTag("Grid").GetComponent<gridmanager>().Find_Empty_Square();

        grid_pos = spawn_square.GetComponent<tilebehavior>().Get_Grid_Pos();


      Gridmanager.GetComponent<gridmanager>().Update_Square(grid_pos,"add",Player);

    }


    private List<KeyCode> Get_Keys_Pressed()
    {
      List<KeyCode> keys = new List<KeyCode>();


      //adding all directional inputs into keys_pressed
      foreach(KeyValuePair<string,List<KeyCode>> kvp in directions)
      foreach(KeyCode vkey in kvp.Value)
      if(Input.GetKey(vkey)) keys.Add(vkey);

      //adding all action inputs into keys_pressed
      foreach(KeyCode vkey in actions.Values)
      if(Input.GetKeyDown(vkey)) keys.Add(vkey);

      return keys;
    }



    private void Align_Camera(Vector3 direction)
    {
      Camera.main.transform.position += direction;
    }

    public void Snap_Camera()
    {
      Camera.main.transform.position = Player.transform.position + Vector3.back;
      camera_offset = Vector3.zero;
    }

}
