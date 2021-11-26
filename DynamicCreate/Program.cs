using DynamicCreate;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

Console.WriteLine("======================1.常规写法======================");
IAnimal animal_Dog = new Dog("旺财");
animal_Dog.Cry();
IAnimal animal_Cat = new Cat();
animal_Cat.Cry();


Console.WriteLine("======================2.动态创建写法======================");
//获取实现接口IAnimal的实例对象
var types = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IAnimal))))
                        .ToList();

//获取实例对象名称
foreach (Type t in types)
{
    Console.WriteLine(t.Name);
}

//判断实例对象的构造函数是否有参数
foreach (Type v in types)
{
    if (v.GetConstructors().Any(x => x.GetParameters().Any()))
    {
        Console.WriteLine($"{v.Name}=>有参构造函数");
    }
    else
    {
        Console.WriteLine($"{v.Name}=>无参构造函数");
    }
}

//动态创建有参，无参构造函数实例对象
//常规动态创建写法
Console.WriteLine("======================2.1常规动态创建写法======================");
foreach (Type t in types)
{
    IAnimal animal;
    if (t.GetConstructors().Any(x => x.GetParameters().Any()))
    {
        //有参构造函数
        //动态创建IAnimal的有参构造函数实现实例对象Dog
        animal = (IAnimal)Activator.CreateInstance(t, new object[] { "阿黄" })!;
    }
    else
    {
        //无参构造函数
        //动态创建IAnimal的无参构造函数实现实例对象Cat
        animal = (IAnimal)Activator.CreateInstance(t, new object[] { })!;
    }
    animal.Cry();
}


//依赖注入创建写法
Console.WriteLine("======================2.2依赖注入创建写法======================");
IServiceCollection sc = new ServiceCollection();
var _serviceProvider = sc.BuildServiceProvider();
foreach (Type t in types)
{
    var constructors = t.GetTypeInfo().DeclaredConstructors
                    .Where(c => !c.IsStatic && c.IsPublic)
                    .ToArray();

    if (constructors.Length != 1)
    {
        throw new ArgumentException($"entity type :[{t}] found more than one  declared constructor ");
    }
    var @params = constructors[0].GetParameters().Select(x => _serviceProvider.GetService(x.ParameterType)).ToArray();
    if (@params.Length.Equals(1))
    {
        @params[0] = "小黑";
    }
    IAnimal animal = (IAnimal)Activator.CreateInstance(t, @params)!;
    animal.Cry();
}
Console.WriteLine("======================2.3依赖注入创建写法======================");
foreach (Type t in types)
{
    var constructors = t.GetTypeInfo().DeclaredConstructors
                    .Where(c => !c.IsStatic && c.IsPublic)
                    .ToArray();

    if (constructors.Length != 1)
    {
        throw new ArgumentException($"entity type :[{t}] found more than one  declared constructor ");
    }
    var @params = constructors[0].GetParameters().Select(x => _serviceProvider.GetService(x.ParameterType)).ToArray();
    if (@params.Length.Equals(1))
    {
        @params[0] = "小白";
    }
    IAnimal animal = (IAnimal)ActivatorUtilities.CreateInstance(_serviceProvider, t, @params);
    animal.Cry();
}
Console.WriteLine("======================2.4接口参数动态调用注入创建写法======================");
foreach (Type t in types)
{
    var constructors = t.GetTypeInfo().DeclaredConstructors
                    .Where(c => !c.IsStatic && c.IsPublic)
                    .ToArray();

    if (constructors.Length != 1)
    {
        throw new ArgumentException($"entity type :[{t}] found more than one  declared constructor ");
    }
    var params_ctor = constructors[0].GetParameters().Select(x => _serviceProvider.GetService(x.ParameterType)).ToArray();
    if (params_ctor.Length.Equals(1))
    {
        params_ctor[0] = "小黄";
    }
    var animal = ActivatorUtilities.CreateInstance(_serviceProvider, t, params_ctor);
    var method = t.GetMethod("Cry", BindingFlags.Public | BindingFlags.Instance)!;
    if (method == null)
    {
        continue;
    }
    method.Invoke(animal, null);
}
//var interfaceImplements = Assembly
//    .GetExecutingAssembly()
//    .GetTypes()
//    .Where(item => item.GetInterfaces().Contains(typeof(IAnimal)))
//    .Select(type => (IAnimal)Activator.CreateInstance(type)!)
//    .ToList();
//foreach (var impl in interfaceImplements)
//{
//    var method = impl.GetType().GetMethod("Cry", BindingFlags.Public | BindingFlags.Instance)!;
//    if (method == null)
//    {
//        continue;
//    }
//    var @params = method.GetParameters().Select(x => _serviceProvider.GetService(x.ParameterType)).ToArray();
//    if (@params.Length.Equals(1))
//    {
//        @params[0] = "小红";
//    }
//    method.Invoke(impl, @params);
//}

////获取实现ICar的实例对象
//var types2 = AppDomain.CurrentDomain.GetAssemblies()
//                        .SelectMany(a => a.GetTypes().Where(t => t.BaseType == typeof(ICar)))
//                        .ToList();
//foreach (Type t in types2)
//{
//    ICar car;
//    var constructors = t.GetTypeInfo().DeclaredConstructors
//                    .Where(c => !c.IsStatic && c.IsPublic)
//                    .ToArray();

//    if (constructors.Length != 1)
//    {
//        throw new ArgumentException($"entity type :[{t}] found more than one  declared constructor ");
//    }

//    constructors[0].GetParameters().ToList().ForEach(f =>
//    {
//        var pt = f.ParameterType;
//        Console.WriteLine(pt.Name);
//    });

//    var @params = constructors[0].GetParameters().Select(x => _serviceProvider.GetService(x.ParameterType)).ToArray();
//    if (@params.Length.Equals(1))
//    {
//        @params[0] = "525";
//    }
//car = (ICar)Activator.CreateInstance(t, @params)!;
//car.Drive();
//}

Console.ReadKey();
