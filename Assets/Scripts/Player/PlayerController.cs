using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    internal GameController gameController;

    [SerializeField]
    internal CameraController camera;

    [SerializeField]
    internal InputController input;

    [SerializeField]
    internal MovementController movement;

    [SerializeField]
    internal CollisionController collision;

    public AnimationController animation;
    public Transform[] targets;
    public Material standardMaterial;
    public Material damageMaterial;

    [Space(10)]
    public float hitPoints;

    internal Text hitPointsText;

    public Transform GetTarget() {
        return targets[Random.Range(0, targets.Length)];
    }

    public void ReduceHealth(float amount) {
        if (hitPoints - amount >= 0) {
            hitPoints -= amount;
        } else {
            hitPoints = 0f;
        }

        StartCoroutine(SetDamageMaterial());

        HandleHealth();
    }

    public void Spawn(float initialHitPoints = 100f) {
        hitPoints = initialHitPoints;
    }

    void Start() {
        Transform mainTarget = new GameObject().transform;
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
        targets[0] = mainTarget;
        FindGuiElements();
        Spawn(initialHitPoints: 100f);
    }

    void Update() {
        UpdateTargets();
        ShowTargets();
    }

    void OnGUI() {
        hitPointsText.text = "HP: " + hitPoints;
    }

    void UpdateTargets() {
        targets[0].position = transform.position + movement.moveDirection;
    }

    void ShowTargets() {
        foreach (Transform target in targets) {
            DrawCross(target.position, 1f, Color.red);
        }
    }

    internal void HandleHealth() {
        if (hitPoints <= 0) {
            gameController.OnDeath();
        }
    }

    internal void FindGuiElements() {
        hitPointsText = GameObject.Find("HP Bar").GetComponent<Text>();
    }

    internal void DrawCross(Vector3 position, float size, Color color) {
        Debug.DrawLine(new Vector3(position.x - size, position.y, position.z),
                       new Vector3(position.x + size, position.y, position.z),
                       color);
        Debug.DrawLine(new Vector3(position.x, position.y - size, position.z),
                       new Vector3(position.x, position.y + size, position.z),
                       color);
        Debug.DrawLine(new Vector3(position.x, position.y, position.z - size),
                       new Vector3(position.x, position.y, position.z + size),
                       color);

    }

    IEnumerator SetDamageMaterial() {
        SkinnedMeshRenderer meshRenderer = GameObject.Find("Escapist").GetComponent<SkinnedMeshRenderer>();
        meshRenderer.material = damageMaterial;
        yield return new WaitForSeconds(.1f);
        meshRenderer.material = standardMaterial;
    }
}
