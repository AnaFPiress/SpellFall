using UnityEngine;


public class Eyes : MonoBehaviour
{
    public float maxDistance = 100f;
    private PuzzleTriger LastPuzzle;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        GameUI ui = GameObject.FindFirstObjectByType<GameUI>();
        if (ui == null) return;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            PuzzleTriger puzzle = hit.collider.gameObject.GetComponent<PuzzleTriger>();
            if (puzzle != null)
            {
                LastPuzzle = puzzle;
                puzzle.ShowInteractiveLabel();
            }
            //if (puzzle != null)     ui.ShowInteractable();
            if (LastPuzzle != null && LastPuzzle != puzzle)
            {
                LastPuzzle.HiddenInteractiveLabel();
                LastPuzzle = null;
            }
        }
        else
        {
            if (LastPuzzle != null)
            {
                LastPuzzle.HiddenInteractiveLabel();
                LastPuzzle = null;
            }
        }
    }
}
