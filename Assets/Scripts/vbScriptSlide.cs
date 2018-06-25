using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
//using System.Collections;

public class vbScriptSlide : MonoBehaviour
{

    // Use this for initialization
    private GameObject penguin;
    public Button vbSlideButton;
    //private GameObject vbFeedButtonObject;
    //private GameObject vbSlideButtonObject;

    void Start()
    {
        penguin = GameObject.Find("Penguin");
        Button btn = vbSlideButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        //vbFeedButtonObject = GameObject.Find("VBFeed");
        //vbSlideButtonObject = GameObject.Find("VBSlide");
        //vbFeedButtonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        //vbSlideButtonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    void TaskOnClick()
    {
        penguin.GetComponent<Animator>().Play("Slide");
    }

    //public void OnClick ()
    //{
    //    //Debug.Log("vbfeed button down!");
    //    penguin.GetComponent<Animator>().Play("JUMP00");
    //}

    //public void OnButtonReleased (VirtualButtonBehaviour vbFeed)
    //{
    //    Debug.Log("vbfeed button released!");
    //    penguin.GetComponent<Animator>().StopPlayback();
    //}


}