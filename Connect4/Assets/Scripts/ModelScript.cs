using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    private int[,] gameTable;
    private List<int[]> winningBoxes;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetUpGame(int colsCount, int rowsCount)
    {
        gameTable = new int[colsCount, rowsCount];
        for (int i = 0; i < colsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                gameTable[i, j] = -1;
            }
        }
        winningBoxes = new List<int[]>();
    }

    public List<int[]> GetWinningList()
    {
        return winningBoxes;
    }

    public bool CanPlay(int x)
    {
        for (int i = 0; i < gameTable.GetLength(1); i++)
        {
            if (gameTable[x, i] == -1)
                return true;
        }
        return false;
    }

    public int Play(int x, int player)
    {
        bool stop = false;
        int i = 0;
        while (!stop)
        {
            if (gameTable[x, i] == -1)
            {
                stop = true;
            }
            else
            {
                i++;
            }
        }
        gameTable[x, i] = player;
        return i;
    }

    private bool IsWinner()
    {
        for (int x = 0; x < gameTable.GetLength(0) - 3; x++)
        {
            for (int y = 0; y < gameTable.GetLength(1); y++)
            {
                if (gameTable[x, y] != -1 && gameTable[x, y] == gameTable[x + 1, y] && gameTable[x + 1, y] == gameTable[x + 2, y]
                    && gameTable[x + 2, y] == gameTable[x + 3, y])
                {
                    AddToWinList(x, y);
                    AddToWinList(x + 1, y);
                    AddToWinList(x + 2, y);
                    AddToWinList(x + 3, y);
                    return true;
                }
            }
        }

        for (int x = 0; x < gameTable.GetLength(0); x++)
        {
            for (int y = 0; y < gameTable.GetLength(1) - 3; y++)
            {
                if (gameTable[x, y] != -1 && gameTable[x, y] == gameTable[x, y + 1] && gameTable[x, y + 1] == gameTable[x, y + 2]
                    && gameTable[x, y + 2] == gameTable[x, y + 3])
                {
                    AddToWinList(x, y);
                    AddToWinList(x, y + 1);
                    AddToWinList(x, y + 2);
                    AddToWinList(x, y + 3);
                    return true;
                }
            }
        }

        for (int x = 0; x < gameTable.GetLength(0) - 3; x++)
        {
            for (int y = 0; y < gameTable.GetLength(1) - 3; y++)
            {
                if (gameTable[x, y] != -1 && gameTable[x, y] == gameTable[x + 1, y + 1] && gameTable[x + 1, y + 1] == gameTable[x + 2, y + 2]
                    && gameTable[x + 2, y + 2] == gameTable[x + 3, y + 3])
                {
                    AddToWinList(x, y);
                    AddToWinList(x + 1, y + 1);
                    AddToWinList(x + 2, y + 2);
                    AddToWinList(x + 3, y + 3);
                    return true;
                }
            }
        }

        for (int x = 3; x < gameTable.GetLength(0); x++)
        {
            for (int y = 0; y < gameTable.GetLength(1) - 3; y++)
            {
                if (gameTable[x, y] != -1 && gameTable[x, y] == gameTable[x - 1, y + 1] && gameTable[x - 1, y + 1] == gameTable[x - 2, y + 2]
                    && gameTable[x - 2, y + 2] == gameTable[x - 3, y + 3])
                {
                    AddToWinList(x, y);
                    AddToWinList(x - 1, y + 1);
                    AddToWinList(x - 2, y + 2);
                    AddToWinList(x - 3, y + 3);
                    return true;
                }
            }
        }
        return false;
    }

    public bool IsEnd()
    {
        if (IsWinner())
            return true;
        for (int x = 0; x < gameTable.GetLength(0); x++)
        {
            for (int y = 0; y < gameTable.GetLength(1); y++)
            {
                if (gameTable[x, y] == -1)
                    return false;
            }
        }
        return true;
    }

    private void AddToWinList(int x, int y)
    {
        int[] box = new int[2];
        box[0] = x;
        box[1] = y;
        winningBoxes.Add(box);
    }

    private int SeekFreePlace(int x)
    {
        bool stop = false;
        int i = 0;
        while (!stop)
        {
            if (gameTable[x, i] == -1)
            {
                stop = true;
            }
            else
            {
                i++;
            }
        }
        return i;
    }

    public int EnemyChoice()
    {
        int[] table = new int[gameTable.GetLength(0)];

        for (int x = 0; x < table.Length; x++)
        {
            if (CanPlay(x))
            {
                table[x] = SeekFreePlace(x);
            }
            else
            {
                table[x] = -1;
            }
        }

        for (int x = 0; x < table.Length; x++)
        {
            if (table[x] != -1)
            {
                if (WinLoosePrevent(x, table[x]))
                    return x;
            }
        }

        List<int> safePlays = new List<int>();
        for (int x = 0; x < table.Length; x++)
        {
            if (table[x] != -1)
            {
                if (IsSafeToPlay(x, table[x]))
                {
                    int i = x;
                    safePlays.Add(i);
                }
            }
        }

        if (safePlays.Count > 0)
            return safePlays[Random.Range(0, (safePlays.Count - 1))];

        for (int i = 0; i < table.Length; i++)
        {
            if (table[i] == -1)
                return table[i];
        }

        return 0;
    }

    private bool WinLoosePrevent(int x, int y)
    {
        //check horizontally
        if (x >= 0 && x <= gameTable.GetLength(0) - 4)
        {
            if (gameTable[x + 1, y] != -1 && gameTable[x + 1, y] == gameTable[x + 2, y] && gameTable[x + 2, y] == gameTable[x + 3, y])
                return true;
        }

        if (x >= 1 && x <= gameTable.GetLength(0) - 3)
        {
            if (gameTable[x - 1, y] != -1 && gameTable[x - 1, y] == gameTable[x + 1, y] && gameTable[x + 1, y] == gameTable[x + 2, y])
                return true;
        }

        if (x >= 2 && x <= gameTable.GetLength(0) - 2)
        {
            if (gameTable[x - 2, y] != -1 && gameTable[x - 2, y] == gameTable[x - 1, y] && gameTable[x - 1, y] == gameTable[x + 1, y])
                return true;
        }

        if (x >= 3 && x <= gameTable.GetLength(0) - 1)
        {
            if (gameTable[x - 3, y] != -1 && gameTable[x - 3, y] == gameTable[x - 2, y] && gameTable[x - 2, y] == gameTable[x - 1, y])
                return true;
        }

        // check vertical
        if (y >= 0 && y <= gameTable.GetLength(1) - 4)
        {
            if (gameTable[x, y + 1] != -1 && gameTable[x, y + 1] == gameTable[x, y + 2] && gameTable[x, y + 2] == gameTable[x, y + 3])
                return true;
        }

        if (y >= 1 && y <= gameTable.GetLength(1) - 3)
        {
            if (gameTable[x, y - 1] != -1 && gameTable[x, y - 1] == gameTable[x, y + 1] && gameTable[x, y + 1] == gameTable[x, y + 2])
                return true;
        }

        if (y >= 2 && y <= gameTable.GetLength(1) - 2)
        {
            if (gameTable[x, y - 2] != -1 && gameTable[x, y - 2] == gameTable[x, y - 1] && gameTable[x, y - 1] == gameTable[x, y + 1])
                return true;
        }

        if (y >= 3 && y <= gameTable.GetLength(1) - 1)
        {
            if (gameTable[x, y - 3] != -1 && gameTable[x, y - 3] == gameTable[x, y - 2] && gameTable[x, y - 2] == gameTable[x, y - 1])
                return true;
        }

        // check right cross
        if (x >= 0 && x <= gameTable.GetLength(0) - 4 && y >= 0 && y <= gameTable.GetLength(1) - 4)
        {
            if (gameTable[x + 1, y + 1] != -1 && gameTable[x + 1, y + 1] == gameTable[x + 2, y + 2] &&
                gameTable[x + 2, y + 2] == gameTable[x + 3, y + 3])
                return true;
        }

        if (x >= 1 && x <= gameTable.GetLength(0) - 3 && y >= 1 && y <= gameTable.GetLength(1) - 3)
        {
            if (gameTable[x - 1, y - 1] != -1 && gameTable[x - 1, y - 1] == gameTable[x + 1, y + 1] &&
                gameTable[x + 1, y + 1] == gameTable[x + 2, y + 2])
                return true;
        }

        if (x >= 2 && x <= gameTable.GetLength(0) - 2 && y >= 2 && y <= gameTable.GetLength(1) - 2)
        {
            if (gameTable[x - 2, y - 2] != -1 && gameTable[x - 2, y - 2] == gameTable[x - 1, y - 1] &&
                gameTable[x - 1, y - 1] == gameTable[x + 1, y + 1])
                return true;
        }

        if (x >= 3 && x <= gameTable.GetLength(0) - 1 && y >= 3 && y <= gameTable.GetLength(1) - 1)
        {
            if (gameTable[x - 3, y - 3] != -1 && gameTable[x - 3, y - 3] == gameTable[x - 2, y - 2] &&
                gameTable[x - 2, y - 2] == gameTable[x - 1, y - 1])
                return true;
        }

        // check left cross
        if (x >= 3 && x <= gameTable.GetLength(0) - 1 && y >= 0 && y <= gameTable.GetLength(1) - 4)
        {
            if (gameTable[x - 1, y + 1] != -1 && gameTable[x - 1, y + 1] == gameTable[x - 2, y + 2] &&
                gameTable[x - 2, y + 2] == gameTable[x - 3, y + 3])
                return true;
        }

        if (x >= 2 && x <= gameTable.GetLength(0) - 2 && y >= 1 && y <= gameTable.GetLength(1) - 3)
        {
            if (gameTable[x + 1, y - 1] != -1 && gameTable[x + 1, y - 1] == gameTable[x - 1, y + 1] &&
                gameTable[x - 1, y + 1] == gameTable[x - 2, y + 2])
                return true;
        }

        if (x >= 1 && x <= gameTable.GetLength(0) - 3 && y >= 2 && y <= gameTable.GetLength(1) - 2)
        {
            if (gameTable[x + 2, y - 2] != -1 && gameTable[x + 2, y - 2] == gameTable[x + 1, y - 1] &&
                gameTable[x + 1, y - 1] == gameTable[x - 1, y + 1])
                return true;
        }

        if (x >= 0 && x <= gameTable.GetLength(0) - 4 && y >= 3 && y <= gameTable.GetLength(1) - 1)
        {
            if (gameTable[x + 3, y - 3] != -1 && gameTable[x + 3, y - 3] == gameTable[x + 2, y - 2] &&
                gameTable[x + 2, y - 2] == gameTable[x + 1, y - 1])
                return true;
        }
        return false;
    }

    private bool IsSafeToPlay(int x, int y)
    {
        if (y == gameTable.GetLength(1) - 1)
            return true;

        //check horizontal
        if (x >= 0 && x <= gameTable.GetLength(0) - 4)
        {
            if (gameTable[x + 1, y + 1] == 0 && gameTable[x + 1, y + 1] == gameTable[x + 2, y + 1] &&
                gameTable[x + 2, y + 1] == gameTable[x + 3, y + 1])
                return false;
        }

        if (x >= 1 && x <= gameTable.GetLength(0) - 3)
        {
            if (gameTable[x - 1, y + 1] == 0 && gameTable[x - 1, y + 1] == gameTable[x + 1, y + 1] &&
                gameTable[x + 1, y + 1] == gameTable[x + 2, y + 1])
                return false;
        }

        if (x >= 2 && x <= gameTable.GetLength(0) - 2)
        {
            if (gameTable[x - 2, y + 1] == 0 && gameTable[x - 2, y + 1] == gameTable[x - 1, y + 1] &&
                gameTable[x - 1, y + 1] == gameTable[x + 1, y + 1])
                return false;
        }

        if (x >= 3 && x <= gameTable.GetLength(0) - 1)
        {
            if (gameTable[x - 3, y + 1] == 0 && gameTable[x - 3, y + 1] == gameTable[x - 2, y + 1] &&
                gameTable[x - 2, y + 1] == gameTable[x - 1, y + 1])
                return false;
        }
        // check right cross
        if (x >= 0 && x <= gameTable.GetLength(0) - 4 && y + 1 >= 0 && y + 1 <= gameTable.GetLength(1) - 4)
        {
            if (gameTable[x + 1, y + 2] == 0 && gameTable[x + 1, y + 2] == gameTable[x + 2, y + 3] &&
                gameTable[x + 2, y + 3] == gameTable[x + 3, y + 4])
                return false;
        }

        if (x >= 1 && x <= gameTable.GetLength(0) - 3 && y + 1 >= 1 && y + 1 <= gameTable.GetLength(1) - 3)
        {
            if (gameTable[x - 1, y] == 0 && gameTable[x - 1, y] == gameTable[x + 1, y + 2] &&
                gameTable[x + 1, y + 2] == gameTable[x + 2, y + 3])
                return false;
        }

        if (x >= 2 && x <= gameTable.GetLength(0) - 2 && y + 1 >= 2 && y + 1 <= gameTable.GetLength(1) - 2)
        {
            if (gameTable[x - 2, y - 1] == 0 && gameTable[x - 2, y - 1] == gameTable[x - 1, y] &&
                gameTable[x - 1, y] == gameTable[x + 1, y + 2])
                return false;
        }

        if (x >= 3 && x <= gameTable.GetLength(0) - 1 && y + 1 >= 3 && y + 1 <= gameTable.GetLength(1) - 1)
        {
            if (gameTable[x - 3, y - 2] == 0 && gameTable[x - 3, y - 2] == gameTable[x - 2, y - 1] &&
                gameTable[x - 2, y - 1] == gameTable[x - 1, y])
                return false;
        }

        // check left cross
        if (x >= 3 && x <= gameTable.GetLength(0) - 1 && y + 1 >= 0 && y + 1 <= gameTable.GetLength(1) - 4)
        {
            if (gameTable[x - 1, y + 2] == 0 && gameTable[x - 1, y + 2] == gameTable[x - 2, y + 3] &&
                gameTable[x - 2, y + 3] == gameTable[x - 3, y + 4])
                return false;
        }

        if (x >= 2 && x <= gameTable.GetLength(0) - 2 && y + 1 >= 1 && y + 1 <= gameTable.GetLength(1) - 3)
        {
            if (gameTable[x + 1, y] == 0 && gameTable[x + 1, y] == gameTable[x - 1, y + 2] &&
                gameTable[x - 1, y + 2] == gameTable[x - 2, y + 3])
                return false;
        }

        if (x >= 1 && x <= gameTable.GetLength(0) - 3 && y + 1 >= 2 && y + 1 <= gameTable.GetLength(1) - 2)
        {
            if (gameTable[x + 2, y - 1] == 0 && gameTable[x + 2, y - 1] == gameTable[x + 1, y] &&
                gameTable[x + 1, y] == gameTable[x - 1, y + 2])
                return false;
        }

        if (x >= 0 && x <= gameTable.GetLength(0) - 4 && y + 1 >= 3 && y + 1 <= gameTable.GetLength(1) - 1)
        {
            if (gameTable[x + 3, y - 2] == 0 && gameTable[x + 3, y - 2] == gameTable[x + 2, y - 1] &&
                gameTable[x + 2, y - 1] == gameTable[x + 1, y])
                return false;
        }
        return true;
    }
}
