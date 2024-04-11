using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.Constants
{
    public class ResponseMessages
    {
        public const string SuccessfullOperation = "Successfull Operation";
        public const string WrongFormat = "Wrong Format:";
        public const string EducationDisplay = "Id: {0}, Name: {1}, Color: {2}, Date of Creation: {3}";
        public const string GroupDisplay = "Id: {0}, Name: {1}, Capacity: {2}, Education Name: {3}, Date of Creation: {4}";
        public const string RegisterSuccess = "Successfull registration";
        public const string LoginSuccess = "Successfull login";
        public const string EmptyInput = "Input cannot be empty";
        public const string NotFound = "Data was not found";
        public const string GroupDTO = "Name: {0}, Capacity: {1}";
        public const string EducationDTO = "Name: {0}, Color: {1}";
        public const string MixedDTO = "Education: {0},Groups {1}";
    } 
}
