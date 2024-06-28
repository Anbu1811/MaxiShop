using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.ApplicatioConstants
{
	public class ApplicatioConstant
	{

	}

	public class CommonMessage
	{

		public const string RegistrationSuccess = "Registration Successfully";
		public const string RegistrationFailed = "Registration Failed";

		public const string LoginSuccess = "Login Successfully ";
		public const string LoginFailed = "Login Failed ";

		public const string CreateOperationSuccess = "Record Created Successfully";
		public const string DeleteOperationSuccess = "Record Delete Successfully";
		public const string UpdateOperationSuccess = "Record Update Successfully";

		public const string CreateOperationFailed = "Created Operation Failed";
		public const string DeleteOperationFailed = "Delete Operation Failed";
		public const string UpdateOperationFailed = "Update Operation Failed";

		public const string RecordNotFound = "Record Not Found";
		public const string SystemError = "Something Went Wrong";

	}
}
