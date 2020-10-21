using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class border_logic : MonoBehaviour
{
    public Sprite border_highlight;
    public Sprite border_norm;
    // Start is called before the first frame update
    void Start()
    {
      border_highlight = Resources.Load<Sprite>("UI/borderhighlight");
      border_norm = Resources.Load<Sprite>("UI/bordernorm");
    }


    public void Highlight_Border()
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = border_highlight;
    }

    public void Un_Highlight_Border()
    {
      gameObject.GetComponent<SpriteRenderer>().sprite = border_norm;
    }
}
