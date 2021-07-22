using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoadScene : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public Image loadBar;
    public Text barTxt;
    private int loadItems;
    private void Start()
    {
        StartCoroutine(LoadAsyncScene());
        StartCoroutine(InitializePolled());
    }
    private IEnumerator LoadAsyncScene()
    {
        yield return StartCoroutine(InitializePolled());
        yield return new WaitForSeconds(0.1f);
        asyncOperation = SceneManager.LoadSceneAsync(1);
        while(!asyncOperation.isDone)
        {
           
            float progess = asyncOperation.progress / 0.9f;
            //loadBar.fillAmount = progess;
            barTxt.text = "Loading..." + string.Format("{0:0}%", progess * 100f);
            yield return 0;
        }
    }
    public IEnumerator InitializePolled()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < PoolManager.instance.enemyPrefabs.Count; j++)
            {
                yield return new WaitForSeconds(0.05f);
                GameObject obj = Instantiate(PoolManager.instance.enemyPrefabs[j]);
                PoolManager.instance.InStanceNewZombie(obj, i);
                loadItems++;
                barTxt.text = "Load items: " + loadItems + "/" + "100";
            }
        }
    }
}
