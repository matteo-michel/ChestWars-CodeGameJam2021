using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public CollectType type;

    [Range(1,15)]
    public int collectAmountRandom = 1;

    [Range(1, 100)]
    public int resistanceRandom = 4;
    private int realResistance;

    private void Start()
    {
        realResistance = Random.Range((int)(resistanceRandom / 2.3f), (int)(resistanceRandom / 1.5f));
    }

    public void Collect(Resources resources)
    {
        int rand = Random.Range(1, collectAmountRandom + 1);
        if (realResistance - rand > 0)
        {
            resources.AddResource(type, rand);
            realResistance -= rand;
        } else
        {
            resources.AddResource(type, realResistance);
            resources.GetComponent<PlayerInteract>().HandleTriggerExit(transform);
            Destroy(gameObject);
        }
    }
}

public enum CollectType { Wood, Food, Stone}