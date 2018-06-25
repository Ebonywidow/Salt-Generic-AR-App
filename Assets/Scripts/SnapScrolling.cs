using UnityEngine;
using UnityEngine.UI;
using DoozyUI;
using DG.Tweening;

public class SnapScrolling : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(0f, 1f)]
    public float scaleAmount;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Range(0f, 1f)]
    public float unfocusedTrans;
    [Range(0f, 1f)]
    public float focusedTrans;
    [Header("Other Objects")]
    //public GameObject panPrefab;
    public ScrollRect scrollRect;

    public GameObject[] buttonArray;
    //public Color[] buttonArrayColor;
    public RectTransform defaultButtonRect;

    //private GameObject[] instPans;
    public Vector2[] pansPos;
    public Vector2[] pansScale;

    //public Color[] unfocusedTransColor;

    //public Image unfocusedButtonImage;

    public RectTransform contentRect;
    public Vector2 contentVector;

    public float scale;
    public float scaleX;
    public float scaleY;

    private int selectedPanID;
    private bool isScrolling;

    private void Awake()
    {
        //defaultButtonRect = buttonArray[0].GetComponent<RectTransform>();
    }

    private void Start()
    {
        /////////// Original Start Code
        /*
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            if (i == 0) continue;
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                instPans[i].transform.localPosition.y);
            pansPos[i] = -instPans[i].transform.localPosition;
            Debug.Log("Pan ID " + i + " pansPos at the start is " + pansPos[i]);
        }
        */
        //unfocusedTransColor = new Color[buttonArray.Length];
        //buttonArrayColor = new Color[buttonArray.Length];

        defaultButtonRect = buttonArray[0].GetComponent<RectTransform>();
        contentRect = GetComponent<RectTransform>();
        //instPans = new GameObject[buttonArray.Length];
        pansPos = new Vector2[buttonArray.Length];
        pansScale = new Vector2[buttonArray.Length];
        for (int i = 0; i < buttonArray.Length; i++)
        {
            //instPans[i] = Instantiate(panPrefab, transform, false);
            //unfocusedTransColor[i] = buttonArray[i].GetComponentInChildren<Image>().color;
            //buttonArrayColor[i] = buttonArray[i].GetComponentInChildren<Image>().color;
            //unfocusedTransColor[i].a = unfocusedTrans;
            //buttonArray[i].GetComponentInChildren<Image>().color = new Color;
            if (i == 0) continue;
            buttonArray[i].transform.localPosition = new Vector2( buttonArray[i].transform.localPosition.x, buttonArray[i - 1].transform.localPosition.y + defaultButtonRect.GetComponent<RectTransform>().sizeDelta.y + panOffset);
            //buttonArray[i].transform.localPosition.x = (buttonArray[i - 1].transform.localPosition.x + defaultButtonRect.GetComponent<RectTransform>().sizeDelta.x + panOffset);
            Debug.Log("The START of the current button has a local x position of: " + buttonArray[i].transform.localPosition.x);
            pansPos[i] = buttonArray[i].transform.localPosition;
            Debug.Log("Button Name: "+ buttonArray[i].name + " Pan ID: " + i + " pansPos at the start: " + pansPos[i]);
        }

    }

    private void FixedUpdate()
    {
        Debug.Log("The selected button is: " + selectedPanID);
        if (contentRect.anchoredPosition.y >= pansPos[0].y && !isScrolling || contentRect.anchoredPosition.y <= pansPos[pansPos.Length - 1].y && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < buttonArray.Length; i++)
        {
            // The next line is a quick fix for the buttons resetting to a local position of 0,0
            // When I have time look into trying to fix this the correct way.
            //buttonArray[i].transform.localPosition = -pansPos[i];

            buttonArray[i].transform.localPosition = -pansPos[i];

            float distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);
            //Debug.Log("Button Name: " + buttonArray[i].name + " Pan ID: " + i + " distance: " + distance + " and its pansPo: " + pansPos[i]);
            if (distance < nearestPos)
            {
                nearestPos = distance;        
                if (selectedPanID != i)
                {
                    buttonArray[selectedPanID].GetComponentInChildren<Image>().SetTransButtonImageColor(unfocusedTrans);
                    buttonArray[selectedPanID].GetComponentInChildren<Text>().SetTransTextColor(unfocusedTrans);
                }
                selectedPanID = i;
            }
            scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);

            pansScale[i].x = Mathf.SmoothStep(buttonArray[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            pansScale[i].y = Mathf.SmoothStep(buttonArray[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            //pansScale[i].x = Mathf.SmoothStep(buttonArray[i].GetComponent<RectTransform>().rect.width, scaleAmount, scaleSpeed * Time.fixedDeltaTime);
            //pansScale[i].y = Mathf.SmoothStep(buttonArray[i].GetComponent<RectTransform>().rect.height, scaleAmount, scaleSpeed * Time.fixedDeltaTime);

            //pansScale[i].x = Mathf.SmoothStep(buttonArray[i].GetComponent<RectTransform>().sizeDelta.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            //pansScale[i].y = Mathf.SmoothStep(buttonArray[i].GetComponent<RectTransform>().sizeDelta.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);


            //scaleX = Mathf.Clamp(buttonArray[i].GetComponent<RectTransform>().sizeDelta.x / (distance / panOffset) * scaleOffset, (buttonArray[i].GetComponent<RectTransform>().sizeDelta.x * 0.5f), buttonArray[i].GetComponent<RectTransform>().sizeDelta.x);
            //scaleY = Mathf.Clamp(buttonArray[i].GetComponent<RectTransform>().sizeDelta.y / (distance / panOffset) * scaleOffset, (buttonArray[i].GetComponent<RectTransform>().sizeDelta.y * 0.5f), buttonArray[i].GetComponent<RectTransform>().sizeDelta.y);
            //Debug.Log("-----------------------------------------------ScaleX: " + scaleX + " ScaleY: " + scaleY);
            //pansScale[i].x = Mathf.SmoothStep(buttonArray[i].GetComponent<RectTransform>().sizeDelta.x, scaleX + 0.3f, scaleSpeed * Time.fixedDeltaTime);
            //pansScale[i].y = Mathf.SmoothStep(buttonArray[i].GetComponent<RectTransform>().sizeDelta.y, scaleY + 0.3f, scaleSpeed * Time.fixedDeltaTime);


            //pansScale[i].x = buttonArray[i].GetComponent<RectTransform>().sizeDelta.x * 1.5f;
            //pansScale[i].y = buttonArray[i].GetComponent<RectTransform>().sizeDelta.y * 1.5f;

            //Debug.Log("Scale: " + scale + " Scale plus .3: " + (scale + 0.3f));
            //Debug.Log("panScale.x: " + pansScale[i].x + "panScale.y: " + pansScale[i].y);
            //Debug.Log("---------------------------------------------------------------------------------------------------Getcomponent rect transform " + buttonArray[i].GetComponent<RectTransform>().sizeDelta.x);
            
            //Debug.Log("Getcomponent Rect width " + buttonArray[i].GetComponent<Rect>().width + " Getcomponent rect transform " + buttonArray[i].GetComponent<RectTransform>().sizeDelta.x);

            buttonArray[i].transform.localScale = pansScale[i];

            //buttonArray[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pansScale[i].x);
            //buttonArray[i].GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pansScale[i].y);

            //buttonArray[i].GetComponent<RectTransform>().sizeDelta = new Vector2(pansScale[i].x, pansScale[i].y);

            //buttonArray[i].GetComponent<RectTransform>().sizeDelta = new Vector2(pansScale[i].x, pansScale[i].y);

            //contentRect.SetWidth(pansScale[i].x);
            //contentRect.SetHeight(pansScale[i].y);



            //contentRect.rect.width = pansScale[i].x;
            //contentRect.SetHeight(pansScale[i].y);

            //buttonArray[i].GetComponent<Button>().image.color.a = unfocusedTrans;
            //buttonArrayColor[i] = unfocusedTransColor[i];
            //buttonArray[i].GetComponentInChildren<Image>().SetTransparency(unfocusedTrans);
            //unfocusedTransColor[i].a = unfocusedTrans;

            //Debug.Log("The FixedUpdate end of the current button has a local x position of: " + buttonArray[i].transform.localPosition.x);
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);
        buttonArray[selectedPanID].GetComponentInChildren<Image>().SetTransButtonImageColor(focusedTrans);
        buttonArray[selectedPanID].GetComponentInChildren<Text>().SetTransTextColor(focusedTrans);
        //Debug.Log("contentVector: " + contentVector.y);
        contentRect.anchoredPosition = contentVector;
        //Debug.Log("contentRect.anchoredPosition: " + contentRect.anchoredPosition);
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
