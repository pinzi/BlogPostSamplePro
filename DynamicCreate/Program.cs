using DynamicCreate;

Console.WriteLine("======================常规写法======================");
IAnimal animal_Dog = new Dog("旺财");
animal_Dog.Cry();
IAnimal animal_Cat = new Cat();
animal_Cat.Cry();


Console.WriteLine("======================动态创建写法======================");
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


Console.ReadKey();
