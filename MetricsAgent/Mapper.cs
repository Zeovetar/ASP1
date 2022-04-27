using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Responses;
namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Добавлять сопоставления в таком стиле надо для всех объектов
            CreateMap<CpuMetric, MetricDto>();
        }
    }
}
