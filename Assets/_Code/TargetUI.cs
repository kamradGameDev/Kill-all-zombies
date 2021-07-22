using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUI : MonoBehaviour
{
    public static TargetUI instance;
    public Transform[] arrows;
    public List<Transform> targets;
    public Camera mainCamera;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    private void LateUpdate()
    {
        if(WorldUIManager.instance.player)
        {
            for(int i = 0; i < targets.Count; i++)
            {
                if(targets[i])
                {
                    Vector3 v_diff = (targets[i].position - WorldUIManager.instance.player.transform.position);
                    float _atan2 = Mathf.Atan2(v_diff.y, v_diff.x) * Mathf.Rad2Deg;

                    arrows[i].position = Vector3.MoveTowards(arrows[i].position, targets[i].position, 1000f);
                    if (Vector2.Distance(WorldUIManager.instance.player.transform.position, targets[i].position) > 10f)
                    {
                        arrows[i].gameObject.SetActive(true);
                        arrows[i].rotation = Quaternion.Euler(0f, 0f, _atan2 - 90);
                    }
                    else
                    {
                        arrows[i].gameObject.SetActive(false);
                    }

                    Vector3 _pos = mainCamera.WorldToViewportPoint(arrows[i].position);

                    _pos.x = Mathf.Clamp01(_pos.x);
                    _pos.y = Mathf.Clamp01(_pos.y);

                    arrows[i].transform.position = mainCamera.ViewportToWorldPoint(_pos);
                }
                else
                {
                    arrows[i].gameObject.SetActive(false);
                }
            }
        }
    }
}