using EmitMapper;

namespace MyProject.Services.Mappers
{
    public class EntityMapper
    {
        public static TOut Map<TIn, TOut>(TIn t)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TIn, TOut>();
            return mapper.Map(t);
        }
    }
}