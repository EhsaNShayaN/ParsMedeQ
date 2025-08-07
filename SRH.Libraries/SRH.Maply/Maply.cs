using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace SRH.Maply;

public sealed class Maply : IMapper
{
    public readonly static Type MaplyGenericInterfaceType = typeof(IMaplyMapper<,>);

    private static ConcurrentDictionary<MapperDicKey, Delegate> _allMappers = new ConcurrentDictionary<MapperDicKey, Delegate>();
    private static Func<LambdaExpression, Delegate> Compiler { get; set; } = (lambda) => lambda.Compile();

    public readonly static Maply Defualt = new();

    public TDestionation Map<TSource, TDestionation>(TSource model)
    {
        var key = new MapperDicKey(typeof(TSource), typeof(TDestionation));

        if (_allMappers.TryGetValue(key, out var mapper))
        {
            return ((Func<TSource, TDestionation>)mapper).Invoke(model);
        }

        return model.Adapt<TDestionation>();
    }

    public static Func<TModel, TDto> GetMapper<TModel, TDto>()
        where TModel : class
        where TDto : class
    {
        var key = new MapperDicKey(typeof(TModel), typeof(TDto));

        if (_allMappers.TryGetValue(key, out var mapper))
        {
            return (Func<TModel, TDto>)mapper;
        }
        return Defualt.Map<TModel, TDto>;
    }

    public static bool AddNewMapper<TModel, TDto>(Func<TModel, TDto> mapper)
        where TModel : class
        where TDto : class
    {
        return _allMappers.TryAdd(new MapperDicKey(typeof(TModel), typeof(TDto)), Compiler(GenerateLambda(mapper)));
    }

    public static bool AddNewInstanceMapper<TModel, TDto>(IMaplyMapper<TModel, TDto> instance)
        where TModel : class
        where TDto : class
    {
        return _allMappers.TryAdd(new MapperDicKey(typeof(TModel), typeof(TDto)), Compiler(GenerateLambda<TModel, TDto>(instance.Map)));
    }

    private static LambdaExpression GenerateLambda<TInput, TOutput>(Func<TInput, TOutput> func)
    {
        // Parameter expression for the input type
        ParameterExpression inputParameter = Expression.Parameter(typeof(TInput), "input");

        // Create a lambda expression from the delegate
        Expression<Func<TInput, TOutput>> lambdaExpression = x => func(x);

        // Replace the lambda expression parameter with the input parameter
        var body = new ParameterReplacer(inputParameter).Visit(lambdaExpression.Body);

        // Create a new lambda expression with the input parameter
        LambdaExpression resultLambda = Expression.Lambda(body, inputParameter);

        return resultLambda;
    }

    public void ScanAndAddMappers(params Assembly[] assemblies)
    {
        var implementingTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(type => type.GetInterfaces().Any(
                i => i.IsGenericType && i.GetGenericTypeDefinition() == MaplyGenericInterfaceType));

        foreach (var type in implementingTypes)
        {
            // Create an instance of the implementing type (assuming it has a parameterless constructor)
            var instance = Activator.CreateInstance(type);

            if (instance is null) continue;

            var typeGenericTypes = FindGenericTypes(type);

            if (typeGenericTypes.Length != 2) continue;

            // Call the AddNewMapper method for the type
            MethodInfo methodInfo = typeof(Maply).GetMethod(nameof(AddNewInstanceMapper))!;
            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(typeGenericTypes[0], typeGenericTypes[1]);

            genericMethodInfo.Invoke(this, [instance]);
        }

    }
    public void ScanAndAddMappers(IServiceCollection services, params Assembly[] assemblies)
    {
        var implementingTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(type => type.GetInterfaces().Any(
                i => i.IsGenericType && i.GetGenericTypeDefinition() == MaplyGenericInterfaceType))
            .ToArray();

        foreach (var type in implementingTypes)
        {
            services.AddTransient(type);
        }
        var serviceProvider = services.BuildServiceProvider();

        foreach (var type in implementingTypes)
        {
            // Create an instance of the implementing type (assuming it has a parameterless constructor)
            var instance = ActivatorUtilities.CreateInstance(serviceProvider, type);

            if (instance is null) continue;

            var typeGenericTypes = FindGenericTypes(type);

            if (typeGenericTypes.Length != 2) continue;

            // Call the AddNewMapper method for the type
            MethodInfo methodInfo = typeof(Maply).GetMethod(nameof(AddNewInstanceMapper))!;
            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(typeGenericTypes[0], typeGenericTypes[1]);

            genericMethodInfo.Invoke(this, [instance]);
        }

    }
    private static Type[] FindGenericTypes(Type type)
    {
        var result = Array.Empty<Type>();

        bool found;
        (found, result) = CheckType(type);
        if (found) return result;

        if (type.BaseType is not null)
        {
            var r = FindGenericTypes(type.BaseType);
            if (r.Any()) return r;
        }

        return result;

    }
    private static (bool, Type[]) CheckType(Type type)
    {
        var genericArgs = type.GetGenericArguments();

        return (genericArgs.Length == 2 && MaplyGenericInterfaceType.MakeGenericType(genericArgs).IsAssignableFrom(type), genericArgs ?? Array.Empty<Type>());
    }
}
