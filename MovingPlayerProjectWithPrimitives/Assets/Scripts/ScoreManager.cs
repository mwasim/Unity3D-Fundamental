using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    private WorkerController workerScript;

    private void OnTriggerExit(Collider other)
    {
        workerScript = other.gameObject.GetComponent<WorkerController>();

        if (workerScript.isHappy)
        {
            score += 2;
        }
        else
        {
            score -= 4; //lose more than gain
        }


        Debug.Log("Totla Score: " + score);

        Destroy(other.gameObject);
    }
}
