using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{

    public static Game obj;

    public int maxLives = 3; //maximo de vidas permitido

    public bool gamePaused = false; // variable para ver si el juego esta en pausa
    public int score = 0; // puntaje

    void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update

    void Start()
    {
        gamePaused = false;
    }

    public void addScore(int scoreGive) // funcion para añadir puntaje
    {
        score += scoreGive;
    }

    public void gameOver()//funcion para reiniciar la escena actual
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        obj = null;
    }

    
}
