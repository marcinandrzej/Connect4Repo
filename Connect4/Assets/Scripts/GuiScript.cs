using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void action(int x);
public delegate void action2(GameObject gO);

public class GuiScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject CreatePanel(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
        Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition,
        Sprite image, Color32 color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent.transform);
        panel.AddComponent<RectTransform>();
        panel.AddComponent<Image>();

        panel.GetComponent<Image>().sprite = image;
        panel.GetComponent<Image>().type = Image.Type.Sliced;
        panel.GetComponent<Image>().color = color;

        panel.GetComponent<RectTransform>().anchorMin = anchorMin;
        panel.GetComponent<RectTransform>().anchorMax = anchorMax;
        panel.GetComponent<RectTransform>().pivot = pivot;
        panel.GetComponent<RectTransform>().localScale = localScale;
        panel.GetComponent<RectTransform>().localPosition = localPosition;
        panel.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        panel.GetComponent<RectTransform>().sizeDelta = sizeDelta;
        return panel;
    }

    public GameObject CreateButton(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
       Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject button = new GameObject(name);
        button.transform.SetParent(parent.transform);
        button.AddComponent<RectTransform>();
        button.AddComponent<Image>();
        button.AddComponent<Button>();

        button.GetComponent<Image>().sprite = image;
        button.GetComponent<Image>().type = Image.Type.Sliced;
        button.GetComponent<Image>().color = color;

        button.GetComponent<RectTransform>().anchorMin = anchorMin;
        button.GetComponent<RectTransform>().anchorMax = anchorMax;
        button.GetComponent<RectTransform>().pivot = pivot;
        button.GetComponent<RectTransform>().localScale = localScale;
        button.GetComponent<RectTransform>().localPosition = localPosition;
        button.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        button.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        return button;
    }

    public GameObject CreateImage(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
      Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, Sprite image, Color32 color)
    {
        GameObject imageOb = new GameObject(name);
        imageOb.transform.SetParent(parent.transform);
        imageOb.AddComponent<RectTransform>();
        imageOb.AddComponent<Image>();

        imageOb.GetComponent<Image>().sprite = image;
        imageOb.GetComponent<Image>().type = Image.Type.Sliced;
        imageOb.GetComponent<Image>().color = color;
        imageOb.GetComponent<Image>().raycastTarget = false;

        imageOb.GetComponent<RectTransform>().anchorMin = anchorMin;
        imageOb.GetComponent<RectTransform>().anchorMax = anchorMax;
        imageOb.GetComponent<RectTransform>().pivot = pivot;
        imageOb.GetComponent<RectTransform>().localScale = localScale;
        imageOb.GetComponent<RectTransform>().localPosition = localPosition;
        imageOb.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        imageOb.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        return imageOb;
    }

    public GameObject CreateText(GameObject parent, string name, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
      Vector3 localScale, Vector3 localPosition, Vector2 sizeDelta, Vector2 anchoredPosition, string text, Color32 color)
    {
        GameObject textBlock = new GameObject(name);
        textBlock.transform.SetParent(parent.transform);
        textBlock.AddComponent<RectTransform>();
        textBlock.AddComponent<Text>();

        textBlock.GetComponent<Text>().resizeTextMaxSize = 100;
        textBlock.GetComponent<Text>().resizeTextForBestFit = true;
        textBlock.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        textBlock.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        textBlock.GetComponent<Text>().fontStyle = FontStyle.Bold;
        textBlock.GetComponent<Text>().color = color;
        textBlock.GetComponent<Text>().text = text;

        textBlock.GetComponent<RectTransform>().anchorMin = anchorMin;
        textBlock.GetComponent<RectTransform>().anchorMax = anchorMax;
        textBlock.GetComponent<RectTransform>().pivot = pivot;
        textBlock.GetComponent<RectTransform>().localScale = localScale;
        textBlock.GetComponent<RectTransform>().localPosition = localPosition;
        textBlock.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        textBlock.GetComponent<RectTransform>().sizeDelta = sizeDelta;

        return textBlock;
    }

    public GameObject[,] FillWithImages(GameObject panel, int columnCount, int rowsCount, Sprite s, Color32 color)
    {
        GameObject[,] images = new GameObject[columnCount, rowsCount];
        float buttonW = Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.x) / (float)columnCount;
        float offsetx = buttonW / 2.0f;
        float buttonH = Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.y) / (float)rowsCount;
        float offsety = buttonH / 2.0f;
        for (int j = 0; j < rowsCount; j++)
        {
            for (int i = 0; i < columnCount; i++)
            {
                GameObject img = CreateImage(panel, ("Hole" + (j * (rowsCount + 1) + i).ToString()),
                    new Vector2(0, 0), new Vector2(0, 0), new Vector2(0.5f, 0.5f),
                   new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonW, buttonH),
                   new Vector3((offsetx + i * buttonW), (offsety + j * buttonH), 0), s,
                   color);
                images[i, j] = img;
            }
        }
        return images;
    }

    public GameObject[,] FillWithButtons(GameObject panel, int columnCount, int rowsCount, Sprite s, Color32 color)
    {
        GameObject[,] buttons = new GameObject[columnCount, rowsCount];
        float buttonW = Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.x) / (float)columnCount;
        float offsetx = buttonW / 2.0f;
        float buttonH = Mathf.Abs(panel.GetComponent<RectTransform>().sizeDelta.y) / (float)rowsCount;
        float offsety = buttonH / 2.0f;
        for (int j = 0; j < rowsCount; j++)
        {
            for (int i = 0; i < columnCount; i++)
            {
                GameObject but = CreateButton(panel, ("Button" + (j * rowsCount + i).ToString()),
                    new Vector2(0, 0), new Vector2(0, 0), new Vector2(0.5f, 0.5f),
                   new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonW, buttonH),
                   new Vector3((offsetx + i * buttonW), (offsety + j * buttonH), 0), s,
                   color);
                buttons[i, j] = but;
            }
        }
        return buttons;
    }

    public void SetAction(GameObject button, int x, action act)
    {
        button.GetComponent<Button>().onClick.AddListener(delegate { act.Invoke(x); });
    }

    public void SetAction(GameObject button, action2 act)
    {
        button.GetComponent<Button>().onClick.AddListener(delegate { act.Invoke(button); });
    }
}
