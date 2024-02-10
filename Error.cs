using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzeeCee
{
    public partial class Error
    {
        public string ErrorMessage { get; private set; }
        public Error? InnerError { get; private set; }

        public Syntax.IElement ErrorReportedElement { get; private set; }

        public Error(string errorMessage, Syntax.IElement errorReportedElement, Error? innerError = null)
        {
            ErrorMessage = errorMessage;
            InnerError = innerError;
            ErrorReportedElement = errorReportedElement;
        }

        public virtual void Print(int indirection = 0)
        {
            void Indent(int offset = 0)
            {
                for (int i = 0; i < offset + indirection; i++)
                    Console.Write('\t');
            }

            Indent();
            Console.WriteLine(ErrorMessage);
            Indent(1);
            Console.WriteLine($"At \"{ErrorReportedElement.SourceLocation.FileName}\", row {ErrorReportedElement.SourceLocation.Row}, column {ErrorReportedElement.SourceLocation.Column}");
            Indent(2);
            
        }
    }
}
