using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;


    void Awake()
    {
        if (Instance == null)
        {
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
      if(Input.GetKeyDown(KeyCode.Alpha1) && SceneManager.GetActiveScene().name != "Gardens")
      {
        SceneManager.LoadScene("Gardens");
      }

      if(Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().name != "Marketplace")
      {
        SceneManager.LoadScene("Marketplace");
      }
    }
}
