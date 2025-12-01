using TMPro;
using UnityEngine;

public class Movimentação : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float acceleration = 10f;
    public int maxStamina = 5;          
    public int staminaRegenRate = 2;    
    public int staminaDrainRate = 1;    
    public int minStaminaToRun = 2;     
    public TMP_Text staminaText;
    private float currentStamina;       
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;
        bool wantsToRun = Input.GetKey(KeyCode.LeftShift);
        if (currentStamina <= 0)
        {
            canRun = false;
            isRunning = false;
        }
        else if (currentStamina >= minStaminaToRun)
        {
            canRun = true;
        }
        if (wantsToRun && canRun && inputDirection.sqrMagnitude > 0)
        {
            isRunning = true;
        }

        else
        {
            isRunning = false;
        }
        float targetSpeed = isRunning ? runSpeed : walkSpeed;
        Vector2 targetVelocity = inputDirection * targetSpeed;
        currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        UpdateStamina();;
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
            {
                currentStamina = 0;
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }

    public float GetStaminaPercent()
    {
        return currentStamina / maxStamina;
    }
}
