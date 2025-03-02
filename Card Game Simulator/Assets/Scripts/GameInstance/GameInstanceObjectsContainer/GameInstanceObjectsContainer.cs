using System.Collections.Generic;
using UnityEngine;

public class GameInstanceObjectsContainer : MonoBehaviour
{
    public GameObject Table { get; set; }
    public List<GameObject> Decks { get; set; }
    public List<GameObject> CardPlacementLocations { get; set; } 
    public List<GameObject> PlayerHands { get; set; }
}
