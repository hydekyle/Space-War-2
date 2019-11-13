using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public static CanvasMenu Instance;
    public Slider vida_slider;
    public Image spriteShip_placeholder;
    List<Ship> list_ship;
    public Text text_fuerza, text_velocidad, text_CD;
    int pointer = 0;
    float vidaUI = 1f;

    private void Awake()
    {
        Instance = Instance ?? this;
    }

    private void Start()
    {
        //IniShipWindow();
    }

    private void Update()
    {
        //ChooseShip();
        vida_slider.value = Mathf.Lerp(vida_slider.value, vidaUI, Time.deltaTime * 2);
    }

    public void SetVidaUI(float vida)
    {
        vidaUI = vida / 100;
    }

    void ChooseShip()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) SeleccionarNave(++pointer);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) SeleccionarNave(--pointer);
    }

    void SeleccionarNave(int s)
    {
        try
        {
            Ship nave = list_ship[Mathf.Abs(s)];
            ColocarCosas(nave);
        }
        catch
        {
            //Reiniciar al llegar al limite de la lista
            pointer = 0;
            ColocarCosas(list_ship[pointer]);
        }

    }

    void ColocarCosas(Ship nave)
    {
        text_fuerza.text = nave.stats.damage.ToString();
        text_CD.text = nave.stats.cd_reduction.ToString();
        text_velocidad.text = nave.stats.velocity.ToString();
        spriteShip_placeholder.sprite = nave.sprite;
    }

    public void IniShipWindow()
    {
        list_ship = new List<Ship>();
        ShipsScriptables ships_tables = GameManager.Instance.table_ships;
        foreach (Ship s in ships_tables.ships)
        {
            list_ship.Add(s);
        }
        SeleccionarNave(0);
    }
}
