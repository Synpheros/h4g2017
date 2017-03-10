using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TVController : MonoBehaviour {

    public DDRController ddr;
    public GameObject whitenoise, futbol;

    private void Update()
    {
        if (ddr.finished)
        {
            StartCoroutine(ChangeChannel());
        }
    }

    public void StartDDR()
    {
        ddr.GetComponent<Animator>().SetBool("start", true);
    }

    public IEnumerator ChangeChannel()
    {
        whitenoise.SetActive(true);
        yield return new WaitForSeconds(1);
        futbol.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Dia5_3");
    }
}
