using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{
    // Маркировочный интерфейс
    // используется, чтобы проверять работу репозитория на тесте-заглушке
    public interface IDotnetMetricsRepository : IRepository<DotnetMetric>
    {
    }

    public class DotnetMetricsRepository : IDotnetMetricsRepository
    {

        public DotnetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }
        private const string ConnectionString = "DataSource=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        // Инжектируем соединение с базой данных в наш репозиторий черезконструктор
        public void Create(DotnetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(value, time)VALUES(@value, @time)",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds
                    });
            }

        }
        public void Delete(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("DELETE FROM cpumetrics WHERE id=@id",
                    new
                    {
                        id = id
                    });
            }
        }
        public void Update(DotnetMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("UPDATE cpumetrics SET value = @value, time =@time WHERE id = @id;",
                    new
                    {
                        value = item.Value,
                        time = item.Time.TotalSeconds,
                        id = item.Id
                    });
            }
        }
        public IList<DotnetMetric> GetAll()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotnetMetric>("SELECT Id, Time, Value FROM cpumetrics").ToList();
            }
        }
        public DotnetMetric GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<DotnetMetric>("SELECT Id, Time, Value FROM cpumetrics Where id=@id",
                    new { id = id });
            }
        }

        public IList<DotnetMetric> GetByTimeToTime(TimeSpan from, TimeSpan to)
        {
            var timeFrom = from.TotalSeconds;
            var timeTo = to.TotalSeconds;
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<DotnetMetric>("SELECT Id, Time, Value FROM cpumetrics WHERE (time >= @timeFrom and time <= @timeTo)", 
                    new { timeFrom = timeFrom, timeTo = timeTo }).ToList();
            }
        }
    }
}
