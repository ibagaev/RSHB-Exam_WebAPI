using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSHB_Exam_BusinessLogicLib.Exceptions
{
    public class RSHBExamNotFoundException : RSHBExamException
    {
        public RSHBExamNotFoundException(long id) : base($"Сотрудник с ID: [{id}] не найден!")
        {

        }

        public RSHBExamNotFoundException(string fullName) : base($"Сотрудник с ФИО: [{fullName}] не найден!")
        {

        }
    }
}
