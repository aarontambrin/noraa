using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    public int avgFrameRate;
    Text display_Text;
    float[] AverageCount;
    float frameAverage = 0;
    int arraycount = 0;
    RectTransform rect;

    private void Start()
    {
        AverageCount = new float[100];
        display_Text = GetComponent<Text>();
        rect = GetComponent<RectTransform>();
        
        rect.anchoredPosition = new Vector3((Screen.width / 2) - 100, (-Screen.height / 2) + 30, 0);


    }
    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        AverageCount[arraycount] = avgFrameRate;
        //display_Text.text = avgFrameRate.ToString() + " FPS";
        arraycount++;
        if (arraycount > AverageCount.Length-1)
        {
            arraycount = 0;
        }
        for (int x=0; x<AverageCount.Length; x++)
        {
            frameAverage += AverageCount[x];
        }
        frameAverage = frameAverage / AverageCount.Length;
        display_Text.text = frameAverage.ToString("000.0") + " FPS";
        
    }
}
