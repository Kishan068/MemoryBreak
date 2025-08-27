using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class setZoneTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    //Get set timetext
    public void SetTimeText(string time)
    {
        timeText.text = time;
    }
}
 