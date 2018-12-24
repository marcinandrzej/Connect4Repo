using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewScript : MonoBehaviour
{
    private GuiScript guiScript;

    private GameObject gamePanel;
    private GameObject[,] holes;
    private GameObject[,] eggs;
    private GameObject[,] buttons;
    private GameObject[] hens;

    private GameObject endMenuPanel;
    private GameObject endMenuButton;
    private GameObject endRetryButton;

    private GameObject menuPanel;
    private GameObject[,] menuButtons;

    private GameObject playerPointer;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetUp(GameObject parent, float panelX, float panelY, int colsCount, int rowsCount, Sprite panelImage, Sprite boardImage,
        float buttonSize, Sprite retryImage, Sprite menuImage, Sprite soundImage, Sprite circleImage, Sprite henImage)
    {
        guiScript = gameObject.AddComponent<GuiScript>();
        eggs = new GameObject[colsCount, rowsCount];

        gamePanel = guiScript.CreatePanel(parent, "GamePanel", new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0.5f, 0),
            new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(panelX, panelY), new Vector2(0, 0), panelImage,
            new Color32(255, 255, 255, 100));

        float sizeX = panelX / (float)colsCount;
        float sizeY = panelY / (float)rowsCount;
        hens = new GameObject[colsCount];
        for (int i = 0; i < colsCount; i++)
        {
            Vector2 pos = new Vector2((i + 0.5f) * sizeX, sizeY + panelY);
            hens[i] = guiScript.CreateImage(gamePanel, "Hen", new Vector2(0, 0), new Vector2(0, 0),
                new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(3.5f * sizeX, 3.5f * sizeY),
                new Vector2(pos.x, pos.y + 1.5f * sizeY), henImage, new Color32(255, 255, 255, 255));
            hens[i].SetActive(false);
        }

        holes = guiScript.FillWithImages(gamePanel, colsCount, rowsCount, boardImage, new Color32(130, 85, 30, 255));
        buttons = guiScript.FillWithButtons(gamePanel, colsCount, 1, panelImage, new Color32(0, 0, 0, 0));

        menuPanel = guiScript.CreatePanel(parent, "MenuPanel", new Vector2(0, 1), new Vector2(0, 1),
            new Vector2(0, 1), new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(3 * buttonSize, buttonSize),
            new Vector2(0, 0), panelImage, new Color32(255, 255, 255, 0));
        menuButtons = guiScript.FillWithButtons(menuPanel, 3, 1, panelImage, new Color32(255, 255, 255, 255));

        menuButtons[0, 0].GetComponent<Image>().sprite = menuImage;
        menuButtons[1, 0].GetComponent<Image>().sprite = retryImage;
        menuButtons[2, 0].GetComponent<Image>().sprite = soundImage;

        menuButtons[0, 0].GetComponent<Button>().onClick.AddListener(delegate 
        {
            Destroy(SettingsScript.instance.gameObject);
            SceneManager.LoadScene("MenuScene");
        });
        menuButtons[1, 0].GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });

        playerPointer = guiScript.CreateImage(parent, "PlayerPointer", new Vector2(1, 1), new Vector2(1, 1), new Vector2(1, 1),
            new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonSize, buttonSize), new Vector2(0, 0),
            circleImage, new Color32(255, 255, 255, 255));
    }

    public void SetUpButtons(action act)
    {
        for (int i = 0; i < holes.GetLength(0); i++)
        {
            int x = i;
            guiScript.SetAction(buttons[i, 0], x, act);
        }
    }

    public void SetUpSoundButton(action2 act, bool isOn, Sprite soundOnImage, Sprite soundOffImage)
    {
        if (isOn)
        {
            menuButtons[2, 0].GetComponent<Image>().sprite = soundOnImage;
        }
        else
        {
            menuButtons[2, 0].GetComponent<Image>().sprite = soundOffImage;
        }
        guiScript.SetAction(menuButtons[2, 0], act);
    }

    public void ColorPlayerPointer(Color32 color)
    {
        playerPointer.GetComponent<Image>().color = color;
    }

    public void ChangeSprite(GameObject ob, Sprite image)
    {
        ob.GetComponent<Image>().sprite = image;
    }

    public void ActiveHen(int x, bool show)
    {
        hens[x].SetActive(show);
    }

    public void PlayMove(int x, int y, float panelX, int colsCount, float panelY, int rowsCount, Color32 color,
        Sprite eggImage)
    {
        float sizeX = panelX / (float)colsCount;
        float sizeY = panelY / (float)rowsCount;
        Vector2 pos = new Vector2((x + 0.5f) * sizeX, sizeY + panelY);
        Vector2 destination = holes[x, y].GetComponent<RectTransform>().anchoredPosition;

        GameObject egg = guiScript.CreateImage(gamePanel, "Circle", new Vector2(0, 0), new Vector2(0, 0),
            new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(sizeX, sizeY),
            pos, eggImage, color);
        egg.transform.SetSiblingIndex(0);
        eggs[x, y] = egg;
        StartCoroutine(PlayMoveCoroutine(egg, destination));
    }

    private IEnumerator PlayMoveCoroutine(GameObject egg, Vector2 destination)
    {
        while (egg.GetComponent<RectTransform>().anchoredPosition != destination)
        {
            egg.GetComponent<RectTransform>().anchoredPosition =
                Vector2.MoveTowards(egg.GetComponent<RectTransform>().anchoredPosition, destination, 15);
            yield return new WaitForEndOfFrame();
        }
        egg.GetComponent<RectTransform>().anchoredPosition = destination;
    }

    public void ShowEndMenu(GameObject parent, float panelX, float panelY, Sprite panelImage, float buttonSize, float offset,
        Sprite retryImage, Sprite menuImage)
    {
        endMenuPanel = guiScript.CreatePanel(parent, "EndMenuPanel", new Vector2(0.5f, 1), new Vector2(0.5f, 1),
            new Vector2(0.5f, 1), new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(panelX, panelY),
            new Vector2(0, -offset), panelImage, new Color32(255, 255, 255, 0));
        guiScript.CreateText(endMenuPanel, "EndText", new Vector2(0.5f, 1), new Vector2(0.5f, 1), new Vector2(0.5f, 1),
            new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(panelX, panelY*0.5f), new Vector2(0, 0), "GAME OVER",
            new Color32(0, 0, 0, 255));
        endMenuButton = guiScript.CreateButton(endMenuPanel, "MenuButton", new Vector2(0.5f, 0), new Vector2(0.5f, 0), new Vector2(0.5f, 0),
            new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonSize, buttonSize),
            new Vector2(-0.55f * buttonSize, 0.1f * buttonSize), menuImage, new Color32(255, 255, 255, 255));
        endRetryButton = guiScript.CreateButton(endMenuPanel, "RetryButton", new Vector2(0.5f, 0), new Vector2(0.5f, 0),
            new Vector2(0.5f, 0), new Vector3(1, 1, 1), new Vector3(0, 0, 0), new Vector2(buttonSize, buttonSize),
            new Vector2(0.55f * buttonSize, 0.1f * buttonSize), retryImage, new Color32(255, 255, 255, 255));

        endMenuButton.GetComponent<Button>().onClick.AddListener(delegate 
        {
            Destroy(SettingsScript.instance.gameObject);
            SceneManager.LoadScene("MenuScene");
        });
        endRetryButton.GetComponent<Button>().onClick.AddListener(delegate { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
    }

    public void HighlightWinner(List<int[]> boxes)
    {
        if (boxes.Count > 0)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                StartCoroutine(HighlightCoroutine(eggs[boxes[i][0], boxes[i][1]]));
            }
        }
    }

    private IEnumerator HighlightCoroutine(GameObject _egg)
    {
        for (int i = 0; i < 20; i++)
        {
            _egg.gameObject.GetComponent<Image>().enabled = !_egg.gameObject.GetComponent<Image>().enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
