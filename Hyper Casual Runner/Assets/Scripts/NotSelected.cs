using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSelected : MonoBehaviour
{
    public float WaitBetween = 0.15f;
    List<Animator> animators;
    void Start()
    {
        animators = new List<Animator>(GetComponentsInChildren<Animator>());

        StartCoroutine(DoAnim());
    }

    IEnumerator DoAnim()
    {
        while (true)
        {

            foreach (var animator in animators)
            {
                animator.SetTrigger("DoAnim");
                yield return new WaitForSeconds(WaitBetween);
            }

        }
     

    }




}
