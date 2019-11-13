using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public BulletsScriptables table_bullets;
    public ShipsScriptables table_ships;
    public Ship equipped_ship;
    public Bullet equipped_bullet;
    public bool IsPaused = true;
    public GameObject testShit;
    public Transform playerT;
    PositionScreenStructure posiScreen = new PositionScreenStructure();

    private void Awake()
    {
        Instance = this;
        GenerateScreenPositionStruct();
    }

    void GenerateScreenPositionStruct()
    {
        foreach (Transform t in transform.Find("MyGrid"))
        {
            switch (t.name)
            {
                case "TopLeft": posiScreen.topLeft = t.position; break;
                case "TopMid": posiScreen.topMid = t.position; break;
                case "TopRight": posiScreen.topRight = t.position; break;

                case "MidLeft": posiScreen.midLeft = t.position; break;
                case "MidMid": posiScreen.midMid = t.position; break;
                case "MidRight": posiScreen.midRight = t.position; break;

                case "BotLeft": posiScreen.botLeft = t.position; break;
                case "BotMid": posiScreen.botMid = t.position; break;
                case "BotRight": posiScreen.botRight = t.position; break;
                default: return;
            }
        }
        //var values = Enum.GetValues(typeof(PositionSpace));
    }

    public Vector3 GetScreenPosition(PositionSpace positionSpace)
    {
        switch (positionSpace)
        {
            case PositionSpace.BotMid: return posiScreen.botMid;
            case PositionSpace.BotLeft: return posiScreen.botLeft;
            case PositionSpace.BotRight: return posiScreen.botRight;

            case PositionSpace.MidLeft: return posiScreen.botLeft;
            case PositionSpace.MidRight: return posiScreen.midRight;
            case PositionSpace.MidMid: return posiScreen.midMid;

            case PositionSpace.TopLeft: return posiScreen.topLeft;
            case PositionSpace.TopMid: return posiScreen.topMid;
            case PositionSpace.TopRight: return posiScreen.topRight;

            default: return Vector3.zero;
        }
    }

    public Ship GetShip(string ID)
    {
        return table_ships.ships.Find(ship => ship.ID == ID);
    }

    public Bullet GetBullet(string ID)
    {
        return table_bullets.bullets.Find(bullet => bullet.ID == ID);
    }

    public Bullet GetEnemyBullet(string ID)
    {
        return table_bullets.enemyBullets.Find(bullet => bullet.ID == ID);
    }
}
