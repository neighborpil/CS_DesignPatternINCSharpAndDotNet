using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec94_MementoForInterop
{
    /*
     * # Memento Pattern은 Interop간에 사용이 가능하다
     *  - 만약 C#과 C++간에 데이터를 주고 받을 경우 직접적으로 호환이 불가능하다
     *  - 데이터가 같은 int라도 바이트수가 다르거나 내부적으로 동작하는 방식이 다를 수 있기 때문이다
     *  - 따라서 memento pattern을 이용하여 토큰을 교환하는 방식으로 사용하면 좋다
     * 
     */

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
