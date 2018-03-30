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

        public ICommand InsertCommand => new DelegatingCommand(o => Insert((string)o));
        public void Insert(string digit)
        {
            _model.Insert(digit);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }

        public ICommand InsertOperationCommand => new DelegatingCommand(o => InsertOperation((Operations)o));
        public void InsertOperation(Operations operation)
        {
            _model.InsertOperation(operation);
            OnPropertyChanged(nameof(Expression));
            OnPropertyChanged(nameof(Result));
        }

        public CalculatorViewModel()
        {
            _model = new CalculatorModel();
        }
    }
}
