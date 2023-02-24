using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Variables
    [field: SerializeField] public GameObject DeadScreen { get; protected set; }
    [field: SerializeField] public Image DashAvailabilityImage { get; protected set; }
    [field: SerializeField] public TextMeshProUGUI LifeText { get; protected set; }
    [field: SerializeField] public TextMeshProUGUI MaxLifeText { get; protected set; }
    [field: SerializeField] public TextMeshProUGUI SussyText { get; protected set; }
    [field: SerializeField] public Color DashInUseColor { get; protected set; } = Color.red;
    [field: SerializeField] public Color DashInCooldownColor { get; protected set; } = Color.red * .5f;
    [field: SerializeField] public Color DashAvailableColor { get; protected set; } = Color.green;

    public Player Player { get; protected set; }
    #endregion

    #region Unity Functions
    protected virtual void Awake()
    {
        if (!(DashAvailabilityImage && DeadScreen && LifeText && MaxLifeText && SussyText))
        {
            Debug.LogWarning("HUD is missing variables.");

            enabled = false;

            return;
        }

        DashAvailabilityImage.color = Color.green;
        Player = GameManager.Instance.PlayerController.Player;
    }
    
    protected virtual void Start()
    {
        MaxLifeText.text = "/" + Player.Data.MaxLife;
        LifeText.text = Player.CooldownedLife.Life.ToString();
    }

    protected virtual void OnEnable()
    {
        var dash = Player.Dash;
        
        dash.OnSkillUse += DashInUse;
        dash.OnSkillCompleted += DashUnavailable;
        dash.OnCooldownFinished += DashAvailable;
        Player.CooldownedLife.OnDeath += DeathScreen;
        Player.CooldownedLife.OnLifeChange += UpdateLife;
        Player.ComboDelay.OnComboComplete += Combo;
    }
    
    protected virtual void OnDisable()
    {
        var dash = Player.Dash;
        
        dash.OnSkillUse -= DashInUse;
        dash.OnSkillCompleted -= DashUnavailable;
        dash.OnCooldownFinished -= DashAvailable;
        Player.CooldownedLife.OnDeath -= DeathScreen;
        Player.CooldownedLife.OnLifeChange -= UpdateLife;
        Player.ComboDelay.OnComboComplete -= Combo;
    }
    #endregion Unity Functions

    #region HUD
    protected void DashInUse()
    {
        DashAvailabilityImage.color = Color.red;
    }

    protected void DashUnavailable()
    {
        DashAvailabilityImage.color = Color.red * .5f;
    }

    protected void DashAvailable()
    {
        DashAvailabilityImage.color = Color.green;
    }

    protected void UpdateLife()
    {
        LifeText.text = Player.CooldownedLife.Life.ToString(CultureInfo.CurrentCulture);
    }

    protected async void Combo()
    {
        SussyText.gameObject.SetActive(true);
        await Task.Delay(1000);
        SussyText.gameObject.SetActive(false);
    }
    
    protected async void DeathScreen()
    {
        DeadScreen.SetActive(true);
        await Task.Delay(3000);
        DeadScreen.SetActive(false);
        GameManager.Instance.PlayerController.Character.Revive();
    }
    #endregion HUD
}
