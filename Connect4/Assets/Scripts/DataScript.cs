using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataScript : MonoBehaviour
{
    public static DataScript instance;

    private const float PANEL_X = 600;
    private const float PANEL_Y = 600;

    private const float END_MENU_OFFSET = 200;
    private const float END_MENU_X = 600;
    private const float END_MENU_Y = 400;
    private const float END_MENU_BUTTON_SIZE = 200;

    private const float MENU_BUTTON_SIZE = 150;

    private const int COLS_COUNT = 7;
    private const int ROWS_COUNT = 6;

    private Color32[] colors;

    public Sprite panelImage;
    public Sprite boardImage;
    public Sprite eggImage;
    public Sprite henImage;

    public Sprite retryImage;
    public Sprite menuImage;
    public Sprite soundOnImage;
    public Sprite soundOffImage;

    public AudioClip playSound;
    public AudioClip backGroundMusic;

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
            instance = this;
        colors = new Color32[2];
        colors[0] = new Color32(255, 150, 150, 255);
        colors[1] = new Color32(150, 150, 150, 255);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public float GetPanelX()
    {
        return PANEL_X;
    }

    public float GetPanelY()
    {
        return PANEL_Y;
    }

    public int GetColsCount()
    {
        return COLS_COUNT;
    }

    public int GetRowsCount()
    {
        return ROWS_COUNT;
    }

    public float GetMenuButtonSize()
    {
        return MENU_BUTTON_SIZE;
    }

    public float GetEndMenuOffset()
    {
        return END_MENU_OFFSET;
    }

    public float GetEndMenuX()
    {
        return END_MENU_X;
    }

    public float GetEndMenuY()
    {
        return END_MENU_Y;
    }

    public float GetEndMenuButtonSize()
    {
        return END_MENU_BUTTON_SIZE;
    }

    public Color32[] GetColors()
    {
        return colors;
    }
}
