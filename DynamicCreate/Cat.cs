namespace DynamicCreate
{
    /// <summary>
    /// 猫
    /// </summary>
    public class Cat : IAnimal
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Cat()
        {

        }

        /// <summary>
        /// 猫叫
        /// </summary>
        public void Cry()
        {
            Console.WriteLine("喵喵喵");
        }
    }
}
