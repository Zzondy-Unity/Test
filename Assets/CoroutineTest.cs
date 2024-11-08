using System.Collections;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    private Coroutine myCoroutine;
    private void Start()
    {
        StartTestCoroutine();
        Invoke("StartTestCoroutine", 1);
    }

    void StartTestCoroutine()
    {
        if (myCoroutine != null) StopCoroutine(myCoroutine);
        myCoroutine = StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
        Debug.Log("a");
        yield return null;
        Debug.Log("b");
        yield return new WaitForSeconds(3);
        Debug.Log("c");
    }

    /* 
     * 1프레임 : 1/200초 = 5 / 1000초 즉, 0.005초
     * 
     * 0.005초 : a
     * 0.01초 : b
     * 3초 대기중...
     * 
     * myCoroutine != null이기 때문에 위의 코루틴 정지
     * 
     * 
     */
}