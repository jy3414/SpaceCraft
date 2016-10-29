using UnityEngine;
using System.Collections;
using Photon;

public class NameAI : Photon.PunBehaviour
{
    public TextMesh textMesh;
    private string[] names = {"Bob", "Davis", "Jimmy", "Yinjun", "Larry",
        "Pirate Ship", "Raven", "Jay", "John", "Overwatch", "Warcraft", "CR7",
        "Owen", "Mark", "Football", "SpaceWar", "KFC", "Superman", "Warlock",
        "Invoker", "Dota", "Lol", "PoisonMilk", "Autumn Day", "Mage", "BOSS",
        "Givenchy", "Spiderman", "Arthas", "Alan Turing"};
    
    // Use this for initialization
    void Start()
    {
        textMesh.text = RandomName();
        textMesh.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {

    }

    string RandomName()
    {
        string name;
        var randomInt = Random.Range(0, 30);
        name = names[randomInt];
        return name;
    }

}