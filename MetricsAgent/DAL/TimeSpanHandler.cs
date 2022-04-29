﻿using Dapper;
using System.Data;
using System;
namespace MetricsAgent.DAL
{
    // Задаём хендлер для парсинга значений в TimeSpan, если таковые попадутся    в наших классах моделей
public class TimeSpanHandler : SqlMapper.TypeHandler<TimeSpan>
    {
        public override TimeSpan Parse(object value)
            => TimeSpan.FromSeconds((double)value);
        public override void SetValue(IDbDataParameter parameter, TimeSpan
        value)
        => parameter.Value = value;
    }
}
