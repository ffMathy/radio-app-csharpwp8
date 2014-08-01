using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Radio.Models;

namespace RadioV2.Controllers.Programs
{
    class EmptyProgramList : ProgramList
    {
        protected override async Task RefreshPrograms()
        {
            await Task.Delay(60000);
        }
    }
}
