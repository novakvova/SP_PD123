using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsTask
{
    /// <summary>
    /// Керування користувачами
    /// </summary>
    public class UserService
    {
        public Task InsertRandomUserAsync(int count)
        {
            return Task.Run(() => InsertRandomUser(count));
        }
        //Додвання рандомних даних в БД
        public void InsertRandomUser(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(100);
            }
        }
    }
}
