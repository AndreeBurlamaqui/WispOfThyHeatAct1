using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyBox;

public class HUDManager : HUDSingleton<HUDManager>
{
    [SerializeField] private float dissolveTimer;
    [SerializeField] private GameObject healthHUD, pauseHUD;

    [Separator("Animations")]
    [SerializeField] private AnimationStateReference idleFeatherHP;
    [SerializeField] private AnimationStateReference twoFeathers, oneFeather, onlyRing;

    [Separator("HP Feather Parts")]
    [SerializeField] private Image[] firstHitParts = new Image[4] , secondHitParts = new  Image[4] , thirdHitParts = new Image[4];
    
    private Animator healthAnim;
    private void Start()
    {
        healthAnim = healthHUD.GetComponentInChildren<Animator>();
    }

    public void UpdateHPUI(int currentHP)
    {
        switch (currentHP)
        {
            case 1: //only the goldmedal > 3rd hit
                onlyRing.Play();
                StartCoroutine(DissolveHP(thirdHitParts));
                Debug.Log("MorteMorrida");
                break;
            case 2: //1 feathers only > 2nd hit
                oneFeather.Play();
                StartCoroutine(DissolveHP(secondHitParts));
                Debug.Log("Quase morrendo 1");
                break;
            case 3: //2 feathers only > 1st hit
                twoFeathers.Play();
                StartCoroutine(DissolveHP(firstHitParts));
                Debug.Log("Doeu 2");
                break;
            case 4: //recover all hp

                foreach(Image imgFirst in firstHitParts)
                {
                    imgFirst.gameObject.SetActive(true);
                    Material matFirst = Instantiate(imgFirst.material);
                    matFirst.SetFloat("_DissolveAmount", 1f);
                    imgFirst.material = matFirst;
                }

                foreach(Image imgSecond in secondHitParts)
                {
                    imgSecond.gameObject.SetActive(true);
                    Material matSecond = Instantiate(imgSecond.material);
                    matSecond.SetFloat("_DissolveAmount", 1f);
                    imgSecond.material = matSecond;
                }

                foreach (Image imgThird in thirdHitParts)
                {
                    imgThird.gameObject.SetActive(true);
                    Material matThird = Instantiate(imgThird.material);
                    matThird.SetFloat("_DissolveAmount", 1f);
                    imgThird.material = matThird;
                }

                idleFeatherHP.Play();
                break;
        }
    }

    IEnumerator DissolveHP(Image[] GOhitParts)
    {
        Material[] matHitParts = new Material[GOhitParts.Length];

        for (int x = 0; x < GOhitParts.Length; x++)
        {
            matHitParts[x] = Instantiate(GOhitParts[x].material);
        }

        float journey = 0f;
        while (journey <= dissolveTimer)
        {
            journey += Time.unscaledDeltaTime;

            for(int y = 0; y < GOhitParts.Length; y++)
            {
                matHitParts[y].SetFloat("_DissolveAmount", Mathf.Lerp(1, 0, journey));
                GOhitParts[y].material = matHitParts[y];
            }


            yield return null;
        }

        for (int z = 0; z < GOhitParts.Length; z++)
        {
            GOhitParts[z].gameObject.SetActive(false);
        }

        idleFeatherHP.Play();
    }

    public void UnderSavingProcess(bool isShowing)
    {
        if (isShowing)
        {
            //show under saving process
        }
        else
        {
            //hide under saving process
        }
    }

    public void SetActiveHPHUD(bool state)
    {
        healthHUD.SetActive(state);
    }

}
