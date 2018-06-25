using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
//using System.Collections;

public class vbScriptFeed : MonoBehaviour {

    // Use this for initialization
    private GameObject penguin;
    private GameObject feedFish;
    public Button vbFeedButton;
    //private GameObject vbFeedButtonObject;
    //private GameObject vbSlideButtonObject;

	void Start () {
        penguin = GameObject.Find("Penguin");
        Button btn = vbFeedButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        feedFish = GameObject.Find("fish1");
        //vbFeedButtonObject = GameObject.Find("VBFeed");
        //vbSlideButtonObject = GameObject.Find("VBSlide");
        //vbFeedButtonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
        //vbSlideButtonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}

    void TaskOnClick()
    {
        feedFish.gameObject.SetActive(enabled);
        feedFish.gameObject.SetActive(true);
        penguin.GetComponent<Animator>().Play("Feed");
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
