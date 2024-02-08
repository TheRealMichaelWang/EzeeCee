using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.IntermediateRepresentation
{
    public partial interface IElement
    {

    }

    public partial interface IValue : IElement
    {

    }

    public partial interface IStatement : IValue
    {

    }

    public partial interface IStaticLiteral : IValue
    {

    }
}
