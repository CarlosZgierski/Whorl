using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectModule : MonoBehaviour
{
    HealthController HealthController;

    [Header("DamageFlash")]
    public Material damageFlashMaterial;
    public float damageFlashTime = 0.15f;
    public MeshRenderer[] meshRenderersToDamageFlash;
    public SkinnedMeshRenderer skinnedMeshToDamageFlash;
    Material[] OriginalMaterialsInDamageFlashTargets;
    private Material[] OrginalSkinnedMeshMaterials;
    [Space]
    [Space]


    [Header("Particles")]
    [SerializeField]
    GameObject AditionalDamageParticle;

    [SerializeField]
    GameObject DeathParticle;



    void Start()
    {
        HealthController = GetComponent<HealthController>();
        HealthController.OnDamage += HandleInstantiationOfDamageTypeParticle;
        if (AditionalDamageParticle)
        {
            HealthController.OnDamage += HandleInstatiationOfAditionalDamageParticle;
        }
        if (damageFlashMaterial)
        {
            HealthController.OnDamage += FlashDamage;
            GetOrginalMaterialsFromMeshRenderers();
          
        }
        if (DeathParticle)
        {
            HealthController.OnDie += InstantiateDeathParticle;
        }

    }

    void GetOrginalMaterialsFromMeshRenderers()
    {
        if (meshRenderersToDamageFlash.Length > 0)
        {
            OriginalMaterialsInDamageFlashTargets = new Material[meshRenderersToDamageFlash.Length];
            for (int i = 0; i < meshRenderersToDamageFlash.Length; i++)
            {
                OriginalMaterialsInDamageFlashTargets[i] = meshRenderersToDamageFlash[i].material;
            }
        }

        if (skinnedMeshToDamageFlash)
        {
            OrginalSkinnedMeshMaterials = new Material[skinnedMeshToDamageFlash.materials.Length];
            for (int i = 0; i < skinnedMeshToDamageFlash.materials.Length; i++)
            {
                OrginalSkinnedMeshMaterials[i] = skinnedMeshToDamageFlash.materials[i];
            }
        }
    }

    void HandleInstantiationOfDamageTypeParticle(float damage, DamageType type, HealthController victim)
    {
        if (type && type.GetDamageParticlePrefabByDamage(damage))
        {
            GameObject p = Instantiate(type.GetDamageParticlePrefabByDamage(damage), transform.position, Quaternion.identity);
            if (victim.health > 0)
            {
                p.transform.parent = transform;
            }
        }
    }

    void HandleInstatiationOfAditionalDamageParticle(float damage, DamageType type, HealthController victim)
    {
        GameObject p = Instantiate(AditionalDamageParticle, transform.position, Quaternion.identity);
        if (victim.health > 0)
        {
            p.transform.parent = transform;
        }
    }


    void InstantiateDeathParticle(HealthController x)
    {
        Instantiate(DeathParticle, transform.position, Quaternion.identity);
    }

    void FlashDamage(float x, DamageType y, HealthController p)
    {
        StartCoroutine(FlashDamageMaterialChange());
    }


    IEnumerator FlashDamageMaterialChange()
    {
        if (meshRenderersToDamageFlash.Length > 0)
        {
            foreach (MeshRenderer x in meshRenderersToDamageFlash)
            {
                x.material = damageFlashMaterial;
            }
        }
        if(skinnedMeshToDamageFlash)
        {
            Material[] x = new Material[skinnedMeshToDamageFlash.materials.Length];
            for (int i = 0; i < skinnedMeshToDamageFlash.materials.Length; i++)
            {
                x[i] = damageFlashMaterial;
                
            }
            skinnedMeshToDamageFlash.materials = x;
        }
        yield return new WaitForSeconds(damageFlashTime);
        if (meshRenderersToDamageFlash.Length > 0)
        {
            for (int i = 0; i < meshRenderersToDamageFlash.Length; i++)
            {
                if(meshRenderersToDamageFlash[i])
                meshRenderersToDamageFlash[i].material = OriginalMaterialsInDamageFlashTargets[i];
            }
        }

        if (skinnedMeshToDamageFlash)
        {
            skinnedMeshToDamageFlash.materials = OrginalSkinnedMeshMaterials;
        }
    }

    

  

    
}
