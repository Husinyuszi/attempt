using UnityEngine;
using UnityEngine.SceneManagement;

 class GameManager : MonoBehaviour
{
    int level;
    int lives;
    int score;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        NewGame();
    }

    void NewGame()
    {
        lives = 1;
        score = 0;

        LoadLevel(1);
    }

    void LoadLevel(int index)
    {
        level = index;

        Camera camera = Camera.main;
        
        if (camera!=null) 
        {
            camera.cullingMask = 0; 
        }

        Invoke(nameof(LoadScene), 1f);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(level);
    }

    public void LevelComplete()
    {
        score += 100;
        int nextLevel = level + 1;

        if (nextLevel< SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(nextLevel);
        }
        else
        {
            LoadLevel(1);
        }

    }

    public void LevelFailed()
    {
        lives--;

        if (lives<=0) 
        {
            NewGame();
        }
        else
        {
            LoadLevel(level);
        }
    }
}
