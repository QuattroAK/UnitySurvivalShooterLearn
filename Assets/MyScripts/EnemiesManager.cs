using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace MyGame
{
    public class EnemiesManager : MonoBehaviour
    {
        public event Action<int> OnEnemyDie;

        [SerializeField] private Transform enemiesParent; // место расположения 
        [SerializeField] private List<EnemyInfo> enemyInfo; // данные о объекте
        
        private List<EnemiesByType> enemiesByTypes; // враги по типам 
        private PlayerController playerController; 
        public float spawnTime = 3f; // задержка спауна 

        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
            CreatePool();
            StartCoroutine(SpawnEnemies());
        }

        public void Refresh()
        {
            for (int i = 0; i < enemiesByTypes.Count; i++)
            {
                for (int j = 0; j < enemiesByTypes[i].enemiesControllers.Count; j++)
                {
                    if (enemiesByTypes[i].enemiesControllers[j].gameObject.activeSelf)
                    {
                        enemiesByTypes[i].enemiesControllers[j].Refresh();
                    }
                }
            }
        }

        public void CreatePool()
        {
            enemiesByTypes = new List<EnemiesByType>(); // лист врагов по типам

            for (int i = 0; i < enemyInfo.Count; i++) // проходим по листу данных врагов 
            {
                EnemiesByType enemyByType = new EnemiesByType(enemyInfo[i].typeEnemy); // выдергиваем из листа данных о врагах элементы(слон идет первый)  и получаем на него ссылку

                for (int j = 0; j < enemyInfo[i].poolCount; j++) // вторим циклом добаляем enemiesContoller
                {
                    EnemiesController enemiesController = Instantiate(enemyInfo[i].enemyPrefab, enemyInfo[i].spawnPoint.position, Quaternion.identity, enemiesParent);
                    enemiesController.gameObject.SetActive(false);
                    enemiesController.Init(playerController, OnEnemyDieHandler); // инициализация данного объекта 
                    enemyByType.enemiesControllers.Add(enemiesController);// добавили в лист данных объектов данного типа но не больше poolCount
                }

                enemiesByTypes.Add(enemyByType);
            }
        }

        public EnemiesController GetPooledEnemy(TypeEnemy typeEnemy) // выдергиваем нужный на объект по типу (допустим нам нужен слон) 
        {
            for (int i = 0; i < enemiesByTypes.Count; i++) // пробегаемся по списку создаанных объектов разного типа
            {
                if (enemiesByTypes[i].typeEnemy == typeEnemy) // и если это слон
                {
                    for (int j = 0; j < enemiesByTypes[i].enemiesControllers.Count; j++) // проходим по листу слонов 
                    {
                        if(!enemiesByTypes[i].enemiesControllers[j].gameObject.activeSelf)
                        {
                            return enemiesByTypes[i].enemiesControllers[j]; // вернуть первого слона из списка объектов(контроллеров) типа слона
                        }
                    }

                    EnemiesController enemiesController = Instantiate(enemyInfo[i].enemyPrefab, enemyInfo[i].spawnPoint.position, Quaternion.identity, enemiesParent);
                    enemiesController.gameObject.SetActive(false);
                    enemiesController.Init(playerController, OnEnemyDieHandler);
                    enemiesByTypes[i].enemiesControllers.Add(enemiesController);
                    return enemiesController;
                }
            }
            return null;
        }

        public IEnumerator SpawnEnemies()
        {
             while(playerController.IsAlive)
             {
                yield return new WaitForSeconds(spawnTime);

                for (int i = 0; i < enemyInfo.Count; i++)
                {
                    EnemiesController retrievedEnemy = GetPooledEnemy(enemyInfo[i].typeEnemy);
                    if (retrievedEnemy != null)
                    {
                        retrievedEnemy.transform.position = enemyInfo[i].spawnPoint.position;
                        retrievedEnemy.gameObject.SetActive(true);
                    }
                }
             }
        }

        private void OnEnemyDieHandler(int score)
        {
            OnEnemyDie?.Invoke(score);
        }
    }

    [Serializable]
    public class EnemyInfo// класс данных о врагах
    {
        public TypeEnemy typeEnemy;
        public EnemiesController enemyPrefab;
        public Transform spawnPoint;
        public int poolCount;
    }

    public class EnemiesByType // класс врагов по типам (слон, заяц, медведь) 
    {
        public TypeEnemy typeEnemy; // тип врага 
        public List<EnemiesController> enemiesControllers; // лист(пул)  этих врагов

        public EnemiesByType(TypeEnemy typeEnemy)
        {
            this.typeEnemy = typeEnemy;
            enemiesControllers = new List<EnemiesController>();
        }
    }

    public enum TypeEnemy
    {
        Elephant,
        Bunny,
        Bear
    }
}
