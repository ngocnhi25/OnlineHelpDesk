using AutoMapper;

namespace Application.Common.Mapppings
{
    public interface IMapForm<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());

    }
}
