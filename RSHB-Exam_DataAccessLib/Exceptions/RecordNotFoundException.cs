using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_DataAccessLib.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(long id) : base($"Запись c ID [{id}] в базе данных не существует!")
        {

        }

        public RecordNotFoundException(string fieldName, string value) : base($"Запись c {fieldName} [{value}] в базе данных не существует!")
        {

        }
    }
}
