using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_BusinessLogicLib.Exceptions
{
    public class RSHBExamAlreadyExistExeption : RSHBExamException
    {
        public RSHBExamAlreadyExistExeption(string fullName) : base($"Сотрудник: [{fullName}] уже существует!")
        {

        }
    }
}
