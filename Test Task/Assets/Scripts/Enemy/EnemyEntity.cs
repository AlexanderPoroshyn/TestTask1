using UnityEngine;
using System.Collections;

public class EnemyEntity : MonoBehaviour
{
    private bool isInfected;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material infectedMaterial;

    [SerializeField] private Animator animator;
    [SerializeField] private ExplosionEffect explosionEffect;

    private Coroutine deactivateCoroutine;

    private void OnEnable()
    {
        animator.Play("Spawn", 0, 0f);
    }

    public void SetIsInfected(bool isInfected)
    {
        this.isInfected = isInfected;

        if (isInfected == true)
        {
            meshRenderer.material = infectedMaterial;
            if (deactivateCoroutine == null)
            {
                deactivateCoroutine = StartCoroutine(DeactivateAfterDelay(0.5f));
            }
        }
        else
        {
            meshRenderer.material = defaultMaterial;
        }
    }

    public bool GetIsInfected()
    {
        return isInfected;
    }

    public void ForceDeactivate()
    {
        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
            deactivateCoroutine = null;
        }

        Deactivate();
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnemyPool.Instance.OnEnemyWasDestroyed();
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Deactivate();
    }

    private void Deactivate()
    {
        SetIsInfected(false);
        gameObject.SetActive(false);
    }
}
