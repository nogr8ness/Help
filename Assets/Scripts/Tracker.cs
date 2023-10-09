using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI controlsTxt, massTxt;
    [SerializeField] private float lightMass, normalMass, heavyMass;

    private int reversedScore;
    private int massScore;

    public static float reversed;
    public static float mass;

    private void Awake()
    {
        reversedScore = GenerateControlScore();
        massScore = GenerateMassScore();

        reversed = 1;
        mass = normalMass;
    }

    public void ResetAll()
    {
        reversedScore = GenerateControlScore();
        massScore = GenerateMassScore();

        reversed = 1;
        mass = normalMass;

        controlsTxt.text = "Controls: Normal";
        controlsTxt.color = Color.white;

        massTxt.text = "Mass: Normal";
        massTxt.color = Color.white;
    }

    private void Update()
    {
        if (Player.score >= reversedScore)
            RandomizeControls();

        if (Player.score >= massScore)
            RandomizeMass();

        print("Reversed: " + reversedScore);
        print("Mass: " + massScore);
    }

    private int GenerateControlScore()
    {
        return Player.score + Random.Range(7, 15);
    }

    private void RandomizeControls()
    {
        int num = Random.Range(0, 2);

        if (num == 0)
        {
            reversed = 1;
            controlsTxt.text = "Controls: Normal";
            controlsTxt.color = Color.white;
        }
            
        else
        {
            reversed = -1;
            controlsTxt.text = "Controls: Reversed";
            controlsTxt.color = Color.red;
        }
            

        reversedScore = GenerateControlScore();
    }
    
    private int GenerateMassScore()
    {
        return Player.score + Random.Range(10, 17);
    }

    private void RandomizeMass()
    {
        int num = Random.Range(0, 3);

        if (num == 0)
        {
            mass = lightMass;
            massTxt.text = "Mass: Skinny";
            massTxt.color = Color.green;
        }
            
        else if (num == 1)
        {
            mass = normalMass;
            massTxt.text = "Mass: Normal";
            massTxt.color = Color.white;
        }
            
        else
        {
            mass = heavyMass;
            massTxt.text = "Mass: Thicc";
            massTxt.color = Color.red;
        }
            

        massScore = GenerateMassScore();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
