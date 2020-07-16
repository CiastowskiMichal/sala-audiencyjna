using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    Text text;
    float counter = 5;
    // Start is called before the first frame update
    void Start()
    {
        counter = 7;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter < 0)
        {
            Destroy(this.gameObject);
        }
        text.color = new Vector4(text.color.r, text.color.g, text.color.b, counter);
    }
    public void setSubtitle(string subtitle)
    {
        text.text = subtitle;
    }
}
