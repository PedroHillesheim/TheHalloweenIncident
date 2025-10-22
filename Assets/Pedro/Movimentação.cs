using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimentação : MonoBehaviour
{
    [Header("Configurações de velocidade")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float acceleration = 10f;

    [Header("Sistema de Stamina")]
    public int maxStamina = 5;          // stamina máxima
    public int staminaRegenRate = 2;    // quanto recupera por segundo
    public int staminaDrainRate = 1;    // quanto gasta por segundo
    public int minStaminaToRun = 2;     // valor mínimo para poder voltar a correr
    public TMP_Text staminaText;
    private float currentStamina;       // valor real, mas arredondado a cada frame
    private bool isRunning;
    private bool canRun = true;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        staminaText.text = currentStamina.ToString();
        // Ler input direcional (WASD)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;

        // Jogador quer correr?
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift);

        // Se stamina acabou, desativa corrida
        if (currentStamina <= 0)
        {
            canRun = false;
            isRunning = false;
        }
        // Se regenerou o suficiente, pode correr de novo
        else if (currentStamina >= minStaminaToRun)
        {
            canRun = true;
        }

        // Se pode correr e está pressionando Shift
        if (wantsToRun && canRun && inputDirection.sqrMagnitude > 0)
            isRunning = true;
        else
            isRunning = false;

        // Define a velocidade alvo
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        Vector2 targetVelocity = inputDirection * targetSpeed;

        // Suaviza a aceleração
        currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);

        // Atualiza stamina
        UpdateStamina();

        // Mostra stamina sem casas decimais
        Debug.Log("Stamina: " + Mathf.RoundToInt(currentStamina));
    }

    void FixedUpdate()
    {
        rb.linearVelocity = currentVelocity;
    }

    void UpdateStamina()
    {
        if (isRunning)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina < 0)
                currentStamina = 0;
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
        }
    }

    public float GetStaminaPercent()
    {
        return currentStamina / maxStamina;
    }
}
