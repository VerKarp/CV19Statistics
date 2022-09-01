using CV19Statistics.Infrastructure.Commands.Base;

namespace CV19Statistics.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => App.Current.Shutdown();
    }
}