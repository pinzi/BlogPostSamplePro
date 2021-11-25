using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicCreate
{
    public class ICar
    {
        public virtual void Drive()
        {

        }
    }

    public class BMW : ICar
    {
        private string _name;
        public BMW(string name)
        {
            _name = name;
        }
        public override void Drive()
        {
            Console.WriteLine($"{_name}宝马开起来");
        }
    }
}
