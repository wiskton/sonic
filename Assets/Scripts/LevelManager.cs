using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    private int moedasAtual = 0;
    private bool gameOver = false;

    private float segundos;
    private int segundosToInt;
    private int minutos;

    public Text minutosText;
    public Text segundosText;
    public Text moedasText;

    void Awake() {
        if(levelManager == null){
            levelManager = this;
        } else if (levelManager != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver){
            segundos += Time.deltaTime;
            if (segundos >= 60){
                segundos = 0;
                minutos++;
                minutosText.text = minutos.ToString();
            }
            segundosToInt = (int)segundos;
            segundosText.text = segundosToInt.ToString();
        }
    }

    public void SetMoedas(){
        moedasAtual++;
        moedasText.text = moedasAtual.ToString();
    }

    public int GetMoedas(){
        return moedasAtual;
    }

    public void ResetMoedas() {
        moedasAtual = 0;
        moedasText.text = moedasAtual.ToString();
    }
}
