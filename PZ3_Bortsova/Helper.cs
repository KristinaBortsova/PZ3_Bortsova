using PZ3_Bortsova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_Bortsova
{
    public class Helper
    {
        private static PZ4_ModulEntities _context; //Создание статической приватной переменной, для обращения к контексту модели данных

        ///Метод (публичный, чтобы к нему можно было обратиться из любой части программы)
        ///получения контекста данных, необходимый для создания подключения к БД
        public static PZ4_ModulEntities GetContext()
        {
            //Условие при котором проверяется, что если подключение не установлено, то необходимо создать новое подключение
            if (_context == null)
            {
                _context = new PZ4_ModulEntities();//Создание нового подключения к БД
            }
            return _context;//возвращение подключения
        }
    }
}
