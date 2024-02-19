using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffectController : MonoBehaviour
{
    private Renderer[] meshRenderers;

    private MaterialPropertyBlock propertyBlock;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderers = GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SpawnEffect();
        }
    }

    private void SpawnEffect()
    {
        StartCoroutine(TweenSpawnEffect());
    }

    private IEnumerator TweenSpawnEffect()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime / 2f;

            propertyBlock.SetFloat("_Clip", t);
            foreach (var renderer in meshRenderers)
            {
                renderer.SetPropertyBlock(propertyBlock);
            }
            yield return null;
        }

        propertyBlock.SetFloat("_Clip", 0);
        foreach (var renderer in meshRenderers)
        {
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
