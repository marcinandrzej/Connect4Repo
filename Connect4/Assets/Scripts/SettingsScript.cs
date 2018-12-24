using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public static SettingsScript instance;

    private bool enemyAI;

    public bool EnemyAI
    {
        get
        {
            return enemyAI;
        }

        set
        {
            enemyAI = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
