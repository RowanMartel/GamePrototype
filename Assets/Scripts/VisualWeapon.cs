using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VisualWeapon : MonoBehaviour
{
    public Vector3 regularPosition;
    public Vector3 regularRotation;
    public Vector3 attackPosition;
    public Vector3 attackRotation;

    public float attackDuration = 0.5f;
    public float recoverDuration = 0.5f;
    private Sequence attackSequence;

    public TrailRenderer trail;
    private void Start()
    {
        trail.emitting = false;
        transform.localPosition = regularPosition;
        transform.localRotation = Quaternion.Euler(regularRotation);
    }

    public LayerMask enemyLayer;
    public float checkRadius;
    public float forwardAmount;
    private void OnDrawGizmos() {
        var checkPosition = transform.parent.position + (Vector3.up * 0.7f) + (transform.parent.forward * forwardAmount);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(checkPosition, checkRadius);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (attackSequence == null || !attackSequence.IsPlaying())) {
            DoAttackAnimation();
        }
    }

    public List<Transform> enemiesHit = new List<Transform>();
    [ContextMenu("DoAttack")]
    public void DoAttackAnimation()
    {
        enemiesHit.Clear();
        trail.emitting = true;
        attackSequence = DOTween.Sequence();
        attackSequence.Append(transform.DOLocalMove(attackPosition, attackDuration).SetEase(Ease.InQuad));
        attackSequence.Join(transform.DOLocalRotate(attackRotation, attackDuration).SetEase(Ease.InQuad).OnComplete(
            () =>
            {
                Debug.Log("HIT!");
            }));
        attackSequence.AppendInterval(0.2f);
        attackSequence.Append(transform.DOLocalMove(regularPosition, recoverDuration).SetEase(Ease.InOutQuad));
        attackSequence.Join(transform.DOLocalRotate(regularRotation, recoverDuration).SetEase(Ease.InOutQuad).OnComplete(
            () =>
            {
                trail.emitting = false;
            }));

        attackSequence.OnUpdate(() => {
            float percentageCompleted = attackSequence.ElapsedPercentage();
            if (percentageCompleted > 0.25f && percentageCompleted < 0.7f)
            {
                var checkPosition = transform.parent.position + (Vector3.up * 0.7f) + (transform.parent.forward * forwardAmount);
                var collidersFound = (Physics.OverlapSphere(checkPosition, checkRadius, enemyLayer,
                    QueryTriggerInteraction.Ignore));
                if (collidersFound.Length > 0)
                {
                    for (int i = 0; i < collidersFound.Length; i++)
                    {
                        if (!enemiesHit.Contains(collidersFound[i].transform)) {
                            enemiesHit.Add(collidersFound[i].transform);
                            collidersFound[i].transform.DOShakeScale(0.35f);
                            collidersFound[i].transform.DOShakeRotation(0.35f);
                        }
                    }
                }
            }
        });
    }
}
