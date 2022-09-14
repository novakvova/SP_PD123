using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsTask
{
    //Вказівник на функцію, яка приймає ціле число і нічого не повертає
    public delegate void AddCurrentUserDelegate(int count);
    /// <summary>
    /// Керування користувачами
    /// </summary>
    public class UserService
    {
        //Подія яка спрацьовує при додавані користвача
        public event AddCurrentUserDelegate AddCurrentUserEvent;
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
                if (AddCurrentUserEvent != null)
                    AddCurrentUserEvent(i+1);
            }
        }
    }
}
