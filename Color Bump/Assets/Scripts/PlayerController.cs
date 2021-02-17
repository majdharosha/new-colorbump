using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;
    private Vector3 lastMousePos;
    public float sensitivity = .16f, clampDelta = 42f;
    public float min = -5f, max = 5;
    public GameObject trail; 
    public bool canMove, gameover, finish;
    public GameObject breakable;
    public GameObject gameovertext, taptoreplay; 

    void Awake ()
    {
        rb = GetComponent<Rigidbody>();

        
    }

    



    void Update()
     {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min, max), transform.position.y, transform.position.z);
        if (canMove)
        transform.position += FindObjectOfType<CameraFollow>().camvelocity;



        if (!canMove && gameover)
        {
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
           
        }
        else if (!canMove && !finish)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<GameManager>().RemoveUI();
                canMove = true;
            }
        }


        


    }



    void FixedUpdate ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
           
        }

        if (canMove)
        {
            if (Input.GetMouseButton(0))
            {

                Vector3 vector = lastMousePos - Input.mousePosition;
                lastMousePos = Input.mousePosition;
                vector = new Vector3(vector.x, 0, vector.y);

                Vector3 moveForce = Vector3.ClampMagnitude(vector, clampDelta);

                rb.AddForce(-moveForce * sensitivity - rb.velocity / 5, ForceMode.VelocityChange);



            }
        }
        rb.velocity.Normalize(); 

    }





   




    void GameOver ()
    {

        GameObject shattershpere = Instantiate(breakable, transform.position, Quaternion.identity);
        foreach (Transform o in shattershpere.transform )
        {
            o.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5, ForceMode.Impulse);
        }
        canMove = false;
        gameover = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        trail.SetActive(false);
        GetComponent<AudioSource>().Play();


        StartCoroutine(GameOvertxt());

       

        Time.timeScale = .3f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
         
    }

   

    IEnumerator GameOvertxt()
    {
        yield return new WaitForSeconds(1);
        taptoreplay.SetActive(true);
        gameovertext.SetActive(true);
    }


    IEnumerator NextLevel()
    {
        finish = true;
        canMove = false;
        GameObject.Find("FINISH").GetComponent<AudioSource>().Play();
        
        yield return new WaitForSeconds(2);
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    


    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Enemy")
        {
             if (!gameover) GameOver(); 
        }
    }


    void OnTriggerEnter (Collider target)
    {
        if (target.gameObject.name == "Finish")
        {
            StartCoroutine(NextLevel());
        }
    }



}
