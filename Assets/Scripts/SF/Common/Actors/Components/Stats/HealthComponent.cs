﻿using SF.Game.Stats;

namespace SF.Common.Actors.Components.Stats
{
    public class HealthComponent : BaseResourceStatComponent
    {
        protected override int Min => 0;
        protected override PrimaryStat Stat => PrimaryStat.HP;
    }
}