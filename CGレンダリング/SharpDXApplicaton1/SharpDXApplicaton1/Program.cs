using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDXApplicaton1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            
            using (MyGame7 mygame = new MyGame7())
            {
                mygame.Run();
            }
            
            
        }
    }
}
