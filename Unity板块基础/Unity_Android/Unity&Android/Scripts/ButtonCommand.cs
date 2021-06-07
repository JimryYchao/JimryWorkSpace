using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCommand : MonoBehaviour
{
    Button _button;
    GameObject gmCube;
    Text text;
    string show = "显  示";
    string hide = "隐  藏";

    private void OnEnable()
    {
        gmCube = GameObject.Find("Cube");
       text = transform.GetChild(0).GetComponent<Text>();
        text.text = hide;
        _button = GetComponent<Button>();
        AddLisener();
    }
    public void ShowCube()
    {
        gmCube.SetActive(true);
    }
    public void HideCube()
    {
        gmCube.SetActive(false);
    }

    bool isShow = true;
    void AddLisener()
    {
        _button.onClick.AddListener(()=> {

            if (isShow)
            {
                isShow = false;
                HideCube();
                text.text = show;
                Debug.Log("点击了隐藏");
            }
            else
            {
                isShow = true;
                ShowCube();
                text.text = hide;
                Debug.Log("点击了显示");

            }

        });
    }
    
}
