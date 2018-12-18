using System.Collections.Generic;

namespace ListApp.BusinessObjects
{
	public class ODataResult<T> where T : class
	{
		public T value { get; set; }
	}
}