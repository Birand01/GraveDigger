using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Tomb : MonoBehaviour
{
 
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private Transform graveTransform;
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float rotationDuration;
    [SerializeField] Image progressBarFillImage;
    [SerializeField] ParticleSystem winFX;
    [SerializeField] Text levelText;
    void Start()
    {
        progressBarFillImage.fillAmount = 1.0f;
        graveTransform.DOLocalRotate(rotationVector, rotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    public void Hit(int keyIndex,float damage)
    {
        float colliderHeight = 2.991831f;
        float newWeight = skinnedMesh.GetBlendShapeWeight(keyIndex) + damage * (100.0f / colliderHeight);
        skinnedMesh.SetBlendShapeWeight(keyIndex, newWeight);
        progressBarFillImage.fillAmount -=0.00015f ;
      
    }
    private void Update()
    {
        PlayWinFX();
    }

    private void PlayWinFX()
    {
        if (progressBarFillImage.fillAmount == 0.0f)
        {
            winFX.Play();
            levelText.gameObject.SetActive(true);
        }
    }

}
