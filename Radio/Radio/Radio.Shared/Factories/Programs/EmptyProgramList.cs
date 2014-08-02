using System.Threading.Tasks;
using Radio.Models;

namespace Radio.Factories.Programs
{
    class EmptyProgramList : ProgramList
    {
        protected override async Task RefreshPrograms()
        {
            await Task.Delay(60000);
        }
    }
}
