using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptekaRu.DAL.Enums
{
	public enum ConstraintType
	{
		[Description("CHECK")]
		Check,
		[Description("FOREIGN KEY")]
        ForeignKey,
		[Description("PRIMARY KEY")]
		PrimaryKey,
		[Description("UNIQUE")]
		Unique
    }
}
