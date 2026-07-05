using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float mana = 100f;
    [SerializeField] private float max_mana = 100f;
    [SerializeField] private float max_Stamina = 30f;
    [SerializeField] private float StaminaDelta = 2f;
    private float currentStamina;
    private CharacterController characterController;
    private float vida = 100f;
    [SerializeField] private float max_vida = 100f;
    private Vector3 velocity = new();
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform model;
    [SerializeField] private ProjetilSpawner projetilSpawner;
    [SerializeField] private GameObject CrystalPos, CrystalDrop;
    [SerializeField] private Canvas pause;
    [SerializeField] private Canvas DeathScreen;
    [SerializeField] private AudioClip deathClip, SpellClip, JumpSound;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource WalkSound, AmbientSound, PausedSound;
    public static bool LockPlayer = false;
    public static bool AttackTutorialLock = true;
    private int TutorialAttacks = 0;
    private float LastStaminaUseTime = -100f;
    private float StaminaRegenDelay = 2f;
    private bool StaminaRegenActive = false;
    private float LastDamageTime = 0f;
    public float InstantTakeCrystal = 0f;
    public bool isPaused = false;
    private bool isWalking = false;
    private bool isRunning = false;


    // Initializes the player state and references
    void Awake()
    {
        LockPlayer = false;
        currentStamina = max_Stamina;
        if (pause == null)
        {
            Destroy(this.gameObject);
            return;
        }
        pause.gameObject.SetActive(false);
    }

    // Sets up required components and starts background coroutines
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        StartCoroutine(WalkSoundCourotine());
    }

    // Handles player movement, input, stamina, jumping and attacks
    void BodyMovementFunction()
    {
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float moveX = 0f;
        float moveZ = 0f;
        float boost = 1f;

        if (Keyboard.current.wKey.isPressed) moveZ += 1f;
        if (Keyboard.current.sKey.isPressed) moveZ -= 1f;
        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;

        // 1. Criamos a direção baseada no input do teclado
        Vector3 inputDirection = new Vector3(moveX, 0f, moveZ);
        
        // 2. Calculamos a magnitude (0 se parado, 1 se andando, 1 na diagonal se normalizado)
        // Usamos o inputDirection bruto para saber se há intenção de andar
        float inputMagnitude = inputDirection.magnitude; 

        if (Mouse.current.leftButton.wasPressedThisFrame 
        && inputMagnitude == 0f // Corrigido aqui para checar se está parado
        && mana >= 10f
        && Time.time - LastDamageTime > 2f
        && ((AttackTutorialLock && TutorialAttacks < 5) || !AttackTutorialLock)){
            TutorialAttacks++;
            StartCoroutine(Attack());
        }
            
        StaminaRegenActive = Time.time - LastStaminaUseTime > StaminaRegenDelay;

        if (Keyboard.current.leftShiftKey.isPressed && currentStamina > 0f && StaminaRegenActive) boost = 5f;

        // 3. Definimos o vetor de movimento real no espaço do mundo
        Vector3 move = (transform.right * moveX + transform.forward * moveZ).normalized;

        if (animator != null)
        {
            // CORREÇÃO AQUI: Passamos a magnitude real multiplicada pelo boost, limitando a 1 se não for corrida
            float walkAnimationValue = Mathf.Clamp01(inputMagnitude) * boost;
            animator.SetFloat("Walk", walkAnimationValue);
        }

        // CORREÇÃO AQUI: IsWalking agora checa a magnitude real do input
        isWalking = inputMagnitude > 0.1f;
        isRunning = inputMagnitude * boost > 1f;

        if (WalkSound != null) WalkSound.gameObject.SetActive(isWalking);

        if (boost != 1f && inputMagnitude > 0f)
                currentStamina -= Time.deltaTime * StaminaDelta;
        else currentStamina += Time.deltaTime * StaminaDelta/3;

        currentStamina = Mathf.Clamp(currentStamina, 0, max_Stamina);
        if (currentStamina <= 1f)
            LastStaminaUseTime = Time.time;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && characterController.isGrounded)
        {
            if (animator != null) animator.SetTrigger("Jump");
            StartCoroutine(Jump());
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move((move * (moveSpeed + boost) + velocity) * Time.deltaTime);
        
        if (model != null && move != Vector3.zero) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * 10f);
        }
        if (mana < max_mana) mana = Mathf.Clamp(mana, 0f, max_mana);
    }

    // Executes the player's magic attack
    public IEnumerator Attack()
    {
        LastDamageTime = Time.time;
        if (animator != null) animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        if (SpellClip != null) GameObject.FindFirstObjectByType<SoundServer>().Play(SpellClip,audioMixer.FindMatchingGroups("Effect")[0]);
        if (projetilSpawner != null) projetilSpawner.SpawnProjetil();
        mana -= 10f;
    }

    // Handles the player jump animation and physics
    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.8f);
        if (JumpSound) GameObject.FindFirstObjectByType<SoundServer>().Play(JumpSound,audioMixer.FindMatchingGroups("Effect")[0]);
        yield return new WaitForSeconds(0.2f);
        velocity.y = Mathf.Sqrt(jumpForce * -5f * gravity);
    }

    // Applies damage to the player
    public void TakeDamage(float damage)
    {
        if (vida <= 0) return;
        Debug.Log("Player take" + damage + "damage");
        StartCoroutine(TakeDamageAnimaton());
        vida -= damage;
        if (vida <= 0) vida = 0;
    }

    // Plays the damage animation
    private IEnumerator TakeDamageAnimaton()
    {
        yield return new WaitForSeconds(1f);
        if (animator != null) animator.SetTrigger("Damage");
    }

    // Controls the playback of footstep sounds
    private IEnumerator WalkSoundCourotine()
    {
        while (true)
        {
            if (characterController.isGrounded)
            {
                WalkSound.pitch = (isRunning) ? UnityEngine.Random.Range(1.4f,1.75f) : UnityEngine.Random.Range(0.9f,1.3f);
                WalkSound.Play();
            }
            else WalkSound.Stop();

            yield return new WaitForSeconds(WalkSound.clip.length/WalkSound.pitch);
        }
    }

    public float getVida(){ return (vida > max_vida) ? max_vida : vida; }
    public float getPercentVida(){ return vida / max_vida; }
    public float getMana(){ return (mana > max_mana) ? max_mana : mana; }
    public float getPercentMana(){ return mana / max_mana; }
    public float getStamina(){ return (currentStamina > max_Stamina) ? max_Stamina : currentStamina; }
    public void addMana(float amount){
        mana = Mathf.Clamp(mana + amount, 0, max_mana);
    }
    public void addVida(float amount){
        vida = Mathf.Clamp(vida + amount, 0, max_vida);
    }

    // Picks up a crystal and attaches it to the player
    public void addCrystal(Crystal crystal)
    {
        if (crystal != null && CrystalPos != null && CrystalDrop != null){
            crystal.transform.SetParent(CrystalPos.transform);
            crystal.transform.position = Vector3.zero;
            crystal.transform.rotation = Quaternion.identity;
            InstantTakeCrystal = Time.time;
            crystal.CrystalTransport();
        }
    }

    // Drops the currently carried crystal
    public void dropCrystal()
    {
        if (CrystalPos.transform.childCount == 0) return;
        Crystal crystal = CrystalPos.transform.GetChild(0).GetComponent<Crystal>();
        if (CrystalDrop != null && crystal != null && Time.time - InstantTakeCrystal > 0.5f){
            crystal.transform.SetParent(null);
            crystal.transform.position = CrystalDrop.transform.position;
            crystal.transform.rotation = Quaternion.identity;   
            crystal.CrystalDrop();
        }
    }

    void Update()
    {
        if (animator != null)
        {
            if (vida <= 0)
            {
                animator.SetTrigger("Death");
                StartCoroutine(OnDeath());
                Destroy(this, 3f);
                LockPlayer = true;
                return;
            }
        }
        AmbientSound.gameObject.SetActive(!isPaused);
        PausedSound.gameObject.SetActive(isPaused);
        if (Keyboard.current[Key.Escape].wasPressedThisFrame) isPaused = !isPaused;
        pause.gameObject.SetActive(isPaused);
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (LockPlayer){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WalkSound.gameObject.SetActive(false);
            animator.SetFloat("Walk", 0f);
            return;
        }
        animator.SetBool("isGrounded",characterController.isGrounded);
        BodyMovementFunction();
        if (Keyboard.current[Key.Z].wasPressedThisFrame) dropCrystal();
        Debug.Log("isGrounded -> " + characterController.isGrounded);
    }

    // Handles the player death sequence
    private IEnumerator OnDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (deathClip != null)
        {
            SoundServer soundServer = GameObject.FindFirstObjectByType<SoundServer>();
            soundServer.Play(deathClip,audioMixer.FindMatchingGroups("Effect")[0]);
        }
        yield return new WaitForSeconds(2f);
        if (DeathScreen != null) Instantiate(DeathScreen,Vector3.zero,Quaternion.identity);
    }

    // Resumes the game after being paused
    public void Resume(){ isPaused = false; }
}
