using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int maxSize;
    public int currentSize;
    public GameObject snakePrefab;
    public Snake head;
    public Snake Tail;
    public int xBound;
    public int yBound;
    public GameObject foodPrefab;
    public GameObject currentFood;
    public int score;
    public Text ScoreValue;

    /// <summary>
    /// Defines the direction moving
    /// </summary>
    public int NESW;

    public Vector2 nexPos;

    void OnEnable()
    {
        Snake.Hit += OnCollision;
    }

    void OnDisable()
    {
        Snake.Hit -= OnCollision;
    }

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("TimerInvoke", 0, 0.5f);
        FoodProcessing();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeDirectionChange();
    }

    void TimerInvoke()
    {
        Movement();

        if (currentSize >= maxSize)
        {
            TailProcessing();
        }
        else
        {
            currentSize++;
        }
    }

    /// <summary>
    /// Process the Movement
    /// </summary>
    private void Movement()
    {
        GameObject gameObject;

        nexPos = head.transform.position;

        switch (NESW)
        {
            case 0:
                nexPos = new Vector2(nexPos.x, nexPos.y + 1);
                break;
            case 1:
                nexPos = new Vector2(nexPos.x + 1, nexPos.y);
                break;
            case 2:
                nexPos = new Vector2(nexPos.x, nexPos.y - 1);
                break;
            case 3:
                nexPos = new Vector2(nexPos.x - 1, nexPos.y);
                break;
            default:
                break;
        }

        gameObject = (GameObject)Instantiate(snakePrefab, nexPos, transform.rotation);

        head.Next = gameObject.GetComponent<Snake>();
        head = gameObject.GetComponent<Snake>();

        return;
    }

    void ComputeDirectionChange()
    {
        if (NESW != 2 && Input.GetKeyDown(KeyCode.W))
        {
            NESW = 0;
        }

        if (NESW != 3 && Input.GetKeyDown(KeyCode.D))
        {
            NESW = 1;
        }

        if (NESW != 0 && Input.GetKeyDown(KeyCode.S))
        {
            NESW = 2;
        }

        if (NESW != 1 && Input.GetKeyDown(KeyCode.A))
        {
            NESW = 3;
        }
    }

    void TailProcessing()
    {
        Snake snake = Tail;
        Tail = Tail.Next;

        snake.RemoveTail();
    }

    void FoodProcessing()
    {
        int xPosition = Random.Range(-xBound, xBound);
        int yPosition = Random.Range(-yBound, yBound);

        currentFood = (GameObject)Instantiate(foodPrefab, new Vector2(xPosition, yPosition), transform.rotation);

        StartCoroutine(CheckRender(currentFood));
    }

    IEnumerator CheckRender(GameObject gameObject)
    {
        // Wait for the End of the Frame
        yield return new WaitForEndOfFrame();

        if (!gameObject.GetComponent<Renderer>().isVisible && gameObject.tag.ToLowerInvariant() == "food")
        {
            // Destroy Food if Spawned outside Camera.
            Destroy(gameObject);
            FoodProcessing();
        }
    }

    /// <summary>
    /// Collision Event
    /// </summary>
    /// <param name="sender">Tag which was collided</param>
    void OnCollision(string sender)
    {
        if (sender.ToLowerInvariant() == "food")
        {
            FoodProcessing();
            maxSize++;

            // Update Score
            score++;
            ScoreValue.text = score.ToString();

            int playerScore = PlayerPrefs.GetInt("HighScore");

            if (score > playerScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }

        if (sender.ToLowerInvariant() == "snake")
        {
            // Cancle Timer
            CancelInvoke("TimerInvoke");
            Exit();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
