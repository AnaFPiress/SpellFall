using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCutscene : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("Menu Inicial");
    }
}
