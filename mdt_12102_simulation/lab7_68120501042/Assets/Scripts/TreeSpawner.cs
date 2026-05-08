using UnityEngine;
public class TreeSpawner : MonoBehaviour
{
  [Header("Spawn Settings")]
  [SerializeField] private GameObject[] prefabs;
  [SerializeField] private float spawnInterval = 2f;
  [SerializeField] private float spawnRadius = 8f;
  [SerializeField] private int maxTrees = 10;
  [Header("Destroy Settings")]
  [SerializeField] private float treeLifetime = 6f;
  private float timer;
  private int currentTreeCount;
  private void Start()
  { Debug.Log("TreeSpawner: พร้อม!"); timer = spawnInterval; }
  private void Update()
  {
    timer += Time.deltaTime;
    if (timer >= spawnInterval && currentTreeCount < maxTrees)
    { SpawnTree(); timer = 0f; }
  }
  private void SpawnTree()
  {
    Vector2 rnd = Random.insideUnitCircle * spawnRadius;
    Vector3 pos = new Vector3(transform.position.x + rnd.x, 0f, transform.position.z + rnd.y);
    float scale = Random.Range(0.5f, 1.5f);
    GameObject tree = Instantiate(prefabs[Random.Range(0, prefabs.Length)], pos, Quaternion.identity);
    tree.transform.localScale *= scale;
    tree.name = $"Tree_Spawned_{currentTreeCount}";
    currentTreeCount++;
    Debug.Log($"Spawned: {tree.name} at {pos}");
    Destroy(tree, treeLifetime);
  }
}
