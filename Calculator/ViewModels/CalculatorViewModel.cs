using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculator.Commands;
using Calculator.Models;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : ViewModelBase, IOperations
    {
        private readonly CalculatorModel _model;


        public string Expression => _model.Expression;
        public string Result => _model.Result;


        public ICommand InsertDigitCommand => new DelegatingCommand(o => InsertDigit((string)o));
        public void InsertDigit(string digit)
        {
            _model.InsertDigit(digit);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }

        public void AddBracket()
        {
            throw new NotImplementedException();
        }

        public ICommand AddCommand => new DelegatingCommand(Add);
        public void Add()
        {
            _model.Add();
            OnPropertyChanged(nameof(Expression));
        }

        public ICommand SubstructCommand => new DelegatingCommand(Substruct);
        public void Substruct()
        {
            _model.Substruct();
            OnPropertyChanged(nameof(Expression));
        }

        public ICommand MultiplyCommand => new DelegatingCommand(Multiply);
        public void Multiply()
        {
            _model.Multiply();
            OnPropertyChanged(nameof(Expression));
        }

        public ICommand DivideCommand => new DelegatingCommand(Divide);
        public void Divide()
        {
            _model.Divide();
            OnPropertyChanged(nameof(Expression));
        }

        public ICommand ClearCommand => new DelegatingCommand(Clear);
        public void Clear()
        {
            _model.Clear();
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }


        public ICommand BackspaceCommand => new DelegatingCommand(Backspace);
        public void Backspace()
        {
            _model.Backspace();
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }

        public ICommand EqualCommand => new DelegatingCommand(Equal);
        public void Equal()
        {
            _model.Equal();
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }
        public CalculatorViewModel()
        {
            _model = new CalculatorModel();
        }
    }
}
