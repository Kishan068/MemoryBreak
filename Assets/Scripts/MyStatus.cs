using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyStatus : MonoBehaviour
{
    //get set text for status
    public TextMeshProUGUI StatusText;
    public string Status
    {
        get { return StatusText.text; }
        set { StatusText.text = value; }
    }
}
