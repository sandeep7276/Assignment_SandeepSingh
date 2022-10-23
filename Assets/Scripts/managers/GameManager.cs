
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SnakeGame;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{


[Header("Snake Configuration")]

    public SnakeCollision snakeCollision;
    [SerializeField] private float snakeSpeedMultipier = 1f;
    [SerializeField] private float snakeTurnMultipier = 3f;
    [SerializeField] private int initSnakeSize = 3;
    [SerializeField] private GameObject snakeHead;
    [SerializeField] private GameObject snakeTail;
    [SerializeField] private Vector2 foodSpawnArea;
    [SerializeField] private Text scoreUI;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private Camera cam;
    [SerializeField] private float smoothTime = 0.4f;
    [SerializeField] private List<FoodItem> foodList = new List<FoodItem>();
    




    private float additionalSpeed = 1;
    private bool stopGame = false;
    private int score = 0;
    private int currentFoodId = -1;
    private int prevFoodId = -1; 
    private List<GameObject> snakeBodyPartsList = new List<GameObject>();



  
    void Start()
    {
  
        for (int i = 0; i< initSnakeSize; i++)
        {
            SpawnTail();
        }

        
        snakeCollision.collision = CollisionEnter;
        Invoke("SpawnFood", 2f);

    }

    void Update()
    {
        if (!stopGame)
        {
            HeadMovement();
            TailMovement();
        }
    }

    private void LateUpdate()
    {
        if (!stopGame)
        {
            CameraMovement();
        }
    }



    #region "Instantiates Methods"
    void SpawnTail()
    {
        GameObject body = Instantiate(snakeTail);
        body.transform.SetParent(snakeHead.transform.parent);
        snakeBodyPartsList.Add(body);
    }
    void SpawnFood()
    {
        int idx = Random.Range(0, foodList.Count);
        GameObject Food = Instantiate(foodList[idx].foodItem.Item, new Vector3(Random.Range(-foodSpawnArea.x , foodSpawnArea.x), 0, Random.Range(-foodSpawnArea.y, foodSpawnArea.y)), transform.rotation);
        currentFoodId = idx;
    }

    #endregion

    #region "Movement Methods"
    void HeadMovement()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            additionalSpeed *= 2;
        }


        snakeHead.transform.position += snakeHead.transform.forward * snakeSpeedMultipier * additionalSpeed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") != 0)
        {
            snakeHead.transform.Rotate(Vector3.up * snakeTurnMultipier * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
        additionalSpeed = 1;
    }

    void TailMovement()
    { 
        for(int i = 0; i < snakeBodyPartsList.Count; i++)
        {
           if(i == 0)
            {
                snakeBodyPartsList[0].transform.position = Vector3.Lerp(snakeBodyPartsList[0].transform.position, snakeHead.transform.position , snakeSpeedMultipier * Time.smoothDeltaTime);
            }
            else
            {
                snakeBodyPartsList[i].transform.position = Vector3.Lerp(snakeBodyPartsList[i].transform.position, snakeBodyPartsList[i - 1].transform.position, snakeSpeedMultipier * Time.smoothDeltaTime);
            }
        }
    }

    void CameraMovement()
    {
        Vector3 camVelocity = Vector3.zero;
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position,
            new Vector3(snakeHead.transform.position.x, 21, snakeHead.transform.position.z - 18),
            ref camVelocity, smoothTime);
    }

    #endregion
    
    #region "Events Methods "
    
   
    void CollisionEnter(CollisionType ct)
    {
        if(ct == CollisionType.Food)
        {
            if(currentFoodId == prevFoodId)
            {
                ScoreUpdate(foodList[currentFoodId].foodItem.FoodValue * 2);
            }
            else
            {
                ScoreUpdate(foodList[currentFoodId].foodItem.FoodValue);
            }

            prevFoodId = currentFoodId;
            SpawnFood();
            SpawnTail();
        }
        if (ct == CollisionType.Wall)
        {
            stopGame = true;
            SaveScore();
            gameoverUI.SetActive(true);
        }
    }

    void ScoreUpdate(int _score)
    {
        score += _score;
        scoreUI.text = $" Score : {score.ToString()}";
    }
    void SaveScore()
    {
        IOOperation io = new IOOperation(DataConst.userFilePath);
        string old = io.LoadData();
        int oldSco = int.Parse(old);
        if (oldSco < score)
            io.SavData(score);
    }

    public void LoadMenuScene()
    {

        SceneManager.LoadScene(DataConst.MainSceneName, LoadSceneMode.Single);
    }

    #endregion

}
