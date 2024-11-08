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
     * 1������ : 1/200�� = 5 / 1000�� ��, 0.005��
     * 
     * 0.005�� : a
     * 0.01�� : b
     * 3�� �����...
     * 
     * myCoroutine != null�̱� ������ ���� �ڷ�ƾ ����
     * 
     * 
     */
}