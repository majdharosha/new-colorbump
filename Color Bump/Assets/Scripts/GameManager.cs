using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private GameObject swipe;
    public GameObject hand;
    private GameObject player, finish; 
   // private Text nLevel, cLevel;
    private Image fill;
    private float startdistance, distance;
   // private TextMesh levelNo;
    private int level;
    public GameObject menu;
    public bool gameIsPaused = false;
     


     void Awake ()
    {
      //  nLevel = GameObject.Find("NextLevelText").GetComponent<Text>();
       // cLevel = GameObject.Find("CurrentLevelText").GetComponent<Text>();
        fill = GameObject.Find("fill").GetComponent<Image>();
        swipe = GameObject.Find("SwipeToStart");
       // levelNo = GameObject.Find("LEVEL").GetComponent<TextMesh>();
        player = GameObject.Find("Player"); 
        finish = GameObject.Find("Finish");
        

    }


    private void Start()
    {

        
        level = PlayerPrefs.GetInt("Level");
       // levelNo.text = "LEVEL " + level;
      //  nLevel.text = level + 1 + "";
       // cLevel.text = level.ToString();


        startdistance = Vector3.Distance(player.transform.position, finish.transform.position);
       
      


    }




    void Update()
    {

        distance = Vector3.Distance(player.transform.position, finish.transform.position);
        if (player.transform.position.z < finish.transform.position.z)
            fill.fillAmount = 1 - distance / startdistance; 



        if (Input.GetMouseButtonDown(0))
        {
            swipe.SetActive(false);
            
           
        }


       



    }

    public void RemoveUI()
    {
        hand.GetComponent<Image>().enabled = false;
    }



    public void Resume ()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        gameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
        gameIsPaused = true; 

    }

    public void Exit ()
    {
        Application.Quit(); 
    }

    public void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }



}
