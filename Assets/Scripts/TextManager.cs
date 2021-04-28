using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public static TextManager instance;

    public Text scoreText;
    public Text lineText;
    public Text levelText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateScoreText(string input)
    {
        scoreText.text = input;
    }

    public void updateLineText(string input)
    {
        lineText.text = input;
    }

    public void updateLevelText(string input)
    {
        levelText.text = input;
    }
}
