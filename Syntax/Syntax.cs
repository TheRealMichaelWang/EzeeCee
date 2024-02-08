using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee.Syntax
{
    public struct SourceLocation
    {
        public string FileName { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public SourceLocation(string fileName, int row, int column)
        {
            FileName = fileName;
            Row = row;
            Column = column;
        }
    }

    public partial interface IElement
    {
        public SourceLocation SourceLocation { get; }
    }

    public partial interface IValue : IElement
    {
           
    }

    public partial interface IStatement : IElement
    {

    }
}