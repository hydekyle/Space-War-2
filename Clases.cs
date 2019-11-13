using System.Runtime.CompilerServices;
using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public enum PositionSpace { TopLeft, TopMid, TopRight, MidLeft, MidMid, MidRight, BotLeft, BotMid, BotRight };

public struct PositionScreenStructure
{
    public Vector3 topLeft, topMid, topRight, midLeft, midMid, midRight, botLeft, botMid, botRight;
}

public static class ScreenPosition
{
    public static Vector3 TopLeft()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.TopLeft);
    }
    public static Vector3 TopMid()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.TopMid);
    }
    public static Vector3 TopRight()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.TopRight);
    }

    public static Vector3 MidLeft()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.MidLeft);
    }
    public static Vector3 MidMid()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.MidMid);
    }
    public static Vector3 MidRight()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.MidRight);
    }

    public static Vector3 BotLeft()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.BotLeft);
    }
    public static Vector3 BotMid()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.BotMid);
    }
    public static Vector3 BotRight()
    {
        return GameManager.Instance.GetScreenPosition(PositionSpace.BotRight);
    }
}

public class Player
{
    public string name;
    public int ID;
}

[Serializable]
public class Enemy : MonoBehaviour
{
    public string _name;
    public int _health;
    public float _speed = 2f;
    public Sprite _sprite;
    public GameObject _myGO;
    public Transform _transform;
    public Enemy(string name, int health, GameObject myGO)
    {
        this._name = name;
        this._health = health;
        this._myGO = myGO;
        this._transform = myGO.transform;
        Debug.Log("vaya");
    }
    public void Die()
    {
        Debug.Log("Me muero");
        Destroy(_myGO);
    }

    public void MoveTo(Vector3 targetPosition)
    {
        _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, Time.deltaTime * _speed);
    }

}


[Serializable]
public struct Bullet
{
    public string ID;
    public GameObject particles_o;
    public Stats stats;
}

[Serializable]
public struct Ship
{
    public string ID;
    public Sprite sprite;
    public Stats stats;
}

[Serializable]
public struct Stats
{
    public float deffense;
    public float damage;
    public float velocity;
    public float cd_reduction;
}

public struct Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class Rutas
{
    public static string GetCosasRuta()
    {
        return "toma";
    }
}

