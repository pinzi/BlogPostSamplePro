namespace DynamicCreate
{
    /// <summary>
    /// 狗
    /// </summary>
    public class Dog : IAnimal
    {
        /// <summary>
        /// 名字
        /// </summary>
        private string _name { get; }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        /// <param name="name">狗名</param>
        public Dog(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 狗叫
        /// </summary>
        public void Cry()
        {
            Console.WriteLine($"{_name}汪汪汪");
        }
    }
}
