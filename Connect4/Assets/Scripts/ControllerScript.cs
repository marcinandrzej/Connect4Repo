using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerScript : MonoBehaviour
{
    public GameObject canvas;
    private ModelScript model;
    private ViewScript view;
    private SoundScript soundScr;

    private int player = 0;
    private bool end = false;
    private bool canPlay = true;
    private bool enemyAI = true;

    // Use this for initialization
    void Start ()
    {
        model = gameObject.AddComponent<ModelScript>();
        view = gameObject.AddComponent<ViewScript>();
        soundScr = gameObject.AddComponent<SoundScript>();

        Invoke("SetUpGame", 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetUpGame()
    {
        soundScr.SetUp(DataScript.instance.backGroundMusic);
        view.SetUp(canvas, DataScript.instance.GetPanelX(), DataScript.instance.GetPanelY(),
            DataScript.instance.GetColsCount(), DataScript.instance.GetRowsCount(),
            DataScript.instance.panelImage, DataScript.instance.boardImage, DataScript.instance.GetMenuButtonSize(),
            DataScript.instance.retryImage, DataScript.instance.menuImage, DataScript.instance.soundOnImage,
            DataScript.instance.eggImage, DataScript.instance.henImage);
        model.SetUpGame(DataScript.instance.GetColsCount(), DataScript.instance.GetRowsCount());

        action act = Play;
        view.SetUpButtons(act);

        action2 act2 = SoundOnOff;
        view.SetUpSoundButton(act2, soundScr.SoundOn,DataScript.instance.soundOnImage, DataScript.instance.soundOffImage);

        view.ColorPlayerPointer(DataScript.instance.GetColors()[player]);

        if (SettingsScript.instance != null)
        {
            enemyAI = SettingsScript.instance.EnemyAI;
        }
    }

    private void Play(int x)
    {
        if (!end && canPlay)
        {
            if (model.CanPlay(x))
            {
                StartCoroutine(PlayCoroutine(x));
            }
        }
    }

    private IEnumerator PlayCoroutine(int x)
    {
        canPlay = false;
        int y = model.Play(x, player);
        view.ActiveHen(x, true);
        yield return new WaitForSeconds(0.2f);
        view.PlayMove(x, y, DataScript.instance.GetPanelX(), DataScript.instance.GetColsCount(),
            DataScript.instance.GetPanelY(), DataScript.instance.GetRowsCount(),
            DataScript.instance.GetColors()[player], DataScript.instance.eggImage);
        soundScr.PlaySound(DataScript.instance.playSound);
        end = model.IsEnd();
        yield return new WaitForSeconds(0.5f);
        view.ActiveHen(x, false);
        if (end)
        {
            view.HighlightWinner(model.GetWinningList());
            view.ShowEndMenu(canvas, DataScript.instance.GetEndMenuX(), DataScript.instance.GetEndMenuY(),
                DataScript.instance.panelImage, DataScript.instance.GetEndMenuButtonSize(),
                DataScript.instance.GetEndMenuOffset(), DataScript.instance.retryImage, DataScript.instance.menuImage);
        }
        else
        {
            player = (player + 1) % 2;
            view.ColorPlayerPointer(DataScript.instance.GetColors()[player]);

            if (enemyAI && player == 1)
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine(PlayCoroutine(model.EnemyChoice()));
            }
            else
            {
                canPlay = true;
            }
        }
    }

    public void SoundOnOff(GameObject ob)
    {
        Sprite sound;
        if (soundScr.SoundOn)
        {
            sound = DataScript.instance.soundOffImage;
            soundScr.SoundOnOff(false);
        }
        else
        {
            sound = DataScript.instance.soundOnImage;
            soundScr.SoundOnOff(true);
        }
        view.ChangeSprite(ob, sound);
    }
}
