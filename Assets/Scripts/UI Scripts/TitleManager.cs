using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    GameObject contents;
    // Start is called before the first frame update
    void Start()
    {
      contents = transform.GetChild(0).gameObject;
    }

    public void Toggle_Title_Display(bool on_off)
    {
      contents.SetActive(on_off);
    }
}
