using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{
    public List<Domino> dominos_1;
    public List<Domino> dominos_2;

    private bool isLevelFinished;

    private void Awake()
    {
        isLevelFinished = false;
    }

    public void DominoFall(Domino domino)
    {
        if (dominos_1.Contains(domino))
        {
            if (domino.isFirst)
                StartCoroutine(Kill(0, dominos_1));
            else
                StartCoroutine(Kill(dominos_1.Count - 1, dominos_1));
        }

        if (dominos_2.Contains(domino))
        {
            if (domino.isFirst)
                StartCoroutine(Kill(0, dominos_2));
            else
                StartCoroutine(Kill(dominos_2.Count - 1, dominos_2));
        }
    }

    
    IEnumerator Kill(int index, List<Domino> listDomino)
    { 
        if (index == 0)
        {
            listDomino[listDomino.Count - 1].GetComponent<PolygonCollider2D>().enabled = false;

            int i = 0;
            while (i < listDomino.Count)
            {
                var tr = listDomino[i];
                tr.transform.DOScale(0, 0.15f).OnComplete(() =>
                {
                    tr.PlayVfx();
                    tr.gameObject.SetActive(false);
                    i++;
                });
                yield return new WaitForSeconds(0.3f);
            }
            listDomino.Clear();
        }
        else
        {
            listDomino[0].GetComponent<PolygonCollider2D>().enabled = false;

            int i = listDomino.Count - 1;
            while (i >= 0)
            {
                var tr = listDomino[i];
                tr.transform.DOScale(0, 0.15f).OnComplete(() =>
                {
                    tr.PlayVfx();
                    tr.gameObject.SetActive(false);
                    i--;
                });
                yield return new WaitForSeconds(0.16f);
            }
            listDomino.Clear();
        }
    }

    private void Update()
    {
        if (isLevelFinished)
            return;

        if (dominos_1.Count == 0 && dominos_2.Count == 0)
        {
            isLevelFinished = true;
            GameManager.Instance.CheckLevelUp();
        }
    }
}
