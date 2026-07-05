using UnityEngine;

public class BossGameObject : MonoBehaviour
{
    [SerializeField] private Camera _combatCamera;
    [SerializeField] private Canvas _combat_UI;
    [SerializeField] private Fight fight;
    [SerializeField] private Bosses bosses;
    [SerializeField] private AudioSource BackgroundMusic;
    [SerializeField] private AudioClip BossMusic;
    [SerializeField] private Canvas VictoryScreen;

    void Update()
    {
        if (bosses.isDead())
        {
            if (_combatCamera != null) _combatCamera.gameObject.SetActive(false);
            if (_combat_UI != null) _combat_UI.gameObject.SetActive(false);
            Destroy(fight);
            Player.LockPlayer = false;
            Cursor.visible = false;
            if (VictoryScreen != null) Instantiate(VictoryScreen,Vector3.zero,Quaternion.identity);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }


    // Starts the boss encounter by enabling the combat UI,
    // locking the player, and switching to the boss battle music.
    private void OnTriggerEnter(Collider other){
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        if (_combatCamera != null) _combatCamera.gameObject.SetActive(true);
        if (_combat_UI != null) _combat_UI.gameObject.SetActive(true); 
        Player.LockPlayer = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (fight != null) fight.StartCombat();
        player.transform.LookAt(bosses.transform);
        bosses.transform.LookAt(player.transform);
        player.transform.GetChild(0).LookAt(bosses.transform);
        if (BackgroundMusic != null && BossMusic != null)
        {
            BackgroundMusic.Stop();
            BackgroundMusic.clip = BossMusic;
            BackgroundMusic.Play();
        }
    }

     // Restores the normal game view when the player leaves the boss trigger area.
    private void OnTriggerExit(Collider other){
        Player player = other.GetComponent<Player>();
        if (player == null) return;
        if (_combatCamera != null) _combatCamera.gameObject.SetActive(false);
        if (_combat_UI != null) _combat_UI.gameObject.SetActive(false);
    }
}
