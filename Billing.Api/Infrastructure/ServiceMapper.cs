using System;
using Mapster;
using MapsterMapper;

namespace Billing.Api.Infrastructure
{
    public class ServiceMapper : Mapper
    {
        internal const string _DIKEY = "Mapster.DependencyInjection.sp";
        private readonly IServiceProvider _ServiceProvider;

        public ServiceMapper(IServiceProvider serviceProvider, TypeAdapterConfig config) : base(config)
        {
            _ServiceProvider = serviceProvider;
        }

        public override TypeAdapterBuilder<TSource> From<TSource>(TSource source)
        {
            return base.From(source)
                .AddParameters(_DIKEY, _ServiceProvider);
        }

        public override TDestination Map<TDestination>(object source)
        {
            using var scope = new MapContextScope();
            scope.Context.Parameters[_DIKEY] = _ServiceProvider;
            return base.Map<TDestination>(source);
        }

        public override TDestination Map<TSource, TDestination>(TSource source)
        {
            using var scope = new MapContextScope();
            scope.Context.Parameters[_DIKEY] = _ServiceProvider;
            return base.Map<TSource, TDestination>(source);
        }

        public override TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            using var scope = new MapContextScope();
            scope.Context.Parameters[_DIKEY] = _ServiceProvider;
            return base.Map(source, destination);
        }

        public override object Map(object source, Type sourceType, Type destinationType)
        {
            using var scope = new MapContextScope();
            scope.Context.Parameters[_DIKEY] = _ServiceProvider;
            return base.Map(source, sourceType, destinationType);
        }

        public override object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            using var scope = new MapContextScope();
            scope.Context.Parameters[_DIKEY] = _ServiceProvider;
            return base.Map(source, destination, sourceType, destinationType);
        }
    }
}
