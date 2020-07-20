using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        START, MENU, PLAY, PAUSE, GAMEOVER, WIN
    }
    public State currentState = State.START;

    public enum Level
    {
        EASY, MEDIUM, HARD
    }
    public Level currentLevel;

    public static GameManager instance; // will hold a reference to the first AudioManager created

    public MenuHandler mainMenu, gameOverMenu;

    public GameObject gameStatus;

    public PlayerController player;

    public GameObject[] prefabs;

    public float spawnProbability = 0.97f;

    private GameObject tom;
    private void Awake()
    {
        if (instance == null)
        {     // if the instance var is null this is first AudioManager
            instance = this;        //save this AudioManager in instance 
        }
        else
        {
            Destroy(gameObject);    // this isnt the first so destroy it
            return;                 // since this isn't the first return so no other code is run
        }
        DontDestroyOnLoad(gameObject); // do not destroy me when a new scene loads
    }

    // Start is called before the first frame update
    void Start()
    {
        tom = GameObject.FindGameObjectWithTag("Tom");
        currentLevel = Level.EASY;
        StartCoroutine(SetState(State.MENU));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update(): " + currentState);
        if (Random.value > spawnProbability)
        {
            // neues Objekt Spawnen:
            GameObject clone = Instantiate(
                prefabs[Random.Range(0, prefabs.Length - 1)],
                new Vector3(Random.Range(-7.5f, 7.5f), 6, 0),
                prefabs[0].transform.rotation);

            float scale = Random.Range(0.5f, 1.5f);
            clone.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    public IEnumerator SetState(State state)
    {
        Debug.Log(Time.time + " : " + " currentState = " + currentState.ToString());
        Debug.Log(Time.time + " : " + " state = " + state.ToString());

        switch (state)
        {
            case State.MENU:

                if (currentState == State.GAMEOVER)
                {
                    yield return StartCoroutine(gameOverMenu.HideMenu(1));
                    gameOverMenu.gameObject.SetActive(false);
                }

                tom.SetActive(true);
                tom.GetComponent<SpringJoint2D>().enabled = true;
                tom.transform.GetComponent<TargetJoint2D>().enabled = false;

                mainMenu.gameObject.SetActive(true);
                yield return StartCoroutine(mainMenu.ShowMenu());

                break;

            case State.PLAY:

                currentState = State.PLAY;

                tom.GetComponent<SpringJoint2D>().enabled = false;
                tom.transform.GetComponent<TargetJoint2D>().enabled = true;
                yield return StartCoroutine(mainMenu.HideMenu(0));
                mainMenu.gameObject.SetActive(false);
                tom.SetActive(false);

                gameStatus.SetActive(true);

                player.gameObject.SetActive(true);
                yield return StartCoroutine(player.ShowPlayer());

                //spawnProbability = 0.97f;

                break;

            case State.GAMEOVER:

                currentState = State.GAMEOVER;
                // Players Tod synchron animieren:
                yield return StartCoroutine(player.RemovePlayer());
                player.gameObject.SetActive(false);

                gameStatus.SetActive(false);

                // Game Over Menu einblenden:
                gameOverMenu.gameObject.SetActive(true);
                yield return StartCoroutine(gameOverMenu.ShowMenu());
                break;
        }
        currentState = state;
    }
}
