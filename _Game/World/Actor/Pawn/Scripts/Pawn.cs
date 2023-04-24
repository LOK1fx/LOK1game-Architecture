using System.Linq;
using UnityEngine;
using Logger = LOK1game.Utils.Logger;

namespace LOK1game
{
    public abstract class Pawn : Actor, IPawn
    {
        public bool IsLocal { get; private set; }

        public EPlayerType PlayerType => playerType;

        [SerializeField] protected EPlayerType playerType;

        public Controller Controller { get; private set; }
        public abstract void OnInput(object sender);

        public virtual void OnPocces(Controller sender)
        {
            Controller = sender;
        }

        public virtual void OnUnpocces()
        {
            Controller = null;
        }

        public static Vector3 GetRandomSpawnPosition()
        {
            return GetRandomSpawnPosition(true);
        }

        public static Vector3 GetRandomSpawnPosition(bool playerFlag)
        {
            var spawnPoints = FindObjectsOfType<CharacterSpawnPoint>().ToList();

            if (playerFlag)
                spawnPoints.RemoveAll(point => point.AllowPlayer == false);
            
            if (spawnPoints.Count < 1)
                return Vector3.zero;
            
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            return spawnPoint.transform.position;
        }

#region Loggers

        public Logger GetPlayerLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Player);
        }

        public Logger GetEnemiesLogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.Enemies);
        }

        public Logger GetAILogger()
        {
            return GetLoggers().GetLogger(ELoggerGroup.AI);
        }

        #endregion
    }
}