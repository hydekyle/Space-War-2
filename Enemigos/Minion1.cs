using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion1 : MonoBehaviour
{
    public Sprite sprite;
    public Enemy enemy;

    void Start()
    {
        enemy = new Enemy("Minion1", 20, this.gameObject);
    }

    void Update()
    {
        enemy.MoveTo(ScreenPosition.TopRight());
    }
}
