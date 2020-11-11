using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public Itemdatabase itemDatabase;

    private GameObject Earlmanager;
    private GameObject Player;
    public GameObject Gridmanager;
    public bool on_startup;

    public Button button_quit;

    void Awake()
    {
      button_quit = GameObject.Find("Quit").GetComponent<Button>();
      button_quit.onClick.AddListener(Save_And_Quit);

      itemDatabase = new Itemdatabase();

        if (Instance == null)
        {
            on_startup = true;
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      EffectManager.Update();

      if(Input.GetKeyDown(KeyCode.S))
      {
        Save_And_Quit();
      }

      if(Input.GetKeyDown(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "Gardens")
      {

        SceneManager.LoadScene("Gardens");

        SaveSystem.Load_Level_Grid(Gridmanager);


      }

      if(Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "Marketplace")
      {

        SceneManager.LoadScene("Marketplace");
        SaveSystem.Load_Level_Grid(Gridmanager);



      }
    }

    public void Save_Game()
    {
      SaveSystem.Save_Game(Gridmanager,Player,Earlmanager);
    }

    public void Save_And_Quit()
    {
      Save_Game();
      Application.Quit();
    }

    public void Set_Player(GameObject player)
    {
      Player = player;
    }

    public void Set_Earlmanager(GameObject earlmanager)
    {
      Earlmanager = earlmanager;
    }

    public void Set_Grid(GameObject grid)
    {
      Gridmanager = grid;
    }



}
