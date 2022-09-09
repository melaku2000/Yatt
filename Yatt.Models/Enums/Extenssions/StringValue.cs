using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatt.Models.Enums
{
	public class StringValue : Attribute
	{
		public string Value { get; private set; }

		public StringValue(string value)
		{
			Value = value;
		}
	}
}
