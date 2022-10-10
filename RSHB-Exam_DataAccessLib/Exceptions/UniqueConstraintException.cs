using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_DataAccessLib.Exceptions
{
    public class UniqueConstraintException : Exception
    {
        public UniqueConstraintException() : base("Ошибка ограничения уникальности поля")
        {

        }
    }
}
