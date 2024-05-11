using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Domino : MonoBehaviour
{
    public bool isFirst;

    public GameObject vfxDestroy;

    private bool isClicked;

    private void Awake()
    {
        isClicked = false;
    }

    public void PlayVfx()
    {
        GameObject vfx = Instantiate(vfxDestroy, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfx, 1f);
    }

    private void OnMouseDown()
    {
        if (isClicked)
            return;

        isClicked = true;

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].DominoFall(this);
    }
}
