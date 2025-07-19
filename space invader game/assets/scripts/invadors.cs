using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Invadors : MonoBehaviour
{
    public int rows = 5;
    public int cols = 7;
    public inavador[] prefabs;



      

    public float spacingX = 2.0f;
    public float spacingY = 2.0f;

    public projectile missileprefabe;

    



    public GameObject restartButtonObject; 

    public GameObject winTextObject;

    public int numkilled { get; private set; }
    public int total => rows * cols;
    public int amountalive => total - numkilled;
    public float percentkilled => (float)numkilled / (float)total;

    public AnimationCurve speed ;//chnaged form float to animationcurve for smother change in speed as the killed invadors more as a challenging game 




    private Vector3 direc = Vector2.right;

    public float missilerate = 1.0f;

    [Header("UI")]
    public TextMeshProUGUI scoreText; 

    void Awake()
    {


        winTextObject.SetActive(false);

        restartButtonObject.SetActive(false);


        float width = (cols - 1) * spacingX;
        float height = (rows - 1) * spacingY;
        Vector3 start = new Vector3(-width / 2f, height / 2f, 0f);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                inavador prefab = prefabs[row % prefabs.Length];
                Vector3 position = start + new Vector3(col * spacingX, -row * spacingY, 0f);
                inavador invador = Instantiate(prefab, transform);
                invador.killed += invadorkilled;
                invador.transform.localPosition = position;
            }
        }

        UpdateScoreUI(); 
    }

    private void Start()
    {
        InvokeRepeating(nameof(missileattack), missilerate, missilerate);
    }

    private void Update()//for the move of the invadors right and keft and flip 
    {
        transform.position += direc * speed.Evaluate(percentkilled) * Time.deltaTime;

        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy) continue;

            Vector3 viewPos = Camera.main.WorldToViewportPoint(child.position);
            if (viewPos.x < 0.05f || viewPos.x > 0.95f)
            {
                FlipDirection();
                break;
            }
        }
    }

    private void missileattack()//whenever teh invaders is bigger the atack is less as more killed more attack 
    {
        foreach (Transform child in transform)
        {
            if (!child.gameObject.activeInHierarchy) continue;

            if (Random.value < (1.0f) / (float)amountalive)
            {
                Instantiate(missileprefabe, child.position, Quaternion.identity);
                break;
            }
        }
    }

    private void FlipDirection()
    {
        direc = -direc;
        Vector3 position = this.transform.position;
        position.y -= 0.1f;
        transform.position = position;
    }

    private void invadorkilled()
    {
        numkilled++;
        UpdateScoreUI();

        if (numkilled >= total)
        {
            winTextObject.SetActive(true);
            restartButtonObject.SetActive(true);

        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " +( numkilled*10).ToString();
           
        }
    }


    public bool AllInvadersKilled()
    {
        return numkilled >= total;
    }

}
