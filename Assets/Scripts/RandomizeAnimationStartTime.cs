using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomizeAnimationStartTime : MonoBehaviour {
    private Animator animator;
    [Range(0f, 2f)]
    public float maxDelay = 1f;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        StartCoroutine(PlayAfterDelay());
    }


    private IEnumerator PlayAfterDelay() {
        yield return new WaitForSeconds(Random.Range(0f, maxDelay));
        animator.SetTrigger("start");
    }
}
