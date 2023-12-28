using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    [SerializeField]
    AnimationCurve[] movementCurve;

    [SerializeField]
    AIEnemy[] Ai_EnemyType;

    [SerializeField]
    Vector3 [] EnemyGenerationPoint;

    List<AIEnemy> _aiEnemy = new List<AIEnemy>();

    public float horizontalWalkRange = 15;

    [SerializeField]
    bool CanMove = false;

    void Awake () {
        GenerateEnemyAt();
    }
	
    void GenerateEnemyAt()
    {
        for(int i=0;i<Ai_EnemyType.Length;i++)
        {
            _aiEnemy.Add(Ai_EnemyType[i]);
        }
    }
    private void Start()
    {
        for (int i = 0; i < _aiEnemy.Count; i++)
        {
            _aiEnemy[i].SetObjectAt(EnemyGenerationPoint[i], 10, movementCurve[i], i);
        }
    }

    internal void GetsetCallBack(int index)
    {
        _aiEnemy[index].SetStationary();
        _aiEnemy[index].SetObjectAt(EnemyGenerationPoint[Random.Range(0,_aiEnemy.Count)], Random.Range(3,7), movementCurve[Random.Range(0, _aiEnemy.Count - 1)], index);
    }

    void Update ()
    {
		
	}
}
