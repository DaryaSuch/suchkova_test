using System;
using System.Threading.Tasks;

namespace go
{
    public class Wait
    {
        public static async Task TimeWait()
        {
            int result = await Task.Run(() => (6*4));
            var taskDelay = Task.Delay(3000); 
            await taskDelay; 
            Console.WriteLine(result);
        }
    }
}
