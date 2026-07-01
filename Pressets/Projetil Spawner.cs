using UnityEngine;

public class ProjetilSpawner : MonoBehaviour
{
    [SerializeField] private Projetil projetilPrefab; // Prefab do projétil a ser instanciado

    public Projetil SpawnProjetil()
    {
        return Instantiate(projetilPrefab, transform.position, transform.rotation).GetComponent<Projetil>();
    }
}
