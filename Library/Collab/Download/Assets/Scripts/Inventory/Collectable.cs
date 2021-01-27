using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectType type = CollectType.Wood;

    [Range(1,15)]
    public int collectAmountRandom = 1;

    public void Collect(Resources resources)
    {
        resources.AddResource(type, Random.Range(1, collectAmountRandom+1));
    }
}

public enum CollectType { Wood, Food}