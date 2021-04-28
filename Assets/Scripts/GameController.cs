using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject[] tetrominosPrefabs;
    public Transform spawnPoint;
    public bool mustCreateNewTetromino = false;
    private bool readyToCreateNewTetromino = false;

    private float timer;
    public float period = 0.2f;
    private bool ready = false;

    [SerializeField]
    private uint currentScore;
    [SerializeField]
    private uint currentLevel;
    [SerializeField]
    private uint currentLines;

    private GameObject nextTetromino;
    public Displayer displayer;
    public SpawnPoint sp;

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

    void Start()
    {
        nextTetromino = getRandomTetromino();
        //displayer.updateSprite(nextTetromino);
        spawnNextTetromino();
        refreshText();
    }

    private void Update()
    {
        if (ready && isAllSleeping())
        {

            if (readyToCreateNewTetromino)
            {
                spawnNextTetromino();
                readyToCreateNewTetromino = false;
            }
            else if (mustCreateNewTetromino)
            {
                LineController.instance.DestroyLine();
                updateScore(LineController.instance.nbLineDestroyed);
                readyToCreateNewTetromino = true;
                mustCreateNewTetromino = false;
            }

            ready = false;
        }
    }

    private void FixedUpdate()
    {
        if (!ready)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= period)
            {
                timer = 0;
                ready = true;
            }
        }
    }

    GameObject getRandomTetromino()
    {
        return tetrominosPrefabs[Random.Range(0, tetrominosPrefabs.Length - 1)];
    }

    public void spawnNextTetromino()
    {
        if (sp.nbPieceInTopSection() > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Instantiate(nextTetromino, spawnPoint);
        nextTetromino = getRandomTetromino();
        displayer.updateSprite(nextTetromino);
    }

    private bool isAllSleeping()
    {
        foreach (GameObject fallen_compo in GameObject.FindGameObjectsWithTag("fallen block"))
        {
            if (fallen_compo.GetComponent<CheckMovement>().isMoving)
                return false;
        }
        return true;
    }

    private void refreshText()
    {
        TextManager.instance.updateScoreText(currentScore.ToString());
        TextManager.instance.updateLineText(currentLines.ToString());
        TextManager.instance.updateLevelText(currentLevel.ToString());
    }

    private void updateScore(uint nbLine)
    {
        currentLines += nbLine;

        if((currentLines - 10 * currentLevel) >= 10)
            currentLevel += 1;

        uint toAdd = 0;

        if (nbLine < 0)
        {
            currentScore = 0;
        }
        else
        {

            if (nbLine == 1)
                toAdd = 40;
            if (nbLine == 2)
                toAdd = 100;
            if (nbLine == 3)
                toAdd = 300;
            if (nbLine == 4)
                toAdd = 1200;

            currentScore += toAdd * (currentLevel + 1);

            refreshText();
        }
    }
}
