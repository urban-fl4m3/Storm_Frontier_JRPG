using System;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Data;
using SF.Game;

namespace SF.Battle.Effects
{
    public abstract class BaseEffect<TEffectData> : IEffect where TEffectData : IEffectData
    {
        public event Action Finished;
        
        protected TEffectData Data { get; private set; }
        protected BattleWorld World { get; private set; }
        protected IServiceLocator ServiceLocator { get; private set; }
        protected IActor Affected { get; private set; }
        protected IActor Affector { get; private set; }

        private int _roundsPassed;
        
        public void SetFactoryMeta(IDataProvider dataProvider)
        {
            if (dataProvider != null)
            {
                World = dataProvider.GetData<BattleWorld>();
                ServiceLocator = dataProvider.GetData<IServiceLocator>();
            }
        }

        public void Apply(IEffectData data, IActor affected, IActor affector)
        {
            if (!(data is TEffectData effectData))
            {
                ServiceLocator.Logger.LogError($"Wrong data {data} for skill {GetType()}");
                return;
            }
            
            Data = effectData;
            Affected = affected;
            Affector = affector;

            if (data.HasDuration)
            {
                var turnPasser = affector.Components.Get<ITurnPasser>();
                turnPasser.TurnPassed += UpdateRemainRounds;
            }

            OnApply();
        }

        private void UpdateRemainRounds()
        {
            _roundsPassed++;

            if (_roundsPassed >= GetEffectiveDuration())
            {
                OnFinish();
            }
        }

        public void Refresh(IEffectData effectData)
        {
            
        }

        public void Cancel()
        {
            Finished = null;
            OnCancel();
        }
        
        protected abstract void OnApply();
        protected abstract void OnCancel();

        private int GetEffectiveDuration()
        {
            var effectiveCount = 0;

            if (Affector == Affected)
            {
                effectiveCount++;
            }

            return Data.Rounds + effectiveCount;
        }

        private void OnFinish()
        {
            Finished?.Invoke();
        }
    }
}